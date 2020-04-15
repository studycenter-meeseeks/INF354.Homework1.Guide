﻿using System.Collections.Generic;

namespace INF354.Homework1.Guide.Models
{
    public class Student
    {
        public Student()
        {
            //Always initialize your lists
            Modules = new List<Module>();
        }
        public int Id { get; set; } //Auto generated by db
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual List<Module> Modules { get; set; }

      
    }
}