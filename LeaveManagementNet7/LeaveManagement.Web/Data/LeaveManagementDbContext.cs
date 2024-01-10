using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Web.Data
{
    public class LeaveManagementDbContext : IdentityDbContext<Employee>
    {
        public LeaveManagementDbContext(DbContextOptions<LeaveManagementDbContext> options)
          : base(options)
        {

        }

        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
    }
}
