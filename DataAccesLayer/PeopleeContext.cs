using Microsoft.EntityFrameworkCore;
using Peoplee.API.Models;

namespace Peoplee.API.DataAccesLayer
{
    public class PeopleeContext : DbContext
    {
        public PeopleeContext(DbContextOptions<PeopleeContext> options) : base(options)
        {

        }

        public DbSet<User> Users {get; set;}
        
    }
}