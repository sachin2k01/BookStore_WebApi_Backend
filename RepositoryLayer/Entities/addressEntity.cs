using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entities
{
    public class addressEntity
    {
        public int addressId {  get; set; }
        public string fullAddress { get; set; }
        public string city {  get; set; }
        public string state { get; set; }   
        public string type {  get; set; }
        public int userId {  get; set; }
    }
}
