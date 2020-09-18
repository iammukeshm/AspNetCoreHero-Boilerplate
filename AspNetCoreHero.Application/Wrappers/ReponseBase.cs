using System.Collections.Generic;

namespace AspNetCoreHero.Application.Wrappers
{
    public class ResponseBase<T>
    {
        public ResponseBase()
        {
        }
        public ResponseBase(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }
        public ResponseBase(string message)
        {
            Succeeded = false;
            Message = message;
        }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }
    }
}
