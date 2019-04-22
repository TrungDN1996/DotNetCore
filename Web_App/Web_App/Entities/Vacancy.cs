using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_App.Entities
{
    public class Vacancy
    {
         [Key]
         [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
         public int Id { get; set; }
        
         public string Name { get; set; }
        
         public ICollection<Candidate> Candidate { get; set; }
    }
}