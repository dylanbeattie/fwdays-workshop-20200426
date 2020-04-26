using System.Collections.Generic;

namespace Autoweb.EmailRenderer {
    public class CustomerDatabase {
        public static IEnumerable<Customer> AllCustomers {
            get {
                for (var i = 0; i < 1000; i++) {
                    yield return new Customer() {
                        Email = $"customer{i}@example.com",
                        Name = $"Customer {i}",
                        MinimumYear = 1980 + (i % 40)
                    };
                }
            }
        }
    }
}