using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Northwind.Model;
using Northwind.Repository.Api;
using Northwind.Repository.Service;

namespace Northwind.ConsoleApp
{
    class UnitOfWorkTest
    {
        static void Main(string[] args)
        {
            using (IDapperContext context = new DapperContext())
            {
                // buat objek unit of work
                IUnitOfWork uow = new UnitOfWork(context);

                var result = 0;

                uow.BeginTransaction(); // mulai transaction

                // buat objek category
                var category = new Category
                {
                    CategoryName = "Condiments",
                    Description = "Sweet and savory sauces, relishes, and seasonings"
                };
                result = uow.CategoryRepository.Save(category); // simpan data category
                Console.WriteLine("Tambah data category {0}", result > 0 ? "berhasil" : "gagal");

                // buat objek product
                var product = new Product
                {
                    CategoryID = category.CategoryID,
                    ProductName = "Genen Shouyu",
                    QuantityPerUnit = "24 - 250 ml bottles",
                    UnitPrice = 15.5,
                    UnitsInStock = 50
                };
                result = uow.ProductRepository.Save(product); // simpan data product
                Console.WriteLine("Tambah data product {0}", result > 0 ? "berhasil" : "gagal");

                uow.Commit(); // commit, simpan data secara permanen ke database
            }

            Console.WriteLine("\nPress any key to exit ...");
            Console.ReadKey();
        }        
    }
}
