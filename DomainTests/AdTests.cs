using Domain;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace DomainTests
{
    public class AdTests
    {
        [Fact]
        public void AdConstructor_WithoutNullValues_ShouldFillProperties()
        {
            //Arrange
            City city = new City("Karaj");
            Town town = new Town("Hesarak");
            Address address = new Address(city,town);
            Category category = new Category("Lavazem");

            //Act
            Ad ad = new Ad("Mobl",10000,"This is very nice mobl",address,category,AdStatus.Worked);

            //Assert
            ad.Address.Should().NotBeNull();
        }

        [Fact]
        public void AdConstructor_WithImages_ShouldFillProperties()
        {
            //Arrange
            City city = new City("Karaj");
            Town town = new Town("Hesarak");
            Address address = new Address(city, town);
            Category category = new Category("Lavazem");
            List<Image> images = new List<Image>()
            {
                new Image("iufrh8wh8uhei"),
            };

            //Act
            Ad ad = new Ad("Mobl", 10000, "This is very nice mobl", address, category, AdStatus.Worked,images);

            //Assert
            ad.Images.Should().NotBeNull();
        }

        [Fact]
        public void AdConstructor_WithInvalidValues_ShouldThrowException()
        {
            //Arrange
            City city = new City("Karaj");
            Town town = new Town("Hesarak");
            Category category = new Category("Lavazem");

            //Act
            Action action = () => new Ad("Mobl",0, "Th",null, category, AdStatus.Worked);

            //Assert
            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void ChangeName_WithoutNullValues_ShouldChangeName()
        {
            //Arrange
            City city = new City("Karaj");
            Town town = new Town("Hesarak");
            Address address = new Address(city, town);
            Category category = new Category("Lavazem");
            Ad ad = new Ad("Mobl", 10000, "This is very nice mobl", address, category, AdStatus.Worked);

            //Act
            ad.ChangeName("Mobleman");

            //Assert
            ad.Name.Should().Be("Mobleman");
        }

        [Fact]
        public void AddImage_WithoutInvalidValues_ShouldAddImage()
        {
            //Arrange
            City city = new City("Karaj");
            Town town = new Town("Hesarak");
            Address address = new Address(city, town);
            Category category = new Category("Lavazem");
            Ad ad = new Ad("Mobl", 10000, "This is very nice mobl", address, category, AdStatus.Worked);

            //Act
            ad.AddImage(new Image("difhuehf9iufs"));

            //Assert
            ad.Images[0].ImagePath.Should().Be("difhuehf9iufs");
        }

        [Fact]
        public void Accept_WithoutInvalidValues_ShouldAcceptAd()
        {
            //Arrange
            City city = new City("Karaj");
            Town town = new Town("Hesarak");
            Address address = new Address(city, town);
            Category category = new Category("Lavazem");
            Ad ad = new Ad("Mobl", 10000, "This is very nice mobl", address, category, AdStatus.Worked);

            //Act
            ad.Accept();

            //Assert
            ad.AdState.Should().Be(AdState.Accepted);
        }
    }
}
