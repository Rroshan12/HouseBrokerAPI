using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBroker.Domain.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PropertyImage
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Url { get; set; }
        public Guid PropertyListingId { get; set; }
        public PropertyListing PropertyListing { get; set; }
    }

}
