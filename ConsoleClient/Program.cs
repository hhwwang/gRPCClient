using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // initialization, better done elsewhere
            var channel = new Grpc.Core.Channel("localhost", 5000, ChannelCredentials.Insecure);

            //// create client
            //var customerClient = new Customer.CustomerClient(channel);
            //var customerRequested = new IsProductAddressUpdateAllowedRequest
            //{
            //    StateFrom = "ON",
            //    StateTo = "OW",
            //    Userrelationid = 1234
            //};

            //var response = customerClient.IsProductAddressUpdateAllowed(customerRequested);

            //Console.WriteLine(response.IsAddressUpdatable.ToString());
            //Console.WriteLine(response.ReturnMessage);

            var client = new ShoppingCart.ShoppingCartClient(channel); // Customer.CustomerClient(channel);

            var request = new InsuranceTrialLinkGetRequest
            {
                State = "SC",
                Server = 1
            };

            var reply = client.InsuranceTrialLinkGet(request);

            Console.WriteLine(reply.TrialURL.ToString());
            Console.ReadLine();

            Console.ReadKey();
        }
    }
}
