using System;
using EasyNetQ;
using Messages;

namespace Subscriber {
    class Program {
        private static string amqp =
            "amqp://lplnqgia:AGXzy06s5Pt7Z9zgPJshyNofOqRES5-h@roedeer.rmq.cloudamqp.com/lplnqgia";
        static void Main(string[] args) {
            var bus = RabbitHutch.CreateBus(amqp);
            bus.Subscribe<Greeting>(Guid.NewGuid().ToString(), HandleGreeting);
        }

        private static void HandleGreeting(Greeting g) {
            Console.WriteLine($"{g.Sender} : {g.Content}");
        }
    }
}
