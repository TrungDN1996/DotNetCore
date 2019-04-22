using System.ComponentModel.DataAnnotations;

namespace Web_App.ViewModels
{
    public class CandidateViewModel
    {
        public int Id { get; set; }
    
        public string Name { get; set; }
        
        public string Phone { get; set; }
        
        public string VacancyName { get; set; }
        
        public string Avatar { get; set; }
    }
}