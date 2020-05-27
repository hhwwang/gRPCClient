using Grpc.Net.Client;

using System;

namespace gRPCClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // This switch must be set before creating the GrpcChannel/HttpClient.
            AppContext.SetSwitch(
                "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            var channel = GrpcChannel.ForAddress("http://10.0.4.134:5000");
            var client = new ShoppingCart.ShoppingCartClient(channel); // Customer.CustomerClient(channel);

            //var customerRequested = new IsProductAddressUpdateAllowedRequest
            //{
            //    StateFrom = "ON",
            //    StateTo = "SC",
            //    Userrelationid = 12345
            //};

            var request = new InsuranceTrialLinkGetRequest
            {
                State = "SC",
                Server = 1
            };

            var reply = client.InsuranceTrialLinkGet(request);

            Console.WriteLine(reply.TrialURL.ToString());
            Console.ReadLine();
        }
    }
}
