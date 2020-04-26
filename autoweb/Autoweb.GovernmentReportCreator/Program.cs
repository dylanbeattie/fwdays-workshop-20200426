using System;
using System.IO;
using Autoweb.Messages;
using EasyNetQ;

namespace Autoweb.GovernmentReportCreator {
    class Program {
        private const string AMQP =
            "amqp://lplnqgia:AGXzy06s5Pt7Z9zgPJshyNofOqRES5-h@roedeer.rmq.cloudamqp.com/lplnqgia";

        private const string AMQP_SUBSCRIBER_ID = "autoweb.governmentreportcreator";
        static void Main(string[] args) {
            var bus = RabbitHutch.CreateBus(AMQP);
            bus.Subscribe<NewCarMessage>(AMQP_SUBSCRIBER_ID, LogNewCar);
        }

        private static void LogNewCar(NewCarMessage message) {
            var log = $"{message.NewCarAddedAtUtc:O},{message.RegistrationNumber},{message.Make},{message.Model},{message.Color},{message.Year}{Environment.NewLine}";
            Console.WriteLine(log);
            File.AppendAllText("new_cars.log", log);
        }
    }
}
