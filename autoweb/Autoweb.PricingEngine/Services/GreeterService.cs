using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Autoweb.Pricing;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Autoweb.PricingEngine {
    public class PricingService : Pricing.PricingEngine.PricingEngineBase {
        private readonly ILogger<PricingService> _logger;
        public PricingService(ILogger<PricingService> logger) {
            _logger = logger;
        }

        public override Task<PriceResponse> GetPrice(PriceRequest request, ServerCallContext context) {
            var price = GetPrice(request.Make, request.Model, request.Year);
            return Task.FromResult(new PriceResponse {
                Price = price.Item1,
                Currency = price.Item2
            });
        }

        private Tuple<Int32, string> GetPrice(string make, string model, int year) {
            if (make == "Ferrari") return new Tuple<int, string>(80000, "EUR");
            if (make == "Land Rover") return new Tuple<int, string>(20000, "GBP");
            var now = DateTime.Now.Year;
            var age = now - year;
            if (age < 5) return new Tuple<int, string>(350000, "UAH");
            return age < 10 ? new Tuple<int, string>(100000, "UAH") : new Tuple<int, string>(20000, "UAH");
        }
    }
}
