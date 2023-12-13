namespace Final.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KeepsController : ControllerBase

{

    private readonly Auth0Provider _a0;
    private readonly KeepsService _keepsService;

    public KeepsController(Auth0Provider a0, KeepsService keepsService)
    {
        _a0 = a0;
        _keepsService = keepsService;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep keepData)
    {
        try
        {
            Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
            keepData.CreatorId = userInfo.Id;
            Keep keep = _keepsService.CreateKeep(keepData);
            return Ok(keep);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }



    }

    [HttpGet]
    public ActionResult<List<Keep>> GetKeeps()
    {
        try
        {

            List<Keep> keeps = _keepsService.GetKeeps();

            return Ok(keeps);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{keepId}")]
    public async Task<ActionResult<Keep>> GetKeepById(int keepId)
    {
        try
        {
            Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
            Keep keep = _keepsService.GetKeepByIdAndIncrementViews(keepId, userInfo?.Id);
            return Ok(keep);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [Authorize]
    [HttpPut("{keepId}")]
    public async Task<ActionResult<Keep>> UpdateKeep(int keepId, [FromBody] Keep keepData)
    {
        try
        {
            Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
            Keep keep = _keepsService.UpdateKeep(keepId, userInfo.Id, keepData);
            return Ok(keep);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }


    [Authorize]
    [HttpDelete("{keepId}")]
    public async Task<ActionResult<string>> DestroyKeep(int keepId)
    {
        try
        {
            Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
            string message = _keepsService.DestroyKeep(keepId, userInfo.Id);
            return Ok(message);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

}