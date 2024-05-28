namespace Kitchen_Appliances_Backend.Commons.Responses
{
    public class ApiResponse<T>
    {
        public int Status { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }

        public ApiResponse(int status , string message, T data)
        {
            Status = status;
            Message = message;
            Data = data;
        }

        public static ApiResponse<T> Initialize(int status, string message, T data)
        {
            return new ApiResponse<T>(status, message, data);
        }
    }
}
