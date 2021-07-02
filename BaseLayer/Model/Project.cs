﻿using System.Collections.Generic;

namespace BaseLayer.Model
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CustomField> CustomFields { get; set; }
    }
}