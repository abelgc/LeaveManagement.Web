using AutoMapper;
using LeaveManagement.Web.Data;
using LeaveManagement.Web.Models;

namespace LeaveManagement.Web.Configutations
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {

            CreateMap<LeaveType, LeaveTypeVM>().ReverseMap();
        }
    }
}
