using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_App.Helpers;

namespace Web_App.Models
{
    public interface IRoleService
    {
        List<SelectListItem> GetRoleList();
    }
    
    public class RoleService : IRoleService
    {
        private readonly DBContext _context;

        public RoleService(DBContext context)
        {
            _context = context;
        }
        
        public List<SelectListItem> GetRoleList()
        {
            var list = _context.Roles.ToList();
            var rolesList = new List<SelectListItem>();    
            
            foreach (var item in list)
            {
                rolesList.Add(new SelectListItem() {Text = item.Name, Value = item.Id.ToString()});
            }

            return rolesList;
        }
    }
}