using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.Lambda.RuntimeSupport;
using Amazon.Lambda.Serialization.SystemTextJson;
using Autofac.Extensions.DependencyInjection;

namespace backend.recibos;

/// <summary>
/// The Main function can be used to run the ASP.NET Core application locally using the Kestrel webserver.
/// </summary>
public class LocalEntryPoint
{
    public static void Main(string[] args)
    {
        if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("AWS_LAMBDA_FUNCTION_NAME")))
        {
            CreateHostBuilder(args).Build().Run();
        }
        else
        {

            // Startup Lambda .NET runtime
            var lambdaEntry = new LambdaEntryPoint();
            var functionHandler = (Func<APIGatewayProxyRequest, ILambdaContext, Task<APIGatewayProxyResponse>>)(lambdaEntry.FunctionHandlerAsync);
            using var handlerWrapper = HandlerWrapper.GetHandlerWrapper(functionHandler, new DefaultLambdaJsonSerializer());
            using (var bootstrap = new LambdaBootstrap(handlerWrapper))
            {
                bootstrap.RunAsync().Wait();
            }

        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}