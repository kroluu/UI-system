using Inputs;
using Zenject;

namespace Installers
{
    public class CoreInstaller : MonoInstaller<CoreInstaller>
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.BindInterfacesAndSelfTo<UIInputDetection>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PopupInputDetection>().AsSingle().NonLazy();
        }
    }
}