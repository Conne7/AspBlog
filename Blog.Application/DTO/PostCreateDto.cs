﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.DTO
{
    public class PostCreateDto
    {
        public string? Type { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public List<string>? Tags { get; set; }
    }
}
