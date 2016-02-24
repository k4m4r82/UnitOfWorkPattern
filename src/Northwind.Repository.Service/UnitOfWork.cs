using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Northwind.Repository.Api;
using System.Data;

namespace Northwind.Repository.Service
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDapperContext _context;
        private IDbTransaction _transaction;
        private ICategoryRepository _categoryRepository;
        private IProductRepository _productRepository;

        public UnitOfWork(IDapperContext context)
        {
            this._context = context;            
        }

        public ICategoryRepository CategoryRepository
        {
            get { return _categoryRepository ?? (_categoryRepository = new CategoryRepository(_transaction, _context)); }
        }

        public IProductRepository ProductRepository
        {
            get { return _productRepository ?? (_productRepository = new ProductRepository(_transaction, _context)); }
        }

        public void BeginTransaction()
        {
            if (_transaction != null)
                throw new NullReferenceException("Not finished previous transaction");

            _transaction = _context.db.BeginTransaction();
        }

        public void Commit()
        {
            if (_transaction == null)
                throw new NullReferenceException("Tryed commit not opened transaction");

            _transaction.Commit();
        }
    }
}
