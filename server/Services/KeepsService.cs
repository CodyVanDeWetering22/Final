


namespace Final.Services;

public class KeepsService
{

    private readonly KeepsRepository _repository;

    public KeepsService(KeepsRepository repository)
    {
        _repository = repository;
    }

    internal Keep CreateKeep(Keep keepData)
    {
        Keep keep = _repository.CreateKeep(keepData);
        return keep;
    }

    internal string DestroyKeep(int keepId, string userId)
    {
        Keep keep = GetKeepById(keepId, userId);

        if (keep.CreatorId != userId)
        {
            throw new Exception("not urs to destroy!");
        }

        _repository.DestroyKeep(keepId);
        return $"{keep.Name} has been destroyed";
    }

    internal Keep GetKeepById(int keepId, string userId)
    {

        Keep keep = _repository.GetKeepById(keepId);

        if (keep == null)
        {
            throw new Exception($"Invalid Id: {keepId}");
        }
        // _repository.GetKeepById(keepId);
        return keep;
    }

    internal Keep GetKeepByIdAndIncrementViews(int keepId, string userId)
    {
        Keep keep = GetKeepById(keepId, userId);
        keep.Views++;
        _repository.UpdateKeep(keep);
        return keep;
    }

    internal List<Keep> GetKeeps()
    {
        List<Keep> keeps = _repository.GetKeeps();
        return keeps;
    }

    internal List<Keep> GetKeepsByProfile(string profileId)
    {
        List<Keep> keeps = _repository.GetKeepsByProfile(profileId);

        return keeps;
    }

    internal Keep UpdateKeep(int keepId, string userId, Keep keepData)
    {
        Keep keepToUpdate = GetKeepById(keepId, userId);

        if (keepToUpdate.CreatorId != userId)
        {
            throw new Exception("not ur keep!");
        }

        keepToUpdate.Name = keepData.Name ?? keepToUpdate.Name;
        keepToUpdate.Description = keepData.Description ?? keepToUpdate.Description;
        keepToUpdate.Name = keepData.Name ?? keepToUpdate.Name;

        Keep keep = _repository.UpdateKeep(keepToUpdate);
        return keep;
    }
}