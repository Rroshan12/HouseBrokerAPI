using HouseBroker.Infra.Dtos;
using HouseBroker.Infra.Interface;
using HouseBroker.Infra.Mappers;
using HouseBroker.Infra.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBroker.Infra.Services
{

    public interface IPropertyListingService
    {
        Task<List<PropertyListingDtos>> GetAllListingsAsync();
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

        public async Task<List<PropertyListingDtos>> GetAllListingsAsync()
        {
            var listings = await _unitOfWork.PropertyListingRepository.GetAll();
            return Mapper.Convert(listings.ToList());
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
                await _unitOfWork.PropertyListingRepository.Insert(entity);
                await _unitOfWork.SaveAsync();

                if (listingDto.ImageUrl?.Count > 0)
                {
                    foreach (var image in listingDto.ImageUrl)
                    {
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
            var entity = Mapper.Convert(listingDto);
            _unitOfWork.PropertyListingRepository.Update(entity);
            await _unitOfWork.SaveAsync();
            return Mapper.Convert(entity);
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
