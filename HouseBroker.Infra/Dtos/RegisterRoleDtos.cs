using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBroker.Infra.Dtos
{
    public class RegisterRoleDtos
    {
        public Guid Id { get; set; }
        public string Description { get; set; }

        public int RolePriority { get; set; }
    }
}
