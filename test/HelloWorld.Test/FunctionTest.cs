using System.Collections.Generic;
using Newtonsoft.Json;
using Xunit;
using Amazon.Lambda.TestUtilities;
using Amazon.Lambda.APIGatewayEvents;
using Xunit.Abstractions;

namespace CoffeeShop.Tests
{
    public class FunctionTest
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public FunctionTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Should_return_all_list_of_coffees()
        {
            var context = new TestLambdaContext();
            var mockData = new CoffeeService();
            var function = new Function();
            var response = function.FunctionHandler(new APIGatewayProxyRequest {Body = "All"}, context);

            var expectedResponse = new APIGatewayProxyResponse
            {
                Body = JsonConvert.SerializeObject(mockData.GetAllAvailableCoffees()),
                StatusCode = 200,
                Headers = new Dictionary<string, string> {{"Content-Type", "application/json"}}
            };

            _testOutputHelper.WriteLine("Lambda Response: \n" + response.Body);
            _testOutputHelper.WriteLine("Expected Response: \n" + expectedResponse.Body);

            Assert.Equal(expectedResponse.Body, response.Body);
            Assert.Equal(expectedResponse.Headers, response.Headers);
            Assert.Equal(expectedResponse.StatusCode, response.StatusCode);
        }

        [Fact]
        public void Should_return_404_for_not_matching_input()
        {
            var context = new TestLambdaContext();
            var function = new Function();
            var response = function.FunctionHandler(new APIGatewayProxyRequest {Body = "TeaList"}, context);

            var expectedResponse = new APIGatewayProxyResponse
            {
                Body = JsonConvert.SerializeObject("Not found"),
                StatusCode = 404,
                Headers = new Dictionary<string, string> {{"Content-Type", "application/json"}}
            };

            _testOutputHelper.WriteLine("Lambda Response: \n" + response.Body);
            _testOutputHelper.WriteLine("Expected Response: \n" + expectedResponse.Body);

            Assert.Equal(expectedResponse.Body, response.Body);
            Assert.Equal(expectedResponse.Headers, response.Headers);
            Assert.Equal(expectedResponse.StatusCode, response.StatusCode);
        }

        [Fact]
        public void Should_return_500_for_null_input()
        {
            var context = new TestLambdaContext();
            var function = new Function();
            var response = function.FunctionHandler(new APIGatewayProxyRequest {Body = null}, context);

            var expectedResponse = new APIGatewayProxyResponse
            {
                Body = string.Empty,
                StatusCode = 500,
                Headers = new Dictionary<string, string> {{"Content-Type", "application/json"}}
            };

            Assert.Equal(expectedResponse.Headers, response.Headers);
            Assert.Equal(expectedResponse.StatusCode, response.StatusCode);
        }

        [Fact]
        public void Should_return_500_for_empty_input()
        {
            var context = new TestLambdaContext();
            var function = new Function();
            var response = function.FunctionHandler(new APIGatewayProxyRequest {Body = string.Empty}, context);

            var expectedResponse = new APIGatewayProxyResponse
            {
                Body = string.Empty,
                StatusCode = 500,
                Headers = new Dictionary<string, string> {{"Content-Type", "application/json"}}
            };

            Assert.Equal(expectedResponse.Headers, response.Headers);
            Assert.Equal(expectedResponse.StatusCode, response.StatusCode);
        }
    }
}