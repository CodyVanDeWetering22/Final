namespace Final.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
    private readonly Auth0Provider _a0;
    private readonly ProfilesService _profilesService;
    private readonly KeepsService _keepsService;
    private readonly VaultsService _vaultsService;

    public ProfilesController(Auth0Provider a0, ProfilesService profilesService, KeepsService keepsService, VaultsService vaultsService)

    {
        _a0 = a0;
        _profilesService = profilesService;
        _keepsService = keepsService;
        _vaultsService = vaultsService;
    }




    [HttpGet("{profileId}")]
    public ActionResult<Profile> GetProfile(string profileId)
    {
        try
        {
            Profile profile = _profilesService.GetProfile(profileId);
            return Ok(profile);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{profileId}/keeps")]
    public ActionResult<List<Keep>> GetKeepsByProfile(string profileId)
    {
        try
        {
            List<Keep> keeps = _keepsService.GetKeepsByProfile(profileId);
            return Ok(keeps);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }


    [HttpGet("{profileId}/vaults")]
    public async Task<ActionResult<List<Vault>>> GetVaultsByProfile(string profileId)
    {
        try
        {
            Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
            List<Vault> vaults = _vaultsService.GetVaultsByProfile(profileId, userInfo?.Id);
            return vaults;
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}


