using System;
using System.Net.Mail;
using Autoweb.Messages;
using EasyNetQ;

namespace Autoweb.EmailSender {
    class Program {
        private const string AMQP =
            "amqp://lplnqgia:AGXzy06s5Pt7Z9zgPJshyNofOqRES5-h@roedeer.rmq.cloudamqp.com/lplnqgia";

        private const string AMQP_SUBSCRIBER_ID = "autoweb.emailsender";

        private static IBus bus = RabbitHutch.CreateBus(AMQP);

        static void Main(string[] args) {
            bus.Subscribe<NewCarEmailMessage>(AMQP_SUBSCRIBER_ID, SendCustomerEmail);
        }

        private static void SendCustomerEmail(NewCarEmailMessage email) {
            //TODO: replace this with real email!
            Console.WriteLine("===================================");
            Console.WriteLine("From: alerts@autoweb.com");
            Console.WriteLine($"To: {email.To}");
            Console.WriteLine($"Subject: {email.Subject}");
            Console.WriteLine("");
            Console.WriteLine(email.Body);
            Console.WriteLine("===================================");
        }
    }
}
