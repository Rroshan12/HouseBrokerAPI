using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBroker.Domain.Models.Identity
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }

        public virtual ICollection<ApplicationRoleClaim> RoleClaims { get; set; }


        public virtual string Description { get; set; }

        public virtual int RolePriority { get; set; }
    }
}
