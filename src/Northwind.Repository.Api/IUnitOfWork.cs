using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Northwind.Repository.Api
{
    public interface IUnitOfWork
    {
        // membuat property dengan tipe interface repository
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductRepository { get; }

        void BeginTransaction();
        void Commit();
    }
}
