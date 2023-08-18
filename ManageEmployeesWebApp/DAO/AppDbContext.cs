using ManageEmployeesWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageEmployeesWebApp.DAO {
    public class AppDbContext :DbContext{
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
        }
        public DbSet<EmployeeEntity> Employees { get; set; }  
}
}
