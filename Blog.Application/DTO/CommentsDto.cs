﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.DTO
{
    public class CommentsDto
    {
        public int AuthorId { get; set; }
        public string Text { get; set; }
        public int? ParentId { get; set; }
    }
}
