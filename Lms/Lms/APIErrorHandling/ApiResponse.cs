using Newtonsoft.Json;

namespace Lms.APIErrorHandling
{
    public class ApiResponse
    {
        public int StatusCode { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; }

        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private static string GetDefaultMessageForStatusCode(int statusCode)
        {
            switch (statusCode)
            {
                case 200:
                    return "Results were a success.";
                case 404:
                    return "Information not found";
                case 500:
                    return "An unhandled error occurred";
                default:
                    return null;
            }
        }
    }
}