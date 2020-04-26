using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Autoweb.Pricing;
using Grpc.Net.Client;


namespace Autoweb.PricingEngineDemoClient {
    class Program {
        static async Task Main(string[] args) {
            var sw = new Stopwatch();
            const string host = "https://workshop.ursatile.com:5003";
            using var channel = GrpcChannel.ForAddress(host);
            var client = new Pricing.PricingEngine.PricingEngineClient(channel);
            var req = new PriceRequest {
                Make = "Ferrari",
                Model = "F40",
                Year = 1981
            };
            while (true) {
                sw.Stop();
                sw.Reset();
                sw.Start();
                var reply = await client.GetPriceAsync(req);
                sw.Stop();
                Console.Write(reply);
                Console.Write("gRPC call took {0}ms", sw.ElapsedMilliseconds);
                Console.ReadKey(false);
            }
        }
    }
}
