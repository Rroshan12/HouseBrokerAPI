using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBroker.Infra.Dtos
{
    public class TokenDataDtos
    {
        public string Username { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        public string RoleDescription { get; set; }
        public string exp { get; set; }
        public string iss { get; set; }

    }
}
