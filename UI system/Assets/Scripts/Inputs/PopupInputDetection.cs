using System;
using Zenject;

namespace Inputs
{
    public sealed class PopupInputDetection : ILateTickable, IDisposable
    {
        public event Action OnQuitPressed;
        private InputActions.PopupActions popupActions;

        [Inject]
        private void InputInit(InputReader _inputReader)
        {
            popupActions = _inputReader.inputs.Popup;
        }
        public void LateTick()
        {
            QuitHandle();
        }

        public void Dispose()
        {
            OnQuitPressed = null;
        }

        private void QuitHandle()
        {
            if(popupActions.Quit.WasPressedThisFrame())
                OnQuitPressed?.Invoke();
        }
        
    }
}