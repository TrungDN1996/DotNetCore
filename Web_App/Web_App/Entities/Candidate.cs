using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Web_App.Entities
{
    public class Candidate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Phone { get; set; }
        
        public string Avatar { get; set; }
        
        public int VacancyID { get; set; }
        
        public Vacancy Vacancy { get; set; }
    }
}