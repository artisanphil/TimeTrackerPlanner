﻿using System.ComponentModel.DataAnnotations;

namespace TimeTrackerPlanerMVC.Models
{
    public class Categories
    {
        [Key]
        public int catid { get; set; }
        public string catname { get; set; }
        public int projectid { get; set; }
    }
}