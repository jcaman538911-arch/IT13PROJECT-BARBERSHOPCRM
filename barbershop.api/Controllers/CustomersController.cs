using Microsoft.AspNetCore.Mvc;
using barbershop.domain;
using barbershop.api.Common;

namespace barbershop.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ISqlDataRepository _repository;

    public CustomersController(ISqlDataRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult GetCustomers()
    {
        var list = _repository.GetCustomers();
        return Ok(ApiResponse<List<Customer>>.Ok(list));
    }

    [HttpGet("{id}")]
    public IActionResult GetCustomer(int id)
    {
        var cust = _repository.GetCustomerById(id);
        if (cust == null)
            return NotFound(ApiResponse<string>.Fail("Customer not found"));

        return Ok(ApiResponse<Customer>.Ok(cust));
    }
}
