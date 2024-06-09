using System.Net;

namespace Kitchen_Appliances_Backend.Commons.Middleware
{
	public class UnauthorizedMiddleware
	{
		private readonly RequestDelegate _next;
		public UnauthorizedMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			await _next(context);

			if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
			{
				context.Response.ContentType = "application/json";
				var result = System.Text.Json.JsonSerializer.Serialize(new { message = "Unauthorized access" });
				await context.Response.WriteAsync(result);
			}
			else if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
			{
				context.Response.ContentType = "application/json";
				var result = System.Text.Json.JsonSerializer.Serialize(new { message = "Forbidden access" });
				await context.Response.WriteAsync(result);
			}
		}
	}
}
