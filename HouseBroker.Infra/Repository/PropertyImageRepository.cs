using HouseBroker.Domain.Models;
using HouseBroker.Infra.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBroker.Infra.Repository
{
    public class PropertyImageRepository : Repository<PropertyImage>, IPropertyImageRepository
    {
        public PropertyImageRepository(DbManagerContext context) : base(context)
        {
        }
    }
}
