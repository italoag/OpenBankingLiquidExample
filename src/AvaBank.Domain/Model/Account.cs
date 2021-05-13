using System;
using System.Collections.Generic;

namespace AvaBank.Domain.Model
{
    public partial class Account
    {
        public string User { get; set; }

        public string Product { get; set;  }

        public IEnumerable <Data> Data { get; set; }

        public IEnumerable<Link> Links { get; set; }

        public IEnumerable<Meta> Meta { get; set; }
    }
}