namespace Infrastructure.Services.ServiceLocator
{
    public interface IPrefs : IService
    {
        string LoadPref(string key);
        void SavePref(string key, string value);
    }
}