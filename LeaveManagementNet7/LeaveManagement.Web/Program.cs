using LeaveManagement.Web.Configutations;
using LeaveManagement.Web.Data;
using LeaveManagement.Web.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<LeaveManagementDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<Employee>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<LeaveManagementDbContext>();

            // investigate that one. Not sure
            // here we register the generic
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            // everyone can relau on ILeaveTypeRepository and this contract is honored by LeaveTypeRepository file
            builder.Services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();

            builder.Services.AddAutoMapper(typeof(MappingConfiguration));

            builder.Services.AddControllersWithViews();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
