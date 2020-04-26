using System;
using EasyNetQ;
using Messages;

namespace Publisher {
    class Program {
        static void Main(string[] args) {

            var bus = RabbitHutch.CreateBus("amqp://lplnqgia:AGXzy06s5Pt7Z9zgPJshyNofOqRES5-h@roedeer.rmq.cloudamqp.com/lplnqgia");
            Console.WriteLine("Connected! Ready to send messages!");
            Console.Write("Enter your name:" );
            var name = Console.ReadLine();
            while (true) {
                Console.Write("Enter a message: ");

                var messageContent = Console.ReadLine();
                var message = new Greeting {
                    Sender = name,
                    Content = messageContent
                };
                bus.Publish(message);
            }
        }
    }
}
