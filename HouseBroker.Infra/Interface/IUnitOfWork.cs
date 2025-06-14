﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBroker.Infra.Interface
{
    public interface IUnitOfWork
    {
        public IPropertyListingRepository PropertyListingRepository { get; }
        public IPropertyImageRepository PropertyImageRepository { get; }
        public Task BeginTransactionAsync();

        public Task CommitTransactionAsync();
        public Task RollbackTransactionAsync();


        public Task<int> SaveAsync();

        public void Dispose();

    }
}
