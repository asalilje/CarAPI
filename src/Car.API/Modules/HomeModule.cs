using System;
using Car.API.Data;
using Nancy;

namespace Car.API.Modules
{
    public class HomeModule : NancyModule
    {

        private readonly ICarRepository _carRepository;

        public HomeModule(ICarRepository carRepository)
        {
            _carRepository = carRepository;
            Get["/"] = _ => ListCars();
        }

        private object ListCars()
        {
            return _carRepository.ListAsync();
        }
    }
}