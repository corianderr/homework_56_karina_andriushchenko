using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homework_56.Models
{
    public class MobileContext : IdentityDbContext<User>
    {
        public DbSet<Task> Tasks { get; set; }
        public MobileContext(DbContextOptions<MobileContext> options) : base(options) { }
    }
}
