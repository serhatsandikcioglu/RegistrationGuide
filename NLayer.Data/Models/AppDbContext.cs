using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Data.Models
{
    public class AppDbContext : IdentityDbContext <AppUser,AppRole,string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            //AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
            //AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CommercialActivity> CommercialActivities { get; set; }

    }
}
