using ModelLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IUserAddressRepo
    {
        public AddressModel AddAdress(AddressModel address, int userId);
        public string UpdateAddress(AddressModel address, int adressId, int userId);
        public IEnumerable<addressEntity> GetAddress(int userId);
    }
}
