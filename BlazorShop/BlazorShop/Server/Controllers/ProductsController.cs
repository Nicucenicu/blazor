using BlazorShop.Server.services;
using BlazorShop.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Runtime.InteropServices;

namespace BlazorShop.Server.Controllers
{
     [ApiController]
     [Route("api/products")]
     public class ProductsController : ControllerBase
     {
          private readonly AppDbContext _db;
          private readonly IShopService _shopService;

          public ProductsController(AppDbContext db, IShopService shopService)
          {
               _db = db;
               _shopService = shopService;
          }

          [HttpPut("{id}")]
          public async Task<Product> Edit(int id, [FromBody] Product prod)
          {
              return await _shopService.Edit(id, prod);
          }

          [HttpDelete("{id}")]
          public async Task<Product> Delete(int id)
          {
               var delete = await _db.Product.FindAsync(id);
               if (delete != null)
               {
                    _db.Product.Remove(delete);
                    await _db.SaveChangesAsync();
               }
               return delete;
          }

          [HttpPost]
          public async Task<Product> Post([FromBody] Product create)
          {
               await _db.Product.AddAsync(create);
               await _db.SaveChangesAsync();
               return create;
          }

          [HttpGet]
          public async Task<IEnumerable<Product>> Get(string name)
          {
               return await _db.Product.Where(x => x.Name.Contains(name)).ToListAsync();
          }

          [HttpGet("list")]
          public async Task<List<Product>> GetProductsAsync()
          {
               return await _db.Product.ToListAsync();
          }

          [HttpGet("{id}")]
          public async Task<Product> Get(int id)
          {
               return await _db.Product.FindAsync(id);
          }
     }
}
