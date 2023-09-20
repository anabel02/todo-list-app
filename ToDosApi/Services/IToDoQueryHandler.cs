using ToDosApi.Queries;

namespace ToDosApi.Services;

public interface IToDoQueryHandler
{
    Task<GetToDoResponse> FindById(int productId);
    IAsyncEnumerable<GetToDoResponse> All();
    IAsyncEnumerable<GetToDoResponse> FindByName(string name);
    
    IAsyncEnumerable<GetToDoResponse> FilterByIsComplete();
    
    IAsyncEnumerable<GetToDoResponse> FilterByIsNotComplete();
}