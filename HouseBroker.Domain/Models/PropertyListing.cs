using HouseBroker.Domain.Models.enums;
using HouseBroker.Domain.Models.Identity;
using HouseBroker.Domain.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBroker.Domain.Models
{

    public class PropertyListing
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public PropertyType PropertyType { get; set; } 
        public string Location { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        [Required]
        [MustContainCommaIfMultipleWords(ErrorMessage = "Features must be a comma-separated list.")]
        public string Features { get; set; } //add comma separated features

        public Guid BrokerId { get; set; }
        public ApplicationUser Broker { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsActive { get; set; } = true;
        public ICollection<PropertyImage> Images { get; set; }
    }

}
