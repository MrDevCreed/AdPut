using System;

namespace Domain
{
    public class Town
    {
        public Town() { }
        public Town(string name)
        {
            this.Name = name;
        }

        public int Id { get; private set; }

        private string _name;
        public string Name
        {
            get { return _name; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Town Name Is Null!");

                _name = value;
            }
        }

        public virtual City City { get; private set; }

        //Functions

        public void ChangeName(string name)
        {
            this.Name = name;
        }
    }
}
