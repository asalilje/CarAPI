using System;
using System.Diagnostics.Contracts;
using Car.API.Entities.Validators;
using FluentValidation.Attributes;
using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Car.API.Entities
{
    [ElasticType(IdProperty = "CarId")]
    public class Car
    {

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public Guid CarId { get; set; }
        
        public string Make { get; set; }

        public CarType CarType { get; set; }

        public int DoorCount { get; set; }

        public int BagCount { get; set; }

        public bool HasAirCondition { get; set; }

        public TransmissionType TransmissionType { get; set; }

        public decimal RentalPricePerDay { get; set; }

        public string Currency { get; set; }


    }

    public enum TransmissionType
    {
        Manual,
        Automatic
    }

    public enum CarType
    {
        Mini,
        Economy,
        Compact,
        Intermediate,
        Standard,
        Fullsize,
        Premium,
        Luxury,
        Oversize,
        Special
    }
    

}