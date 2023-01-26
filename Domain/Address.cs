using System;

namespace Domain
{
    public class Address
    {
        public Address() { }
        public Address(City city, Town town)
        {
            this.City = city;
            this.Town = town;
        }

        public int Id { get; private set; }

        private City _city;
        public virtual City City
        {
            get { return _city; }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("City Is Null!");

                _city = value;
            }
        }

        private Town _town;
        public virtual Town Town
        {
            get { return _town; }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("Town Is Null!");

                _town = value;
            }
        }

        public int AdId { get; private set; }
        public virtual Ad Ad { get; private set; }

        //Functions

        public void ChangeCity(City city)
        {
            this.City = city;
        }

        public void ChangeTown(Town town)
        {
            this.Town = town;
        }
    }
}
