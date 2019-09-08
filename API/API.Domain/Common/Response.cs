using System.Collections.Generic;

namespace API.Domain.Common
{
    public class Response<T>
    {
        public T Data { get; }

        public bool IsValid { get; set; }

        public List<string> Errors { get; }

        public Response()
        {
            Errors = new List<string>();
            IsValid = true;
        }

        public Response(T data) : this()
        {
            Data = data;
        }
    }
}
