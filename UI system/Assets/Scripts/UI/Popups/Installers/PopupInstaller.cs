using Inputs;
using UI.Popups.Containers;
using UI.Popups.Interfaces;
using UI.Popups.Types;
using UI.Signals;
using UnityEngine;
using Zenject;

namespace UI.Popups.Installers
{
    internal sealed class PopupInstaller : MonoInstaller
    {
        [SerializeField] private PopupsContainer popupsContainer;
        [SerializeField] private PopupManager popupManager;
        
        public override void InstallBindings()
        {
            Container.BindInstance(popupsContainer).AsSingle().WhenInjectedInto<PopupManager>().Lazy();
            Container.BindInstance(popupManager).AsSingle().Lazy();
            Container.BindInterfacesAndSelfTo<PopupInputDetection>().AsSingle().NonLazy();
            Factories();
            Signals();
        }

        private void Factories()
        {
            Container.BindFactory<Transform, IPopupVisibility, Popup.Factory>().FromFactory<Popup.PopupFactory>();
        }

        private void Signals()
        {
            Container.DeclareSignal<FirePopupSignal>();
            Container.BindSignal<FirePopupSignal>().ToMethod<PopupManager>(_popupManager => _popupManager.ReceiveFirePopupSignal)
                .FromResolve();
            Container.DeclareSignal<FirePopupWithEnqueueSignal>();
            Container.BindSignal<FirePopupWithEnqueueSignal>()
                .ToMethod<PopupManager>(_popupManager => _popupManager.ReceiveFirePopupSignal).FromResolve();
            Container.DeclareSignal<ClosePopupSignal>();
            Container.BindSignal<ClosePopupSignal>()
                .ToMethod<PopupManager>(_popupManager => _popupManager.ReceiveClosePopupSignal).FromResolve();
        }
    }
}