





namespace Final.Services;


public class VaultKeepService
{

    private readonly VaultKeepRepository _repository;
    private readonly VaultsService _vaultService;
    private readonly VaultsRepository _vaultRepository;

    public VaultKeepService(VaultKeepRepository repository, VaultsService vaultService = null, VaultsRepository vaultRepository = null)
    {
        _repository = repository;
        _vaultService = vaultService;
        _vaultRepository = vaultRepository;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData, string userId)
    {
        // FIXME make sure the user is the creator of the vault
        Vault vault = GetVaultById(vaultKeepData.VaultId, userId);

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
        // FIXME before getting the vaultkeeps get the vault to make sure the user has access to it


        Vault vault = _vaultRepository.GetVaultById(vaultId);
        if (vault.CreatorId != userId)
        {
            throw new Exception("cant");
        }
        List<KeepInVault> vaultKeeps = _repository.GetVaultKeeps(vaultId);
        return vaultKeeps;
    }
}