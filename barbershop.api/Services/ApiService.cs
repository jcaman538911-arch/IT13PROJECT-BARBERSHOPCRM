using barbershop.domain;

namespace barbershop.api.Services;

public class ApiService
{
    private readonly ISqlDataRepository _repository;

    public ApiService(ISqlDataRepository repository)
    {
        _repository = repository;
    }

    public User? Authenticate(string username, string password)
    {
        return _repository.Authenticate(username, password);
    }
}
