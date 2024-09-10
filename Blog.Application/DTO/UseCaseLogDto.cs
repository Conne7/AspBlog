using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.DTO
{
    public class UseCaseLogDto
    {
        public int LogId { get; set; }
        public string UseCaseName { get; set; }
        public string Username { get; set; }
        public DateTime ExecutedAt { get; set; }
    }
}
