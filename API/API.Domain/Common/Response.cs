using System.Collections.Generic;

namespace API.Domain.Common
{
    public class Response<T>
    {
        public T Data { get; }

        public bool IsValid { get; set; }

        public IEnumerable<string> Errors { get; }

        private Response(T data)
        {
            Data = data;
            IsValid = true;
            Errors = new List<string>();
        }

        private Response(IEnumerable<string> errors)
        {
            IsValid = false;
            Errors = errors;
        }

        public static Response<T> Valid(T data)
        {
            return new Response<T>(data);
        }
        
        public static Response<T> Invalid(IEnumerable<string> errors)
        {
            return new Response<T>(errors);
        }
    }
}
