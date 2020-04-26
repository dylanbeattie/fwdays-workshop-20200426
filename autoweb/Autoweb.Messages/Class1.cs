using System;

namespace Autoweb.Messages {
    public class NewCarMessage {
        public string RegistrationNumber { get; set;  }
        public string Make { get; set;  }
        public string Model { get; set;  }
        public string Color { get; set;  }
        public int Year { get; set;  }
        public DateTime NewCarAddedAtUtc { get; set;  }
        // TODO:
        // Which dealer added this car?
        // What IP address did this car get added from?
    }
}
