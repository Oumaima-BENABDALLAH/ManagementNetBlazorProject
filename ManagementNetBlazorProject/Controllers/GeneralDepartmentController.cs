using BasicLibrary.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace ManagementNetBlazorServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralDepartmentController (IGenericRepositoryInterface<GeneralDepartment> genericRepositoryInterface)
     : GenericController<GeneralDepartment>(genericRepositoryInterface)
    {
    }
}
