using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IApartment
    {
        Task<IEnumerable<Apartment>> GetAllApartmentsAsync();
        Task<Apartment> GetApartmentByIdAsync(int apartmentId);
        Task InsertApartmentAsync(Apartment apartment);
        Task UpdateApartmentAsync(Apartment apartment);
        Task DeleteApartmentAsync(int apartmentId);
    }
}
