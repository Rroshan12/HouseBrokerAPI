using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBroker.Infra.Interface
{
    public interface IUnitOfWork
    {
        public Task BeginTransactionAsync();

        public Task CommitTransactionAsync();
        public Task RollbackTransactionAsync();


        public Task<int> SaveAsync();

        public void Dispose();

    }
}
