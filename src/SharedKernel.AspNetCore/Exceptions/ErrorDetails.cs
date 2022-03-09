using Newtonsoft.Json;

namespace SharedKernel.AspNetCore.Exceptions
{
    internal class ErrorDetails
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }

        public string TraceId { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}