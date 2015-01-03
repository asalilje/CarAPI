using System;
using System.Collections;
using System.Collections.Generic;

namespace Car.API.Data
{
    public interface ICarRepository
    {

        string Save(Entities.Car car);

        Entities.Car Get(Guid id);

        bool Delete(Guid id);

        IEnumerable<Entities.Car> List();

        IEnumerable<Entities.Car> Search(string query);

    }
}