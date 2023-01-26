using FluentAssertions;
using Xunit;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainTests
{
    public class CategoryTests
    {
        [Fact]
        public void CategoryConstructor_WithoutInvalidValues_ShouldFillProperties()
        {
            //Arrange
            Category parentCategory = new Category("Things");

            //Act
            Category category = new Category("Lavazem",parentCategory);

            //Assert
            category.Title.Should().NotBeNull();
        }

        [Fact]
        public void CategoryConstructor_WithInvalidValues_ShouldThrowException()
        {
            //Arrange
            Category parentCategory = new Category("Things");

            //Act
            Action action = () => new Category("L", parentCategory);

            //Assert
            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void ChangeTitle_WithoutInvalidValues_ShouldChangeTitle()
        {
            //Arrange
            Category parentCategory = new Category("Things");
            Category category = new Category("Lavazem", parentCategory);

            //Act
            category.ChangeTitle("Lavazem Bargy");

            //Assert
            category.Title.Should().Be("Lavazem Bargy");
        }

        [Fact]
        public void AddSubCategory_WithoutInvalidValues_ShouldAddSubCategory()
        {
            //Arrange
            Category parentCategory = new Category("Things");
            Category category = new Category("Lavazem", parentCategory);

            //Act
            category.AddSubCategory(new Category("Sub Category"));

            //Assert
            category.SubCategories[0].Should().NotBeNull();
        }
    }
}
