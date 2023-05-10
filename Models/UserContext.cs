using Diplom_Pogodel.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace Diplom_Pogodel.Models
{
    public class UserContext : DbContext
    {
        public DbSet<User> User { get; set; } = null!; //Указываем что данное свойство не может быть пустым
        



        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
            
        }

		
	}
}
