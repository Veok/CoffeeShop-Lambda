using System.Collections.Generic;
using Newtonsoft.Json;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Microsoft.Extensions.DependencyInjection;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace CoffeeShop
{
    public class Function
    {
        private ICoffeeService _coffeeService;

        private void InitializeFunctionConfiguration()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<ICoffeeService, CoffeeService>();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            _coffeeService = serviceProvider.GetService<ICoffeeService>();
        }

        public APIGatewayProxyResponse FunctionHandler(APIGatewayProxyRequest input, ILambdaContext context)
        {
            if (string.IsNullOrEmpty(input.Body))
            {
                var logMessage = $"Tried to invoke lambda with empty input. Request Id: {context.AwsRequestId}";
                LambdaLogger.Log(logMessage);
                return FormatResponse(logMessage, 500);
            }

            InitializeFunctionConfiguration();

            return input.Body switch
            {
                "All" => FormatResponse(_coffeeService.GetAllAvailableCoffees(), 200),
                _ => FormatResponse("Not found", 404)
            };
        }

        private APIGatewayProxyResponse FormatResponse(object body, int statusCode)
        {
            return new APIGatewayProxyResponse
            {
                Body = JsonConvert.SerializeObject(body),
                StatusCode = statusCode,
                Headers = new Dictionary<string, string> {{"Content-Type", "application/json"}}
            };
        }
    }
}

