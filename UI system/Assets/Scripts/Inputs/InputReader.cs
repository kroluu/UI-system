using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Inputs
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "Containers/Input/Input Reader")]
    public class InputReader : ScriptableObject, InputActions.IUIActions, InputActions.IPopupActions
    {
        public InputActions inputs;

        private void OnEnable()
        {
            if (inputs == null)
            {
                inputs = new InputActions();
                inputs.UI.SetCallbacks(this);
                inputs.Popup.SetCallbacks(this);
            }
            InputsForUI();
        }

        public void InputsForPopup()
        {
            inputs.UI.Disable();
            inputs.Popup.Enable();
        }

        public void InputsForUI()
        {
            inputs.Popup.Disable();
            inputs.UI.Enable();
        }
        
        private void OnDisable()
        {
            DisableAllInput();
        }

        private void DisableAllInput()
        {
            inputs.UI.Disable();
            inputs.Popup.Disable();
        }

        public void OnBack(InputAction.CallbackContext context)
        {
        }

        public void OnPoint(InputAction.CallbackContext context)
        {
        }

        public void OnScrollWheel(InputAction.CallbackContext context)
        {
        }

        public void OnMiddleClick(InputAction.CallbackContext context)
        {
        }

        public void OnRightClick(InputAction.CallbackContext context)
        {
        }

        public void OnClick(InputAction.CallbackContext context)
        {
        }

        public void OnNavigate(InputAction.CallbackContext context)
        {
        }

        public void OnSubmit(InputAction.CallbackContext context)
        {
        }

        public void OnTab(InputAction.CallbackContext context)
        {
        }

        public void OnQuit(InputAction.CallbackContext context)
        {
        }
    }
}