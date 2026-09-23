

using Project_l01.Api.Models;

namespace Project_l01.Api.Repositories;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(int id);
}