using System.ComponentModel.DataAnnotations;

namespace Web_App.ViewModels
{
    public class CandidateCreateViewModel
    {
        [Required]
        public string Name { get; set; }
        
        [Phone]    
        public string Phone { get; set; }
        
        [Required]
        public int VacancyId { get; set; }
        
        public string Avatar { get; set; }
    }
}