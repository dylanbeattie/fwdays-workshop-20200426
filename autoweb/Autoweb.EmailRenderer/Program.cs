using System;
using System.Collections;
using System.ComponentModel.Design;
using Autoweb.Messages;
using EasyNetQ;

namespace Autoweb.EmailRenderer {
    class Program {
        private const string AMQP =
            "amqp://lplnqgia:AGXzy06s5Pt7Z9zgPJshyNofOqRES5-h@roedeer.rmq.cloudamqp.com/lplnqgia";

        private const string AMQP_SUBSCRIBER_ID = "autoweb.emailrenderer";
        
        private static IBus bus = RabbitHutch.CreateBus(AMQP);
        
        static void Main(string[] args) {
            bus.Subscribe<NewCarMessage>(AMQP_SUBSCRIBER_ID, SendEmails);
        }

        public static void SendEmails(NewCarMessage newCar) {
            foreach (var customer in CustomerDatabase.AllCustomers) {
                if (customer.WantsEmailAbout(newCar)) {
                    QueuePersonalizedEmail(customer, newCar);
                    Console.WriteLine($@"Sending {customer.Email} an email about {newCar.RegistrationNumber}");
                }
                else {
                    Console.WriteLine($@"SKIPPING {customer.Email} about {newCar.RegistrationNumber}");
                }
            }
        }

        public static void QueuePersonalizedEmail(Customer customer, NewCarMessage newCar) {
            var email = new NewCarEmailMessage() {
                To = customer.Email,
                Subject = $"NEW CAR ALERT: {newCar.Year} {newCar.Make} {newCar.Model}",
                Body = @$"
Dear {customer.Name},

We thought you'd like to know about this new car that's just been listed for sale!

Make: {newCar.Make}
Model: {newCar.Model}

Thanks,

AutoWeb!
"
            };
            bus.Publish(email);
        }
    }
}
