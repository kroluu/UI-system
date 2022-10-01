using UI.Signals;
using UI.StateMachine.Machines;
using UnityEngine;
using Zenject;

namespace UI.StateMachine.Installers
{
    internal sealed class GameMachineInstaller : MonoInstaller
    {
        [SerializeField] private UIGameMachine gameMachine;

        public override void InstallBindings()
        {
            Container.BindInstance(gameMachine).AsSingle().NonLazy();
            Signals();
        }

        private void Signals()
        {
            Container.DeclareSignal<ChangeStateSignal>();
            Container.BindSignal<ChangeStateSignal>().ToMethod<UIGameMachine>(_uiGameManager => _uiGameManager.ReceiveChangeViewSignal).FromResolve();
        }
    }
}