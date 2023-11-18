using Microsoft.AspNetCore.Routing;

namespace library.presentation.Endpoints.Base;
public interface IModule
{
    public void RegisterEndpoints(IEndpointRouteBuilder app);
}
