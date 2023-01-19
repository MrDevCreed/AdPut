using System;

namespace Domain
{
    public class Image
    {
        public Image(string imagePath)
        {
            this.ImagePath = imagePath;
        }

        public int Id { get; private set; }

        private string _imagePath;

        public string ImagePath
        {
            get { return _imagePath; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Image Path Is Null!");

                _imagePath = value;
            }
        }

        public Ad Ad { get; private set; }

        //Functions

        public void ChangeImagePath(string imagePath)
        {
            this.ImagePath = imagePath;
        }
    }
}
