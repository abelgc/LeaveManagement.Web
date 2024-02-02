using LeaveManagement.Web.Data;

namespace LeaveManagement.Web.Repositories
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        public LeaveTypeRepository(LeaveManagementDbContext context) : base(context)
        {
        }
    }
}
