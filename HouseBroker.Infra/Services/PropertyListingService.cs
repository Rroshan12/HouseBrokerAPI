﻿using HouseBroker.Domain.Models;
using HouseBroker.Domain.Models.enums;
using HouseBroker.Infra.Dtos;
using HouseBroker.Infra.Dtos.FilterDtos;
using HouseBroker.Infra.Helpers;
using HouseBroker.Infra.Interface;
using HouseBroker.Infra.Mappers;
using HouseBroker.Infra.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HouseBroker.Infra.Services
{

    public interface IPropertyListingService
    {
        Task<PagedObject<PropertyListing>> GetAllListingsAsync(PropertyListingFilter filter);
        Task<PropertyListingDtos> GetListingByIdAsync(Guid id);
        Task<PropertyListingDtos> AddListingAsync(PropertyListingDtos listingDto);
        Task<PropertyListingDtos> UpdateListingAsync(PropertyListingDtos listingDto);
        Task<bool> DeleteListingAsync(Guid id);
    }
    public class PropertyListingService : IPropertyListingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PropertyListingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedObject<PropertyListing>> GetAllListingsAsync(PropertyListingFilter filter)
        {
            try
            {
                var includes = new[]
                {
            nameof(PropertyListing.Images),
            nameof(PropertyListing.Broker)
        };

                var searchTerm = filter.SearchTerm?.ToLower();

                // Match PropertyType enums based on search term (e.g., "apartment" matches PropertyType.Apartment)
                var matchingPropertyTypes = new List<PropertyType>();
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    matchingPropertyTypes = Enum.GetValues(typeof(PropertyType))
                        .Cast<PropertyType>()
                        .Where(e => e.ToString().ToLower().Contains(searchTerm))
                        .ToList();
                }

                // SQL-compatible expression
                Expression<Func<PropertyListing, bool>> dbFilter = p =>
                    (!filter.MinPrice.HasValue || p.Price >= filter.MinPrice.Value) &&
                    (!filter.MaxPrice.HasValue || p.Price <= filter.MaxPrice.Value) &&
                    p.IsActive &&
                    (string.IsNullOrWhiteSpace(searchTerm) ||
                     p.Title.ToLower().Contains(searchTerm) ||
                     p.Location.ToLower().Contains(searchTerm) ||
                     matchingPropertyTypes.Contains(p.PropertyType));

                var query = _unitOfWork.PropertyListingRepository
                    .SelectWhereIncludeQuery(includes, dbFilter);

                var totalRecords = await query.CountAsync();

                var pagedItems = await query
                    .OrderByDescending(p => p.CreatedAt)
                    .Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize)
                    .ToListAsync();

                var totalPages = (int)Math.Ceiling((double)totalRecords / filter.PageSize);

                return new PagedObject<PropertyListing>(
                    pagedItems,
                    totalPages,
                    filter.PageNumber,
                    totalRecords
                );
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving listings: " + ex.Message);
            }
        }


        public async Task<PropertyListingDtos> GetListingByIdAsync(Guid id)
        {
            var result = await _unitOfWork.PropertyListingRepository.GetById(id);
            return Mapper.Convert(result);
        }

        public async Task<PropertyListingDtos> AddListingAsync(PropertyListingDtos listingDto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var entity = Mapper.Convert(listingDto);
                entity.Id = Guid.NewGuid();
                await _unitOfWork.PropertyListingRepository.Insert(entity);
                await _unitOfWork.SaveAsync();

                if (listingDto.ImageUrl?.Count > 0)
                {
                    foreach (var image in listingDto.ImageUrl)
                    {
                        image.Id = Guid.NewGuid();
                        image.PropertyListingId = entity.Id;
                        await _unitOfWork.PropertyImageRepository.Insert(Mapper.Convert(image));  

                    }
                }

                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();

                return Mapper.Convert(entity);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return null;
            }
        }

        public async Task<PropertyListingDtos> UpdateListingAsync(PropertyListingDtos listingDto)
        {
            try
            {
                var entity = Mapper.Convert(listingDto);
                    _unitOfWork.PropertyListingRepository.Update(entity);
               await  _unitOfWork.SaveAsync();
                return Mapper.Convert(entity);

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
          
        }

        public async Task<bool> DeleteListingAsync(Guid id)
        {
            var listing = await _unitOfWork.PropertyListingRepository.GetById(id);
            if (listing == null)
                return false;

            listing.IsActive = false; // Soft delete
            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}
