using AutoServices.Core.Dto;
using AutoServices.Core.ServiceInterface;

namespace AutoServices.CarTest
{
    public class CarTest : TestBase
    {
        [Fact]
        public async Task ShouldNot_AddEmptyCar_WhenReturnResult()
        {
            //Arrange
            CarDto dto = new();
            dto.Make = "Audi";
            dto.Model = "A4";
            dto.Year = 1998;
            dto.CreatedAt = DateTime.Now;
            dto.ModifiedAt = DateTime.Now;
            //Act
            var result = await Svc<ICarsServices>().Create(dto);
            //Assert
            Assert.NotNull(result);
        }
    }
}