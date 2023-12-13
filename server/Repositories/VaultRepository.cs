




namespace Final.Repositories;

public class VaultsRepository
{
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Vault CreateVault(Vault vaultData)
    {
        string sql = @"
        INSERT INTO 
        vaults (name, description, img, isPrivate, creatorId)
        VALUES (@Name , @Description, @Img, @IsPrivate, @CreatorId);


        SELECT
        vaults.*,
        accounts.*
        FROM vaults
        JOIN accounts On vaults.creatorId = accounts.Id
        WHERE vaults.id = LAST_INSERT_ID();";

        Vault vault = _db.Query<Vault, Profile, Vault>(sql, VaultBuilder, vaultData).FirstOrDefault();
        return vault;



    }

    internal void DestroyVault(int vaultId)
    {
        string sql = "DELETE FROM vaults WHERE id= @vaultId";
        _db.Execute(sql, new { vaultId });
    }

    internal Vault GetVaultById(int vaultId)
    {
        string sql = @"
        SELECT
        vaults.*,
        accounts.*
        FROM vaults
        JOIN accounts On vaults.creatorId = accounts.Id
        WHERE vaults.id = @vaultId;";

        Vault vault = _db.Query<Vault, Profile, Vault>(sql, VaultBuilder, new { vaultId }).FirstOrDefault();
        return vault;

    }

    internal Vault UpdateVault(Vault vaultData)
    {
        string sql = @"
        
    UPDATE vaults
    SET
    name = @Name,
    description =@description,
    img = @Img,
    isPrivate = @IsPrivate
    WHERE id = @Id;
    
    SELECT 
        vaults.*,
        accounts.*
        FROM vaults
        JOIN accounts On accounts.id = vaults.creatorId
        WHERE vaults.id = @Id;";

        Vault vault = _db.Query<Vault, Profile, Vault>(sql, VaultBuilder, vaultData).FirstOrDefault();
        return vault;
    }

    private Vault VaultBuilder(Vault vault, Profile profile)
    {
        vault.Creator = profile;
        return vault;
    }


    internal List<Vault> GetVaultsByProfile(string profileId)
    {
        string sql = @"
    SELECT 
    vaults.*, 
    accounts.*
    FROM vaults
    JOIN accounts ON accounts.id = vaults.creatorId
    WHERE vaults.creatorId = @profileId;";

        List<Vault> vaults = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
            {
                vault.Creator = profile;
                return vault;
            }, new { profileId }).ToList();
        return vaults;
    }

    internal List<Vault> GetVaultsByAccount(string userId)
    {
        string sql = @"
        SELECT
        vaults.*,
        accounts.*
        FROM vaults
        JOIN accounts ON accounts.id = vaults.creatorId
        WHERE vaults.creatorId = @userId;";


        List<Vault> vaults = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
        {
            vault.Creator = profile;
            return vault;
        }, new { userId }).ToList();
        return vaults;
    }
}