using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopProductExpirationAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            using (ShopProductExpirationContext db = new ShopProductExpirationContext())
            {
                return await db.Products.ToListAsync();

            }


        }

        [HttpGet("{dateTimeNowFromWeb}")]
        public async Task<ActionResult<Product>> Get(string dateTimeNowFromWeb)
        {
                
            if (dateTimeNowFromWeb != null)
            {
                DateTime dateTimeNow = DateTime.Parse(dateTimeNowFromWeb);

                using (ShopProductExpirationContext db = new ShopProductExpirationContext())
                {
                    var product = db.Products.Where(x =>  dateTimeNow > x.SellBy).ToArray();
                    if (product == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        return new ObjectResult(product);
                    }
                }
            }
            else
            {
                return BadRequest();
            }
             

        }

        [HttpPost]
        public async Task<ActionResult<Product>> Post(string productName, 
            string productManufacturingDate,
            string productPackagingDate,
            string shelfLife,
            string timeUnits,
            string sellBy,
            string productCount,
            string countUnits,
            string shopDepartment,
            string departmentHeadFio,
            string rowNumber,
            string shelvingNumber,
            string shelfNumber
        )
        {
            if (shelfNumber == null)
            {
                return BadRequest();
            }

            try
            {
                Product newProduct = new Product
                {
                    ProductName = productName,
                    ProductManufacturingDate = DateTime.Parse(productManufacturingDate),
                    ProductPackagingDate = DateTime.Parse(productPackagingDate),
                    ShelfLife = Convert.ToInt16(shelfLife),
                    TimeUnits = timeUnits,
                    SellBy =  DateTime.Parse(sellBy),
                    ProductCount = Convert.ToInt16(productCount),
                    CountUnits = countUnits,
                    ShopDepartment = shopDepartment,
                    DepartmentHeadFio = departmentHeadFio,
                    RowNumber = Convert.ToInt16(rowNumber),
                    ShelvingNumber = Convert.ToInt16(shelvingNumber),
                    ShelfNumber = Convert.ToInt16(shelfNumber)
                };
                using (ShopProductExpirationContext db = new ShopProductExpirationContext())
                {
                    db.Products.Add(newProduct);
                    await db.SaveChangesAsync();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
      
        [HttpPut]
        public async Task<ActionResult<Product>> Put(
            string Id,
            string productName,
            string productManufacturingDate,
            string productPackagingDate,
            string shelfLife,
            string timeUnits,
            string sellBy,
            string productCount,
            string countUnits,
            string shopDepartment,
            string departmentHeadFio,
            string rowNumber,
            string shelvingNumber,
            string shelfNumber
        )
        {
            using (ShopProductExpirationContext db = new ShopProductExpirationContext())
            {
                if (!db.Products.Any(x => x.Id == Convert.ToInt32(Id)))
                {
                    return NotFound();
                }

                Product newProduct = new Product
                {
                    Id = Convert.ToInt32(Id),
                    ProductName = productName,
                    ProductManufacturingDate = DateTime.Parse(productManufacturingDate),
                    ProductPackagingDate = DateTime.Parse(productPackagingDate),
                    ShelfLife = Convert.ToInt16(shelfLife),
                    TimeUnits = timeUnits,
                    SellBy = DateTime.Parse(sellBy),
                    ProductCount = Convert.ToInt16(productCount),
                    CountUnits = countUnits,
                    ShopDepartment = shopDepartment,
                    DepartmentHeadFio = departmentHeadFio,
                    RowNumber = Convert.ToInt16(rowNumber),
                    ShelvingNumber = Convert.ToInt16(shelvingNumber),
                    ShelfNumber = Convert.ToInt16(shelfNumber)
                };

                db.Update(newProduct);
                await db.SaveChangesAsync();
                return Ok(newProduct);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> Delete(int id)
        {
            using (ShopProductExpirationContext db = new ShopProductExpirationContext())
            {
                Product product = db.Products.FirstOrDefault(x => x.Id == id);
                if (product == null)
                {
                    return NotFound();
                }
                db.Products.Remove(product);
                await db.SaveChangesAsync();
                return Ok(product);
            }
            
        }
    }
}
