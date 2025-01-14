using AutoServices.ApplicationServices.Services;
using AutoServices.Core.Domain;
using AutoServices.Core.Dto;
using AutoServices.Core.ServiceInterface;
using AutoServices.Data;
using Microsoft.EntityFrameworkCore;

namespace AutoServices.CarTest
{
    public class CarTest : TestBase
    {
        private AutoServicesContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AutoServicesContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AutoServicesContext(options);
        }

        [Fact]
        public async Task Create_ShouldAddCarToDatabase()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var carService = new CarsServices(context, null);
            var carDto = new CarDto
            {
                Make = "Toyota",
                Model = "Corolla",
                Year = 2021
            };

            // Act
            var createdCar = await carService.Create(carDto);

            // Assert
            Assert.NotNull(createdCar);
            Assert.Equal("Toyota", createdCar.Make);
            Assert.Equal("Corolla", createdCar.Model);
            Assert.Equal(2021, createdCar.Year);
            Assert.Equal(1, context.Cars.Count());
        }

        [Fact]
        public async Task Should_AddCar_WhenValidDto()
        {
            // Arrange
            CarDto dto = new()
            {
                Make = "Audi",
                Model = "A4",
                Year = 1998,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };

            // Act
            var result = await Svc<ICarsServices>().Create(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dto.Make, result.Make);
            Assert.Equal(dto.Model, result.Model);
            Assert.Equal(dto.Year, result.Year);
            Assert.True(result.CreatedAt <= DateTime.Now);
            Assert.True(result.ModifiedAt <= DateTime.Now);
        }


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


        [Fact]
        public async Task Should_DeleteByIdCar_WhenDeleteCar()
        {
            //Arrange
            CarDto car = MockCarData();
            var AddCar = await Svc<ICarsServices>().Create(car);

            //Act
            var result = await Svc<ICarsServices>().Delete((Guid)AddCar.Id);

            //Assert
            Assert.Equal(result, AddCar);
        }

        [Fact]
        public async Task ShouldNot_DeleteByIdCar_WhenDidNotDeleteCar()
        {
            //Arrange
            CarDto car = MockCarData();
            var car1 = await Svc<ICarsServices>().Create(car);
            var car2 = await Svc<ICarsServices>().Create(car);
            //Act
            var result = await Svc<ICarsServices>().Delete((Guid)car2.Id);
            //Assert
            Assert.NotEqual(result.Id, car1.Id);
        }
        private CarDto MockCarData()
        {
            CarDto car = new()
            {
                Make = "Audi",
                Model = "A6",
                Year = 2007,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
            };
            return car;
        }
        [Fact]
        public async Task Should_UpdateCar_WhenUpdateData()
        {
            var guid = new Guid("f0d2b871-29c3-456e-8eb2-3277a4aa791a");
            CarDto dto = MockCarData();
            Car domain = new();
            domain.Id = Guid.Parse("f0d2b871-29c3-456e-8eb2-3277a4aa791a");
            domain.Make = "Audi";
            domain.Model = "100";
            domain.Year = 1993;
            domain.CreatedAt = DateTime.UtcNow;
            domain.ModifiedAt = DateTime.UtcNow;

            await Svc<ICarsServices>().Update(dto);
            Assert.Equal(guid, domain.Id);
            Assert.DoesNotMatch(dto.Model, domain.Model);
            Assert.DoesNotMatch(dto.Year.ToString(), domain.Year.ToString());
        }
        [Fact]
        public async Task Should_UpdateCar_WhenUpdatedataVersion2()
        {
            CarDto dto = MockCarData();
            var createCar = await Svc<ICarsServices>().Create(dto);
            CarDto update = MockCarData2();
            var result = await Svc<ICarsServices>().Update(update);
            Assert.DoesNotMatch(result.Make, createCar.Make);
            Assert.NotEqual(result.Year, createCar.Year);
        }
        [Fact]
        public async Task ShouldNot_UpdateCar_WhenDidNotUpdateData()
        {
            CarDto dto = MockCarData();
            var createCar = await Svc<ICarsServices>().Create(dto);
            CarDto nullUpdate = MockNullCarData();
            var result = await Svc<ICarsServices>().Update(nullUpdate);
            Assert.NotEqual(createCar.Id, result.Id);

        }

        private CarDto MockCarData2()
        {
            CarDto car2 = new()
            {
                Make = "Hyundai",
                Model = "Something",
                Year = 2000,
                CreatedAt = DateTime.Now.AddYears(1),
                ModifiedAt = DateTime.Now.AddYears(1),
            };
            return car2;
        }
        private CarDto MockNullCarData()
        {
            CarDto car2 = new()
            {
                Id = null,
                Make = "BMW",
                Model = "E46",
                Year = 2004,

                CreatedAt = DateTime.Now.AddYears(-1),
                ModifiedAt = DateTime.Now.AddYears(-1),
            };
            return car2;
        }

    }
}