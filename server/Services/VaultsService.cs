


namespace Final.Services;

public class VaultsService
{
    private readonly VaultsRepository _repository;

    public VaultsService(VaultsRepository repository)
    {
        _repository = repository;
    }

    internal Vault CreateVault(Vault vaultData)
    {
        Vault vault = _repository.CreateVault(vaultData);
        return vault;
    }

    internal string DestroyVault(int vaultId, string userId)
    {
        Vault vault = GetVaultById(vaultId, userId);

        if (vault.CreatorId != userId)
        {
            throw new Exception("not ur vault!!");
        }

        _repository.DestroyVault(vaultId);

        return $"{vault.Name} has been deleted";
    }

    internal Vault GetVaultById(int vaultId, string userId)
    {
        Vault vault = _repository.GetVaultById(vaultId);

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

    internal List<Vault> GetVaultsByAccount(string userId)
    {
        List<Vault> vaults = _repository.GetVaultsByAccount(userId);
        return vaults;
    }

    internal List<Vault> GetVaultsByProfile(string profileId, string userId)
    {
        List<Vault> vaults = _repository.GetVaultsByProfile(profileId);
        vaults = vaults.FindAll(vault => vault.IsPrivate == false || vault.CreatorId == userId);

        return vaults;
    }

    internal Vault UpdateVault(int vaultId, string userId, Vault vaultData)
    {
        Vault vaultToUpdate = GetVaultById(vaultId, userId);

        if (vaultToUpdate.CreatorId != userId)
        {
            throw new Exception("not ur vault!");
        }
        vaultToUpdate.Name = vaultData.Name ?? vaultToUpdate.Name;
        vaultToUpdate.Description = vaultData.Description ?? vaultToUpdate.Description;
        vaultToUpdate.Img = vaultData.Img ?? vaultToUpdate.Img;
        vaultToUpdate.IsPrivate = vaultData.IsPrivate ?? vaultToUpdate.IsPrivate;

        Vault vault = _repository.UpdateVault(vaultToUpdate);
        return vault;
    }
}