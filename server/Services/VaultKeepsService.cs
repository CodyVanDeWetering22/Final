





namespace Final.Services;


public class VaultKeepService
{

    private readonly VaultKeepRepository _repository;
    private readonly VaultsService _vaultService;
    private readonly VaultsRepository _vaultRepository;

    private readonly KeepsService _keepsService;

    public VaultKeepService(VaultKeepRepository repository, VaultsService vaultService = null, VaultsRepository vaultRepository = null, KeepsService keepsService = null)
    {
        _repository = repository;
        _vaultService = vaultService;
        _vaultRepository = vaultRepository;
        _keepsService = keepsService;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData, string userId)
    {

        Vault vault = GetVaultById(vaultKeepData.VaultId, userId);
        Keep keep = _keepsService.GetKeepById(vaultKeepData.KeepId, userId);
        keep.Kept++;
        _keepsService.UpdateKeepKept(keep);



        if (vault.CreatorId != userId)
        {
            throw new Exception("you cant do that ");
        }
        VaultKeep vaultKeep = _repository.CreateVaultKeep(vaultKeepData);
        return vaultKeep;
    }





    internal Vault GetVaultById(int vaultId, string userId)
    {
        Vault vault = _vaultRepository.GetVaultById(vaultId);

        if (vault == null)
        {
            throw new Exception($"Invalid Id: ${vaultId}");
        }
        if (vault.IsPrivate == true && vault.CreatorId != userId)
        {
            throw new Exception("Something went wrong!");
        }

        return vault;
    }

    internal string DestroyVaultKeep(int vaultKeepId, string userId)
    {
        VaultKeep vaultKeep = GetVaultKeepById(vaultKeepId);
        if (vaultKeep.CreatorId != userId)
        {
            throw new Exception("not urs to delete");

        }
        _repository.DestoryVaultKeep(vaultKeepId);
        return "its gone";
    }

    internal VaultKeep GetVaultKeepById(int vaultKeepId)
    {
        VaultKeep vaultKeep = _repository.GetVaultKeepById(vaultKeepId);
        return vaultKeep;
    }

    internal List<KeepInVault> GetVaultKeeps(int vaultId, string userId)
    {

        Vault vault = _vaultRepository.GetVaultById(vaultId);
        if (vault.CreatorId != userId && vault.IsPrivate == true)
        {
            throw new Exception("Unable to get keeps for a private vault");
        }
        List<KeepInVault> vaultKeeps = _repository.GetVaultKeeps(vaultId);
        return vaultKeeps;
    }
}