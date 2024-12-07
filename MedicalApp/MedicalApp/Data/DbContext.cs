using MedicalApp.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MedicalApp.Data
{
    public class AppDBContext : IdentityDbContext<UserApp>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

       
    }
}
