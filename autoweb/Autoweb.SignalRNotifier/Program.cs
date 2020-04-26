using System;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Autoweb.Messages;
using Autoweb.Pricing;
using EasyNetQ;
using Grpc.Net.Client;
using Microsoft.AspNetCore.SignalR.Client;

namespace Autoweb.SignalRNotifier {
    class Program {

        private const string AMQP =
            "amqp://lplnqgia:AGXzy06s5Pt7Z9zgPJshyNofOqRES5-h@roedeer.rmq.cloudamqp.com/lplnqgia";

        private const string AMQP_SUBSCRIBER_ID = "autoweb.signalr_notifier";
        private const string SIGNALR_HUB_URL = "https://workshop.ursatile.com:5006/chat";

        private static readonly IBus bus = RabbitHutch.CreateBus(AMQP);
        private static PricingEngine.PricingEngineClient grpc;

        private static readonly HubConnection hub = new HubConnectionBuilder()
            .WithUrl(SIGNALR_HUB_URL)
            .Build();

        static async Task Main(string[] args) {
            Console.WriteLine("Connecting to SignalR...");
            await hub.StartAsync();
            Console.WriteLine("SignalR connected!");
            Console.WriteLine("Connecting to gRPC");
            var channel = GrpcChannel.ForAddress("https://workshop.ursatile.com:5003");
            grpc = new Pricing.PricingEngine.PricingEngineClient(channel);


            bus.Subscribe<NewCarMessage>(AMQP_SUBSCRIBER_ID, NotifySignalR);
        }

        private static async void NotifySignalR(NewCarMessage newCar) {
            try {
                var req = new PriceRequest {
                    Make = newCar.Make,
                    Model = newCar.Model,
                    Year = newCar.Year
                };
                var price = await grpc.GetPriceAsync(req);
                await hub.SendAsync("SendMessage",
                    "Autoweb",
                    $"NEW CAR! {newCar.Make} {newCar.Model} {newCar.Year} ({price.Price} {price.Currency}"
                );
            }
            catch (Exception ex) {
                await hub.SendAsync("SendMessage",
                    "Autoweb",
                    $"ERROR! {ex.Message}"
                );
            }
        }
    }
}
