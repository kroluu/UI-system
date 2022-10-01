using UI.Signals;
using UI.StateMachine.Machines;
using UnityEngine;
using Zenject;

namespace UI.StateMachine.Installers
{
    internal sealed class MenuMachineInstaller : MonoInstaller
    {
        [SerializeField] private UIMenuMachine menuMachine;
        
        public override void InstallBindings()
        {
            Container.BindInstance(menuMachine).AsSingle().NonLazy();
            Signals();
        }
        
        private void Signals()
        {
            Container.DeclareSignal<ChangeStateSignal>();
            Container.BindSignal<ChangeStateSignal>().ToMethod<UIMenuMachine>(_uiMenuMachine => _uiMenuMachine.ReceiveChangeViewSignal).FromResolve();
        }
    }
}