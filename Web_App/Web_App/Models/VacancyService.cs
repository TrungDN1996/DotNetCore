using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web_App.Helpers;

namespace Web_App.Models
{
    public interface IVacancyService
    {
        List<SelectListItem> GetVacanciesList();
    }
    
    public class VacancyService : IVacancyService
    {
        private readonly DBContext _context;

        public VacancyService(DBContext context)
        {
            _context = context;
        }
        
        public List<SelectListItem> GetVacanciesList()
        {
            var list = _context.Vacancies.ToList();
            List<SelectListItem> vacanciesList = new List<SelectListItem>();    
            
            foreach (var item in list)
            {
                vacanciesList.Add(new SelectListItem() {Text = item.Name, Value = item.Id.ToString()});
            }

            return vacanciesList;
        }
    }
}