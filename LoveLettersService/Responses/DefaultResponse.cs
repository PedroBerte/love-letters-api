using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveLetters.Service.Responses
{
    public class DefaultResponse<T>
    {
        public string Message { get; set; }
        public List<string>? Errors { get; set; }
        public bool Success { get; set; }
        public T? Data { get; set; }
        public int Code { get; set; }
    }
}
