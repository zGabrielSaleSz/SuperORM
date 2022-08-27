using System;

namespace SuperORM.Core.Test.Complement.Model
{
    public class User
    {
        public User()
        {

        }

        public int id { get; set; }
        public string Name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public bool active { get; set; }
        public double? height { get; set; }
        public DateTime approvedDate { get; set; }
    }
}
