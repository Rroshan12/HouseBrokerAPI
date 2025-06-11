using HouseBroker.Domain.Models;
using HouseBroker.Infra.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBroker.Infra.Mappers
{
    public static class Mapper
    {

        #region PropertyListing
        public static PropertyListingDtos Convert(PropertyListing entity)
        {
            if (entity == null)
                return null;

            return new PropertyListingDtos
            {
                Id = entity.Id,
                Title = entity.Title,
                PropertyType = entity.PropertyType,
                Location = entity.Location,
                Price = entity.Price,
                Description = entity.Description,
                Features = entity.Features,
                BrokerId = entity.BrokerId,
                CreatedAt = entity.CreatedAt,
                IsActive = entity.IsActive,
                //ImageUrl = entity.Images?.ToList() ?? new List<PropertyImage>()
            };
        }

        public static PropertyListing Convert(PropertyListingDtos dto)
        {
            if (dto == null)
                return null;

            return new PropertyListing
            {
                Id = dto.Id ?? Guid.NewGuid(),
                Title = dto.Title,
                PropertyType = dto.PropertyType,
                Location = dto.Location,
                Price = dto.Price,
                Description = dto.Description,
                Features = dto.Features,
                BrokerId = dto.BrokerId,
                CreatedAt = dto.CreatedAt == default ? DateTime.UtcNow : dto.CreatedAt,
                IsActive = dto.IsActive,
                //Images = dto.ImageUrl?.ToList() ?? new List<PropertyImage>()
            };
        }

        public static List<PropertyListingDtos> Convert(List<PropertyListing> entities)
        {
            if (entities == null)
                return null;

            return entities.Select(Convert).ToList();
        }

        #endregion PropertyListing

        #region PropertyImage

        public static PropertyImageDtos Convert(PropertyImage entity)
        {
            if (entity == null)
                return null;

            return new PropertyImageDtos
            {
                Id = entity.Id,
                Url = entity.Url,
                PropertyListingId = entity.PropertyListingId
            };
        }

        public static PropertyImage Convert(PropertyImageDtos dto)
        {
            if (dto == null)
                return null;

            return new PropertyImage
            {
                Id = dto.Id ?? Guid.NewGuid(),
                Url = dto.Url,
                PropertyListingId = dto.PropertyListingId ?? new Guid()
            };
        }

        public static List<PropertyImageDtos> Convert(List<PropertyImage> entities)
        {
            if (entities == null)
                return null;

            return entities.Select(Convert).ToList();
        }



        #endregion PropertyImage

    }
}
