using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBroker.Infra.Dtos
{
    public class PropertyImageDtos
    {
        public Guid? Id { get; set; }
        public string Url { get; set; }
        public Guid? PropertyListingId { get; set; }
    }
}
