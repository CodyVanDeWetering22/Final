



using System.Security.Cryptography;

namespace Final.Services;


public class VaultKeepService
{

    private readonly VaultKeepRepository _repository;

    public VaultKeepService(VaultKeepRepository repository)
    {
        _repository = repository;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
    {
        VaultKeep vaultKeep = _repository.CreateVaultKeep(vaultKeepData);
        return vaultKeep;
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

    internal List<KeepInVault> GetVaultKeeps(int vaultId)
    {
        List<KeepInVault> vaultKeeps = _repository.GetVaultKeeps(vaultId);
        return vaultKeeps;




    }
}