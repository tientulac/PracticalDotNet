using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PracticalDotNet.Models
{
    public class Classroom
    {
        [Key] // khoa chinh
        public int Id { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }
        public virtual ICollection<Exam> Exams { get; set; } // giong hasMany
    }
}