﻿using System;
using System.Collections.Generic;

namespace HseAr.Data.DataProjections
{
    public class Building
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }
        
        public string Address { get; set; }
        
        public string Coordinate { get; set; }
        
        public List<Floor> Floors { get; set; } =new List<Floor>();
    }
}