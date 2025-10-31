using Microsoft.AspNetCore.Identity;

namespace Newspaper.Domain.Entities;

public class User : IdentityUser
{
    public ICollection<News> News { get; set; }
}