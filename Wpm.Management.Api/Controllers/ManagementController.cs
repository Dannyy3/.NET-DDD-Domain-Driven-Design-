using Microsoft.AspNetCore.Mvc;
using Wpm.Management.Api.Application;

namespace Wpm.Management.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ManagementController(ManagementApplicationService appService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post(CreatePetCommand command)
    {
        await appService.Handle(command);
        return Ok();
    }

    [HttpPost("set-weight")]
    public async Task<IActionResult> Post(SetWeightCommand command)
    {
        await appService.Handle(command);
        return Ok();
    }

}
