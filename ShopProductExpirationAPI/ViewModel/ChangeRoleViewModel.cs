using ShopProductExpirationAPI.Models;
using System.Collections.Generic;

namespace ShopProductExpirationAPI.ViewModel
{
    public class ChangeRoleViewModel
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public List<ShopEmploye> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }
        public ChangeRoleViewModel()
        {
            AllRoles = new List<ShopEmploye>();
            UserRoles = new List<string>();
        }
    }
}
