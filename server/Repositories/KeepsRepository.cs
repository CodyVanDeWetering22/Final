





namespace Final.Repositories;

public class KeepsRepository
{

    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Keep CreateKeep(Keep keepData)
    {
        string sql = @"
        INSERT INTO 
        keeps (name, description, img, views, creatorId)
        VALUES (@Name , @Description, @Img, @Views, @CreatorId);


        SELECT
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts On keeps.creatorId = accounts.Id
        WHERE keeps.id = LAST_INSERT_ID();";

        Keep keep = _db.Query<Keep, Profile, Keep>(sql, KeepBuilder, keepData).FirstOrDefault();
        return keep;
    }

    internal void DestroyKeep(int keepId)
    {
        string sql = @" DELETE FROM keeps WHERE id = @keepId;";
        _db.Execute(sql, new { keepId });
    }

    internal Keep GetKeepById(int keepId)
    {
        string sql = @"
        SELECT
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON accounts.id = keeps.creatorId
        WHERE keeps.id = @keepId;";

        Keep keep = _db.Query<Keep, Account, Keep>(sql, KeepBuilder, new { keepId }).FirstOrDefault();
        return keep;
    }

    internal List<Keep> GetKeeps()
    {
        string sql = @"
        SELECT 
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts On accounts.id = keeps.creatorId;";

        List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, KeepBuilder).ToList();
        return keeps;
    }

    internal List<Keep> GetKeepsByProfile(string profileId)
    {
        string sql = @"
    SELECT 
    keeps.*, 
    accounts.*
    FROM keeps
    JOIN accounts ON accounts.id = keeps.creatorId
    WHERE keeps.creatorId = @profileId;";


        List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
        {
            keep.Creator = profile;
            return keep;
        }, new { profileId }).ToList();
        return keeps;
    }

    internal Keep UpdateKeep(Keep keepData)
    {
        string sql = @"
    UPDATE keeps
    SET
    name = @Name,
    description =@description,
    img = @Img,
    views = @Views
    WHERE id = @Id;
    
    SELECT 
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts On accounts.id = keeps.creatorId
        WHERE keeps.id = @Id;";

        Keep keep = _db.Query<Keep, Account, Keep>(sql, KeepBuilder, keepData).FirstOrDefault();
        return keep;






    }

    private Keep KeepBuilder(Keep keep, Profile profile)
    {
        keep.Creator = profile;
        return keep;
    }
}