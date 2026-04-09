using BitcoinApi.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/bitcoin")]
public class WorkerController : ControllerBase
{
    private readonly ILogger<WorkerController> _logger;
    private readonly WorkerService _service;

    public WorkerController(ILogger<WorkerController> logger, WorkerService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet("latest")]
    public ActionResult<Bitcoin> GetLastBitcoin()
    {
        _logger.LogInformation("Received request for latest Bitcoin data");

        return Ok(_service.GetLastBitcoin());
    }
}