using BlazorShop.Shared;

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
     }
}
