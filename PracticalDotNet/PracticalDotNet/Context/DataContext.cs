using PracticalDotNet.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PracticalDotNet.Context
{
    public class DataContext : DbContext
    {
        public DataContext() : base("Practical")
        {
        }

        public DbSet<Classroom> Classrooms { get; set; }

        public DbSet<Exam> Exams { get; set; }
        public System.Data.Entity.DbSet<PracticalDotNet.Models.Faculty> Faculties { get; set; }
    }
}