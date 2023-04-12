
namespace Lms.APIErrorHandling
{
    public class ApiBadRequestResponse : ApiResponse
    {
        public object Errors { get; }

        public ApiBadRequestResponse(object errors)
            : base(400)
        {
            Errors = errors;
        }
    }
}
