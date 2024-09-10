using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain
{
    public class UseCaseLog
    {
        public int LogId { get; set; }
        public string UseCaseName { get; set; }
        public string Username { get; set; }
        public string Data { get; set; }
        public DateTime ExecutedAt { get; set; }
    }
}
