using SoccerHub.Api.DTOs;

namespace SoccerHub.Api.Helpers
{
    public class ApiResponseHelper
    {
        public static ApiResponse<T> Success<T>(T data, string message = "")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static ApiResponse<T> Fail<T>(string message)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Data = default
            };
        }
    }
}
