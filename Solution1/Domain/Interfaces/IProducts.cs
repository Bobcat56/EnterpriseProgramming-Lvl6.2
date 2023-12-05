using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    internal interface IProducts
    {
        /*Note: An interface continas only method signatures (no implementaion)
          Note: An interface is like a contract 
        */

        IQueryable<Product> GetProducts();

        Product? GetProduct(Guid id);

        void AddProduct(Product product);
        void UpdateProduct(Product product);

        void DeleteProduct(Guid id);

    }
}
