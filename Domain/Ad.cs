using System;
using System.Collections.Generic;

namespace Domain
{
    public class Ad
    {
        public Ad() { }
        public Ad(string name,
                 int price,
                 string description,
                 Address address,
                 Category category,
                 AdStatus adStatus)
        {
            this.Name = name;
            this.Price = price;
            this.Description = description;
            this.Address = address;
            this.Category = category;
            this.AdStatus = adStatus;
            this.AdState = AdState.Pending;
            this.CreatedAt = DateTime.UtcNow;
            this.Images = new List<Image>();
        }

        public Ad(string name,
                int price,
                string description,
                Address address,
                Category category,
                AdStatus adStatus,
                List<Image> images) : this(name, price, description, address, category, adStatus)
        {
            this.Images = images;
        }

        public int Id { get; private set; }

        private string _name;
        public string Name
        {
            get { return _name; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Name Is Null!");

                if (value.Length <= 2 || value.Length > 100)
                    throw new ArgumentOutOfRangeException("Name Length Should Between [2-100]");

                _name = value;
            }
        }

        private int _price;
        public int Price
        {
            get { return _price; }
            private set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("Price Is Invalid!");

                _price = value;
            }
        }

        public virtual AdState AdState { get; private set; }

        public virtual DateTime CreatedAt { get; private set; }

        private string _description;
        public string Description
        {
            get { return _description; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Description Is Null!");

                if (value.Length <= 3 || value.Length > 250)
                    throw new ArgumentOutOfRangeException("Description Length Should Between [3-250]");

                _description = value;
            }
        }

        public virtual List<Image> Images { get; private set; }

        private Category _category;
        public virtual Category Category
        {
            get { return _category; }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("Category Is Null!");

                _category = value;
            }
        }

        private Address _address;
        public virtual Address Address
        {
            get { return _address; }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("Address Is Null!");

                _address = value;
            }
        }

        public virtual AdStatus AdStatus { get; private set; }

        //Functions

        public void ChangeName(string name)
        {
            this.Name = name;
        }

        public void ChangePrice(int price)
        {
            this.Price = price;
        }

        public void ChangeDescription(string description)
        {
            this.Description = description;
        }

        public void AddImage(Image image)
        {
            if (image == null)
                throw new ArgumentNullException("Image Is Null!");

            this.Images.Add(image);
        }

        public void RemoveImage(Image image)
        {
            if (image == null)
                throw new ArgumentNullException("Image Is Null!");

            this.Images.Remove(image);
        }

        public void ChangeAddress(Address address)
        {
            this.Address = address;
        }

        public void Accept()
        {
            this.AdState = AdState.Accepted;
        }

        public void Reject()
        {
            this.AdState = AdState.Rejected;
        }

        public void Delete()
        {
            this.AdState = AdState.Deleted;
        }
    }
}
