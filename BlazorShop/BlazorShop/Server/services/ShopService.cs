using BlazorShop.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BlazorShop.Server.services
{
     public class ShopService : IShopService
     {
          private readonly AppDbContext db;

          public ShopService(AppDbContext db)
          {
               this.db = db;
          }

          public async Task<Product> Edit(int id, Product prod)
          {
               var product = await db.Product.FindAsync(id);
               if (product != null)
               {
                    product.Name = prod.Name;
                    product.Price = prod.Price;
                    product.Stoc = prod.Stoc;
                    await db.SaveChangesAsync();
               }
               return product;
          }
          public async Task<Product> Delete(int id)
          {
               var delete = await db.Product.FindAsync(id);
               if (delete != null)
               {
                    db.Product.Remove(delete);
                    await db.SaveChangesAsync();
               }
               return delete;
          }
          public async Task<Product> Post(Product create)
          {
               await db.Product.AddAsync(create);
               await db.SaveChangesAsync();
               return create;
          }
     }
}
