﻿using System.Text.Json.Serialization;

namespace RESTArchitecture.Common.Models
{
    public class Category
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public List<int>? ItemsIds { get; set; }
        public List<Item>? Items { get; set; } = null;
    }
}