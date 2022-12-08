using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShop.Shared
{
     public class Product
     {
          [Key]
          public int Id { get; set; } 

          [Required]
          [Display(Name="Nume produs") ]
          [StringLength(30, ErrorMessage ="Nume produs nu poate fi mai lung decat 30")]
          public string? Name { get; set; }

          [Required]
          [Range(1,1000, ErrorMessage ="Pretul nu poate fi mai mic ca 0")]
          public int? Price { get; set; }

          [Required]
          [Range(1, 100, ErrorMessage = "Stocul nu poate fi mai mic ca 0")]
          public int? Stoc { get; set; }

          public byte[] Image { get; set; }

          public string ImageName { get; set; }
     }
}