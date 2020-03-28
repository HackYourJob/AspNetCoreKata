namespace HYJ.Formation.AspNetCore.DataAccess
{
    public interface IApiKeyStore
    {
        bool IsAllowed(string key);
        string TryToGetUserId(string key);
    }
}