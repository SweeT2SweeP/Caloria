using Infrastructure.Services.States;
using UnityEngine;

namespace Infrastructure
{
    public class AppBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private bool isDataReset;
        [SerializeField] private GameObject _curtainPrefab;
        
        private App _app;

        private void Awake()
        {
            if (isDataReset)
                PlayerPrefs.DeleteAll();
            
            _app = new App(this, _curtainPrefab);
            _app._stateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}