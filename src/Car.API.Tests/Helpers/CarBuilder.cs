using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Car.API.Entities;

namespace Car.API.Tests.Helpers
{
    public class CarBuilder
    {

        private Entities.Car _car;

        private CarBuilder()
        {
            _car = new Entities.Car();
        }
    
        public static CarBuilder CreateCar()
        {
            return new CarBuilder();
        }

        public CarBuilder WithId(Guid id)
        {
            _car.CarId = id;
            return this;
        }

        public CarBuilder WithMake(string make)
        {
            _car.Make = make;
            return this;
        }

        public CarBuilder WithCarType(CarType carType)
        {
            _car.CarType = carType;
            return this;
        }

        public CarBuilder WithTransmissionType(TransmissionType transmissionType)
        {
            _car.TransmissionType = transmissionType;
            return this;
        }

        public CarBuilder WithBagCount(int bagCount)
        {
            _car.BagCount = bagCount;
            return this;
        }

        public CarBuilder WithDoorCount(int doorCount)
        {
            _car.DoorCount = doorCount;
            return this;
        }

        public CarBuilder WithCurrency(string currency)
        {
            _car.Currency = currency;
            return this;
        }

        public CarBuilder WithRentalPricePerDay(decimal rentalPrice)
        {
            _car.RentalPricePerDay = rentalPrice;
            return this;
        }

        public CarBuilder WithAirCondition()
        {
            _car.HasAirCondition = true;
            return this;
        }

        public Entities.Car Build()
        {
            return _car;
        }

    }
}
