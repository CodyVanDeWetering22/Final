



namespace Final.Repositories;

public class VaultKeepRepository
{
    private readonly IDbConnection _db;

    public VaultKeepRepository(IDbConnection db)
    {
        _db = db;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
    {
        string sql = @"
        INSERT INTO
        vaultkeeps (creatorId, keepId, vaultId)
        VALUES(@CreatorId, @KeepId, @VaultId );


        SELECT * FROM vaultkeeps WHERE id = LAST_INSERT_ID();";




        VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, vaultKeepData).FirstOrDefault();
        return vaultKeep;
    }

    internal void DestoryVaultKeep(int vaultKeepId)
    {
        string sql = @"DELETE FROM vaultkeeps WHERE id = @vaultKeepId;";
        _db.Execute(sql, new { vaultKeepId });
    }

    internal VaultKeep GetVaultKeepById(int vaultKeepId)
    {
        string sql = @"
        SELECT 
        vaultkeeps.*,
        accounts.*
        FROM vaultkeeps
        JOIN accounts ON vaultkeeps.creatorId = accounts.id
        WHERE vaultkeeps.id = @vaultkeepId;";

        VaultKeep vaultKeep = _db.Query<VaultKeep, Profile, VaultKeep>(sql, (vaultkeep, profile) =>
        {
            vaultkeep.Creator = profile;
            return vaultkeep;
        }, new { vaultKeepId }).FirstOrDefault();
        return vaultKeep;
    }

    internal List<KeepInVault> GetVaultKeeps(int vaultId)
    {
        string sql = @"
    SELECT
    vaultkeeps.*,
    keeps.*,
    accounts.*
FROM vaultkeeps
    JOIN keeps on keeps.id = vaultkeeps.keepId
    JOIN accounts ON accounts.id = keeps.creatorId
WHERE vaultkeeps.vaultId = @vaultId";


        List<KeepInVault> vaultKeeps = _db.Query<VaultKeep, KeepInVault, Profile, KeepInVault>(sql, (vaultKeep, keepInVault, profile) =>
        {
            keepInVault.VaultKeepId = vaultKeep.Id;
            keepInVault.VaultId = vaultKeep.VaultId;
            keepInVault.Creator = profile;
            return keepInVault;
        }, new { vaultId }).ToList();
        return vaultKeeps;
    }
}
