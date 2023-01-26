using System;
using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repositories.implementations;
using Data.Database;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DataTests
{
    public class CategoryRepositoryTests
    {
        private readonly Context _context;
        public CategoryRepositoryTests(Context context)
        {
            this._context = context;
        }

        [Fact]
        public void Add_ShouldAddCategory()
        {
            //Arrange
            var context = new Mock<Context>();
            var categoryrep = new Mock<CategoryRepository>();

            //Act
            categoryrep.Setup(X => X.Add(new Domain.Category("Lavazem")));
            var result = categoryrep.Object;
            var o = result.FindById(1);

            //Assert
            o.Should().NotBeNull();
        }
    }
}
