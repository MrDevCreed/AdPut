using System;
using System.Collections.Generic;

namespace Domain
{
    public class User
    {
        public User() { }
        public User(string userId, string name)
        {
            this.UserId = userId;
            this.Name = name;
            this.Ads = new List<Ad>();
        }

        public int Id { get; private set; }

        private string _userId;
        public string UserId
        {
            get { return _userId; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("User Id Is Null");

                _userId = value;
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Name Is Null!");

                if (value.Length <= 3 || value.Length > 50)
                    throw new ArgumentOutOfRangeException("Name Length Should Between [3-50]");

                _name = value;
            }
        }

        public virtual List<Ad> Ads { get; private set; }

        //Functions

        public void ChangeName(string name)
        {
            this.Name = name;
        }
    }
}
