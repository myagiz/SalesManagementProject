using Core.Repositories.Entity;
using DataAccess.Abstract;
using Entities.DTOs;
using Entities.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EfCore
{
    public class EfCoreProductDal : EfCoreEntityRepository<Product, SalesManagementDBContext>, IProductDal
    {
        public async Task AddProduct(CreateProductDto model)
        {
            using (var context = new SalesManagementDBContext())
            {
                Product entity = new Product();
                entity.Name = model.Name;
                entity.ImageSrc = model.ImageSrc;
                entity.CategoryId = model.CategoryId;
                entity.Salesprice = model.Salesprice;
                context.Products.Add(entity);
                await context.SaveChangesAsync();
            }
        }

        public void DeleteProduct(int id)
        {
            using (var context = new SalesManagementDBContext())
            {
                var controlProduct = context.Products.FirstOrDefault(x => x.Id == id);

                if (controlProduct != null)
                {
                    var controlProductStock = context.Stocks.FirstOrDefault(x => x.ProductId == id);

                    if (controlProductStock != null) { context.Stocks.RemoveRange(controlProductStock); }

                    context.Products.Remove(controlProduct);
                    context.SaveChanges();


                }

            }
        }

        public List<GetAllProductDto> GetAllProducts()
        {
            using (var context = new SalesManagementDBContext())
            {
                var getData = (from a in context.Products
                               join b in context.Categories on a.CategoryId equals b.Id
                               select new GetAllProductDto
                               {
                                   Id = a.Id,
                                   Name = a.Name,
                                   CategoryId = a.CategoryId,
                                   CategoryName = b.Name,
                                   ImageSrc = a.ImageSrc,
                                   Salesprice = a.Salesprice,
                               }
                             ).ToList();

                return getData;
            }
        }

        public GetAllProductDto GetProductById(int id)
        {
            using (var context = new SalesManagementDBContext())
            {
                var getData = (from a in context.Products.Where(x => x.Id == id)
                               join b in context.Categories on a.CategoryId equals b.Id
                               select new GetAllProductDto
                               {
                                   Id = a.Id,
                                   Name = a.Name,
                                   CategoryId = a.CategoryId,
                                   CategoryName = b.Name,
                                   ImageSrc = a.ImageSrc,
                                   Salesprice = a.Salesprice,
                               }
                             ).FirstOrDefault();
                if (getData != null)
                {
                    return getData;

                }
                return null;
            }
        }

        public async Task UpdateProduct(UpdateProductDto model)
        {
            using (var context = new SalesManagementDBContext())
            {
                var controlProduct = context.Products.FirstOrDefault(x => x.Id == model.Id);
                if (controlProduct != null)
                {
                    controlProduct.CategoryId = model.CategoryId;
                    controlProduct.Name = model.Name;
                    controlProduct.ImageSrc = model.ImageSrc;
                    controlProduct.Salesprice = model.Salesprice;
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
