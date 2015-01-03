using System;
using System.Linq;
using System.Threading;
using Car.API.Entities;
using Car.API.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace Car.API.Tests
{
    public class ElasticSearchCarRepositoryTests : IClassFixture<ElasticSearchFixture>
    {

        private readonly ElasticSearchFixture _fixture;

        public ElasticSearchCarRepositoryTests(ElasticSearchFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Save_Should_Return_Id_Of_Indexed_Car()
        {
            // given
            var carId = Guid.NewGuid();
            var car = CarBuilder.CreateCar()
                .WithId(carId)
                .WithMake("Ford Focus")
                .WithCarType(CarType.Premium).Build();

            // when 
            var result = _fixture.Repository.Save(car);

            // then
            result.Should().Be(carId.ToString());
        }

        [Fact]
        public void Saving_Already_Indexed_Car_Should_Update_Car()
        {
            // given
            var carId = Guid.NewGuid();
            var car = CarBuilder.CreateCar()
                .WithId(carId)
                .WithMake("Ford Focus").Build();

            // when 
            var result = _fixture.Repository.Save(car);
            var indexedCar = _fixture.Repository.Get(Guid.Parse(result));
            var updatedResult = _fixture.Repository.Save(indexedCar);


            // then
            result.Should().Be(carId.ToString());
            updatedResult.Should().Be(result);
        }


        [Fact]
        public void Get_Should_Return_Indexed_Car()
        {
            // given
            var carId = Guid.NewGuid();
            var car = CarBuilder.CreateCar()
                .WithId(carId)
                .WithMake("Ford Focus")
                .WithCarType(CarType.Premium).Build();

            // when 
            var result = _fixture.Repository.Save(car);
            var resultGuid = Guid.Parse(result);
            var indexedCar = _fixture.Repository.Get(resultGuid);

            // then
            resultGuid.Should().Be(carId);
            indexedCar.ShouldBeEquivalentTo(car);
            indexedCar.CarType.Should().Be(CarType.Premium);
            indexedCar.TransmissionType.Should().Be(TransmissionType.Manual);
        }

        [Fact]
        public void Delete_Should_Delete_Indexed_Car()
        {
            // given
            var carId = Guid.NewGuid();
            var car = CarBuilder.CreateCar()
                .WithId(carId)
                .WithMake("Ford Focus").Build();

            // when
            var result = _fixture.Repository.Save(car);
            var deleted = _fixture.Repository.Delete(Guid.Parse(result));

            // then
            deleted.Should().BeTrue();
        }

        [Fact]
        public void List_Should_List_All_Indexed_Cars()
        {
            // given
            for (var i = 0; i < 3; i++)
            {
                var carId = Guid.NewGuid();
                _fixture.Repository.Save(CarBuilder.CreateCar().WithId(carId).WithMake("Ford Focus").Build());
            }
            Thread.Sleep(2000);

            // when
            var result = _fixture.Repository.List();

            // then
            result.Count().Should().BeGreaterOrEqualTo(3);

        }

        [Fact]
        public void Search_Should_List_All_Indexed_Cars_Matching_Query()
        {
            // given
            for (var i = 0; i < 3; i++)
            {
                var carId = Guid.NewGuid();
                _fixture.Repository.Save(CarBuilder.CreateCar().WithId(carId).WithMake("My Test Car").WithBagCount(i + 2).Build());
            }
            Thread.Sleep(2000);

            // when
            var result = _fixture.Repository.Search("my test");
            
            // then
            result.Count().Should().Be(3);

        }
    }
}