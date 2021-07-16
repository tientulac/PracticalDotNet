using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PracticalDotNet.Models
{
    public class Exam
    {
        [Key] // khoa chinh
        public int Id { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Start_time { get; set; }
        [Required(ErrorMessage = "Required")]
        public DateTime Exam_date { get; set; }
        [Required(ErrorMessage = "Required")]
        public double Exam_duration { get; set; }
        [Required(ErrorMessage = "Required")]

        public string Status { get; set; }
        [Required(ErrorMessage = "Required")]

        public int ClassroomID { get; set; } // khoa ngoai
        public int FacultyID { get; set; }

        public virtual Classroom Classroom { get; set; }
        public virtual Faculty Faculty {get;set;}
    }
}