using HouseBroker.Domain.Models;
using HouseBroker.Domain.Models.enums;
using HouseBroker.Domain.Models.Identity;
using HouseBroker.Domain.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBroker.Infra.Dtos
{
    public class PropertyListingDtos
    {
        [Key]
        public Guid? Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public PropertyType PropertyType { get; set; }

        [Required]
        public string Location { get; set; }
        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [MustContainCommaIfMultipleWords(ErrorMessage = "Features must be a comma-separated list.")]
        public string Features { get; set; }

        public List<PropertyImageDtos> ImageUrl { get; set; }

        public Guid BrokerId { get; set; }



        public DateTime CreatedAt { get; set; }

        public bool IsActive { get; set; } = true;
    }

   
}
