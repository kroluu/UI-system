using System;
using Zenject;

namespace Inputs
{
    public sealed class UIInputDetection : ITickable, IDisposable
    {
        public event Action OnEscapePressed;
        public event Action OnTabPressed;
        private InputActions.UIActions uiActions;
        [Inject]
        private void InputInit(InputReader _inputReader)
        {
            uiActions = _inputReader.inputs.UI;
        }
        
        public void Tick()
        {
            EscapeHandle();
            TabHandle();
        }
        
        public void Dispose()
        {
            OnEscapePressed = null;
        }

        private void EscapeHandle()
        {
            if(uiActions.Back.WasPressedThisFrame())
                OnEscapePressed?.Invoke();
        }

        private void TabHandle()
        {
            if(uiActions.Tab.WasPressedThisFrame())
                OnTabPressed?.Invoke();
        }
        
    }
}
