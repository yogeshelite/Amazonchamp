using AmazonwebApi.Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmazonwebApi.Persistance.Repositary
{
    public class ProductRepositary : GenericRepository<amazonchampEntities1>, IProductRepositary
    {
        public List<ProductCategory_Result> GetCategory()
        {
            return Context.ProductCategory().ToList();
        }
    }
    public interface IProductRepositary : IGenericRepository<amazonchampEntities1>
    {
       List<ProductCategory_Result>  GetCategory();
      
    }
}