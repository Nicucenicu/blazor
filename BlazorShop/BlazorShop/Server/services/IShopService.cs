using BlazorShop.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BlazorShop.Server.services
{
     public interface IShopService
     {
          Task<Product> Edit(int id, Product prod);
     }
}
