﻿namespace EHR.Models
{
    public class Disease
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool Active { get; set; }
    }
}
