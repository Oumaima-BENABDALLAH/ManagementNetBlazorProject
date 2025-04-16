using BasicLibrary.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;


namespace ManagementNetBlazorServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController(IGenericRepositoryInterface<Branch> genericRepositoryInterface)
     : GenericController<Branch>(genericRepositoryInterface)
    {
    }
}
