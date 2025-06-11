using HouseBroker.Domain.Models;
using HouseBroker.Infra.Interface;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBroker.Infra.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbManagerContext _context;
        private bool _disposed = false;

        public UnitOfWork(DbManagerContext context)
        {
            _context = context;
        }

    
        public IDbContextTransaction _transaction { get; set; }

        public IPropertyListingRepository _propertyListingRepo;
        public IPropertyImageRepository _propertyImageRepo;


        public IPropertyListingRepository PropertyListingRepository
        {
            get
            {
                if (_propertyListingRepo == null)
                {
                    _propertyListingRepo = new PropertyListingRepository(_context);
                }
                return _propertyListingRepo;
            }
        }

        public IPropertyImageRepository PropertyImageRepository
        {
            get
            {
                if (_propertyImageRepo == null)
                {
                    _propertyImageRepo = new PropertyImageRepository(_context);
                }
                return _propertyImageRepo;
            }
        }

        public async Task BeginTransactionAsync()
        {
            _transaction ??= await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true); GC.SuppressFinalize(this);
        }

        public async Task RollbackTransactionAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
