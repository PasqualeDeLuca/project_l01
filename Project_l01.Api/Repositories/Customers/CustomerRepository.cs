

using Microsoft.EntityFrameworkCore;
using Project_l01.Api.Data;
using Project_l01.Api.Models;


namespace Project_l01.Api.Repositories;

public class CustomerRepository: ICustomerRepository
{
    private readonly ApplicationDbContext _context;

    public CustomerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Customer?> GetByIdAsync(int id)
    {
        return await _context.Customers
            .FirstOrDefaultAsync(customer => customer.Id == id);
    }
}