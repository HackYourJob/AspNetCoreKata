namespace HYJ.Formation.AspNetCore.DataAccess
{
    public class MemoryApiKeyStore : IApiKeyStore
    {
        public bool IsAllowed(string key)
        {
            return key == "GOOD";
        }

        public string TryToGetUserId(string key)
        {
            return IsAllowed(key) ? "1" : null;
        }
    }
}