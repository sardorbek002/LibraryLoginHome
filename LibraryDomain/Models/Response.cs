using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Models
{
    public class Response<T>
    {
        public Response(string error,int statusCode=400)
        {
            StatusCode = statusCode;
            Error = error;
        }
        public Response(T result)
        {
            Result = result;
            Error = null;
        }
        public int StatusCode { get ; set; }
        public string Error { get; set; }
        public T Result { get; set; }

    }
}
