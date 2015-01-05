using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Car.API.Data
{
    public interface ICarRepository
    {

        Task<string> SaveAsync(Entities.Car car);

        Task<Entities.Car> GetAsync(Guid id);

        Task<bool> DeleteAsync(Guid id);

        Task<IEnumerable<Entities.Car>> ListAsync();

        Task<IEnumerable<Entities.Car>> SearchAsync(string query);

    }
}