using Microsoft.AspNetCore.Identity;

namespace WebApp.domain.Identity;

public class AppUser: IdentityUser
{
    public ICollection<Cesar>? Cesar { get; set; }
    
    //we are inheriting the rest of fields like primary keys from Identity user
}