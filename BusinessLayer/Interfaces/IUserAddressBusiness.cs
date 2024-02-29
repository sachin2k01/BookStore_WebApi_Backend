using ModelLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IUserAddressBusiness
    {
        public AddressModel AddAdress(AddressModel address, int userId);
        public IEnumerable<addressEntity> GetAddress(int userId);
    }
}
