using System.Net;
using System.Text;
using System.Text.Json;
using Domain;

namespace API.Extension
{
    public class RequestResponseLoggingMiddleware
    { private readonly RequestDelegate _next;
       
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;
        public RequestResponseLoggingMiddleware(RequestDelegate next, 
                                                ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try {
                Dictionary<string, object> scope = new();
                //First, get the incoming request
                var request = await FormatRequest(context.Request);
                context.Request.Body.Position = 0;
                //Copy a pointer to the original response body stream
                var originalBodyStream = context.Response.Body;

                //Create a new memory stream...
                using var responseBody = new MemoryStream();
                //...and use that for the temporary response body
                context.Response.Body = responseBody;

                scope.TryAdd("request_headers", context.Request.Headers);
                if (originalBodyStream != null)
                {
                    scope.Add("request_body", request);
                }
                //Continue down the Middleware pipeline, eventually returning to this class
                await _next(context);

                //Format the response from the server
                var response = await FormatResponse(context.Response);
                scope.TryAdd("response_headers", context.Response.Headers);
                if (response != null)
                {
                    scope.Add("response_body", response);
                }
                var existeResponseTime = context.Response.Headers.TryGetValue("X-Response-Time-ms", out Microsoft.Extensions.Primitives.StringValues responseTime);
                if (existeResponseTime)
                {
                    scope.TryAdd("Response_Time_ms", responseTime.FirstOrDefault());
                }
                using (_logger.BeginScope(scope))
                {
                    _logger.LogInformation("[TRACE] request/response");
                    _logger.LogInformation("[ALL TRACE IN DICTIONARY] {@myDictionary}", scope);

                }
                //TODO: Save log to chosen datastore

                //Copy the contents of the new memory stream (which contains the response) to the original stream, which is then returned to the client.
                await responseBody.CopyToAsync(originalBodyStream);
            }
            catch (Exception e) {
                await HandleExceptionAsync(context, e);
            }
        }
        private static async Task HandleExceptionAsync(HttpContext context,Exception exception)
        {
           var statusCode = (int)HttpStatusCode.InternalServerError;

            if (exception is UnauthorizedAccessException)
            {
                statusCode = (int)HttpStatusCode.Unauthorized;
            }
            else if (exception is not null)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
           
            var json  = System.Text.Json.JsonSerializer.Serialize(new BasicResponse <string?>(statusCode),options);

            await context.Response.WriteAsync(json);
        }
        private static async Task<string> FormatRequest(HttpRequest request)
        {
            var body = request.Body;

            //This line allows us to set the reader for the request back at the beginning of its stream.
            request.EnableBuffering();

            //We now need to read the request stream.  First, we create a new byte[] with the same length as the request stream...
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];

            //...Then we copy the entire request stream into the new buffer.
            _ = await request.Body.ReadAsync(buffer, 0, buffer.Length);

            //We convert the byte[] into a string using UTF8 encoding...
            var bodyAsText = Encoding.UTF8.GetString(buffer);

            //..and finally, assign the read body back to the request body, which is allowed because of EnableRewind()
            request.Body = body;

            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }

        private static async Task<string> FormatResponse(HttpResponse response)
        {
            //We need to read the response stream from the beginning...
            response.Body.Seek(0, SeekOrigin.Begin);

            //...and copy it into a string
            string text = await new StreamReader(response.Body).ReadToEndAsync();

            //We need to reset the reader for the response so that the client can read it.
            response.Body.Seek(0, SeekOrigin.Begin);

            //Return the string for the response, including the status code (e.g. 200, 404, 401, etc.)
            return $"{response.StatusCode}: {text}";
        }
      
    }
}