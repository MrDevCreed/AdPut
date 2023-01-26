using System;
using System.Collections.Generic;

namespace Domain
{
    public class City
    {
        public City() { }
        public City(string name)
        {
            this.Name = name;
            this.Towns = new List<Town>();
            this.Addresses = new List<Address>();
        }

        public int Id { get; private set; }

        private string _name;
        public string Name
        {
            get { return _name; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("City Name Is Null!");

                _name = value;
            }
        }

        public virtual List<Town> Towns { get; private set; }

        public virtual List<Address> Addresses { get; private set; }

        //Functions

        public void ChangeName(string name)
        {
            this.Name = name;
        }

        public void AddTown(Town town)
        {
            if (town == null)
                throw new ArgumentNullException("Town Is Null!");

            this.Towns.Add(town);
        }

        public void RemoveTown(Town town)
        {
            if (town == null)
                throw new ArgumentNullException("Town Is Null!");

            this.Towns.Remove(town);
        }
    }
}
