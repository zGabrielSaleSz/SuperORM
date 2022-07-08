using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperORM.Core.Test.Complement.Model
{
    public class NullTest
    {
        public int id { get; set; }
        public string? name { get; set; }
        public int? phoneNumber { get; set; }
        public DateTime? expirationDate { get; set; }
        public bool? inactive { get; set; }
    }
}
