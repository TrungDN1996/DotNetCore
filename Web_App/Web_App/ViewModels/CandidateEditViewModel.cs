using System.ComponentModel.DataAnnotations;

namespace Web_App.ViewModels
{
    public class CandidateEditViewModel
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Phone]
        public string Phone { get; set; }
        
        [Required]
        public int VacancyId { get; set; }
        
        public string Avatar { get; set; }
    }
}