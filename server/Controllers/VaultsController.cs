namespace Final.Controllers;

[ApiController]
[Route("api/[controller]")]

public class VaultsController : ControllerBase
{
    private readonly VaultsService _vaultsService;
    private readonly Auth0Provider _a0;
    private readonly VaultKeepService _vaultKeepsService;


    public VaultsController(Auth0Provider a0, VaultsService vaultsService, VaultKeepService vaultKeepsService)
    {
        _a0 = a0;
        _vaultsService = vaultsService;
        _vaultKeepsService = vaultKeepsService;
    }


    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData)
    {
        try
        {
            Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
            vaultData.CreatorId = userInfo.Id;
            Vault vault = _vaultsService.CreateVault(vaultData);
            return Ok(vault);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{vaultId}")]
    public async Task<ActionResult<Vault>> GetVaultById(int vaultId)
    {
        try
        {
            Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
            Vault vault = _vaultKeepsService.GetVaultById(vaultId, userInfo?.Id);
            return vault;
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [Authorize]
    [HttpPut("{vaultId}")]
    public async Task<ActionResult<Vault>> UpdateVault(int vaultId, [FromBody] Vault vaultData)
    {
        try
        {
            Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
            Vault vault = _vaultsService.UpdateVault(vaultId, userInfo.Id, vaultData);
            return Ok(vault);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }


    [Authorize]
    [HttpDelete("{vaultId}")]
    public async Task<ActionResult<string>> DestroyVault(int vaultId)
    {
        try
        {
            Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
            string message = _vaultsService.DestroyVault(vaultId, userInfo.Id);
            return Ok(message);

        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }

    }

    [HttpGet("{vaultId}/keeps")]
    public async Task<ActionResult<List<KeepInVault>>> GetVaultKeeps(int vaultId)
    {
        try
        {
            Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
            List<KeepInVault> vaultKeeps = _vaultKeepsService.GetVaultKeeps(vaultId, userInfo?.Id);
            return Ok(vaultKeeps);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}