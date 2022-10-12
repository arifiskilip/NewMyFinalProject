using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal
    {
        public List<Product> GetAllByCategoryId(int categoryId)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                return context.Products.Where(x => x.CategoryId == categoryId).ToList();
            }
        }

        public List<Product> GetByUnitPrice(decimal min, decimal max)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                return context.Products.Where(x => x.UnitPrice 
                >= min && x.UnitPrice<=max).ToList();
            }
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var result = from p in context.Products
                             join c in context.Categories on p.CategoryId equals c.CategoryId
                             select new ProductDetailDto
                             {
                                 CategoryName = c.CategoryName,
                                 ProductId = p.ProductId,
                                 ProductName = p.ProductName,
                                 UnıtsInStock = p.UnitsInStock
                             };
                return result.ToList();
            }

        }
    }
}
