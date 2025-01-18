using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DeviceCategoryManagement.Models;

namespace DeviceCategoryManagement.Data
{
    public class DeviceCategoryManagementContext : DbContext
    {
        public DeviceCategoryManagementContext (DbContextOptions<DeviceCategoryManagementContext> options)
            : base(options)
        {



        }

        public DbSet<DeviceCategoryManagement.Models.Device> Device { get; set; } = default!;

        public DbSet<DeviceCategoryManagement.Models.User>? User { get; set; }
    }
}
