namespace Final.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultKeepsController : ControllerBase
{
    private readonly Auth0Provider _a0;
    private readonly VaultKeepService _vaultKeepService;

    public VaultKeepsController(Auth0Provider a0, VaultKeepService vaultKeepService)
    {
        _a0 = a0;
        _vaultKeepService = vaultKeepService;
    }


    [Authorize]
    [HttpPost]
    public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep vaultKeepData)
    {
        try
        {
            Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
            vaultKeepData.CreatorId = userInfo.Id;
            VaultKeep vaultKeep = _vaultKeepService.CreateVaultKeep(vaultKeepData);
            return Ok(vaultKeep);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }

    }

    [Authorize]
    [HttpDelete("{vaultKeepId}")]
    public async Task<ActionResult<string>> DestroyVaultKeep(int vaultKeepId)
    {
        try
        {
            Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
            string message = _vaultKeepService.DestroyVaultKeep(vaultKeepId, userInfo.Id);
            return Ok(message);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{vaultKeepId}")]
    public ActionResult<VaultKeep> GetVaultKeepById(int vaultKeepId)
    {
        try
        {
            VaultKeep vaultKeep = _vaultKeepService.GetVaultKeepById(vaultKeepId);
            return Ok(vaultKeep);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

}
