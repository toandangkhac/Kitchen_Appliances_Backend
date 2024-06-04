namespace Kitchen_Appliances_Backend.Commons.Responses
{
    public class APIResponse<T>
    {
        public int Status { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }

        public APIResponse(int status , string message, T data)
        {
            Status = status;
            Message = message;
            Data = data;
        }

        public static APIResponse<T> Initialize(int status, string message, T data)
        {
            return new APIResponse<T>(status, message, data);
        }
    }
}
