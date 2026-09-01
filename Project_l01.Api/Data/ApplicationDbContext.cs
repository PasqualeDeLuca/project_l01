using Microsoft.EntityFrameworkCore;

namespace Project_l01.Api.Data;

public class ApplicationDbContext : DbContext
{
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
}