using System.ComponentModel;

namespace Final.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;

  private readonly VaultsService _vaultsService;

  private readonly Auth0Provider _a0;
  public AccountController(AccountService accountService, Auth0Provider auth0Provider, VaultsService vaultsService, Auth0Provider a0)
  {
    _accountService = accountService;
    _auth0Provider = auth0Provider;
    _vaultsService = vaultsService;
    _a0 = a0;
  }

  [HttpGet]
  [Authorize]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateProfile(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("vaults")]
  public async Task<ActionResult<List<Vault>>> GetVaultsByAccount()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<Vault> vaults = _vaultsService.GetVaultsByAccount(userInfo?.Id);
      return Ok(vaults);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
  [Authorize]
  [HttpPut("{accountId}")]
  public async Task<ActionResult<List<Account>>> Edit(int accountId, [FromBody] Account accountData)
  {
    try
    {
      Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
      Account account = _accountService.Edit(accountId, userInfo.Id, accountData);
      return Ok(account);

    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

}
