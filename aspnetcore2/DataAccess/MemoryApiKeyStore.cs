namespace HYJ.Formation.AspNetCore.DataAccess
{
    public class MemoryApiKeyStore : IApiKeyStore
    {
        public bool IsAllowed(string key)
        {
            return key == "GOOD";
        }
    }
}