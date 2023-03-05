using UnityEngine;

namespace Infrastructure.Services.ServiceLocator
{
    public class Prefs : IPrefs
    {
        public string LoadPref(string key)
        {
            return PlayerPrefs.GetString(key);
        }

        public void SavePref(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
            PlayerPrefs.Save();
        }
    }
}