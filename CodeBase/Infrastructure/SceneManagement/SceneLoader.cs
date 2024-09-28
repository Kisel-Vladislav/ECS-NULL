using System;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.SceneManagement
{
    public class SceneLoader
    {
        public void Load(string name, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == name)
            {
                onLoaded?.Invoke();
                return;
            }

            var asyncOperation = SceneManager.LoadSceneAsync(name);

            asyncOperation.completed += (a) => onLoaded?.Invoke();
        }
    }
}