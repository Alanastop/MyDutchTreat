using DutchTreat.Library2.Models.Persistent.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Library2.Models.Persistent
{
    public class StoreUser : IdentityUser, IEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        int IEntity.Id { get; set; }
    }
}
