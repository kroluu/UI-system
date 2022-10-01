using Inputs;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = "ScriptableInstaller", menuName = "Containers/Installers/ScriptableInstaller", order = 0)]
    public class ScriptableInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private InputReader inputReader;
        
        public override void InstallBindings()
        {
            Container.BindInstance(inputReader).AsSingle().NonLazy();
        }
    }
}
