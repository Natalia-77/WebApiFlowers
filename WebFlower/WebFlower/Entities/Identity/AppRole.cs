using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;


namespace WebFlower.Entities.Identity
{
    public class AppRole:IdentityRole<long>
    {
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
    }
}
