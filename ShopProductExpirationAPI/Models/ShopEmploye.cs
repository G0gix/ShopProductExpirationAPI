using Microsoft.AspNetCore.Identity;

namespace ShopProductExpirationAPI.Models
{
    public class ShopEmploye : IdentityUser
    {
        public string EmployeName { get; set; }
    }
}
