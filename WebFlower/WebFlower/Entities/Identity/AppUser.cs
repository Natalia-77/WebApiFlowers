using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;


namespace WebFlower.Entities.Identity
{
    public class AppUser:IdentityUser<long>
    {
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
    }
}
