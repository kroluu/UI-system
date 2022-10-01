using UI.StateMachine.Machines;
using Zenject;

namespace UI.StateMachine.States.MenuStates
{
    /// <summary>
    /// Class <c>MenuState</c> is <c>State</c> representation for each state in menu scene
    /// </summary>
    internal class MenuView : View<MenuType, MenuTrigger>
    {
        /// <summary>
        /// Machine currently on scene
        /// </summary>
        /// <param name="_machine">Machine of <c>UIMenuMachine</c> type</param>
        [Inject]
        private void Init(UIMenuMachine _machine)
        {
            machine = _machine;
        }
        
        /// <summary>
        /// Sets own reference to state in UI machine 
        /// </summary>
        /// <param name="_actualMachineview">Actual state in machine</param>
        protected override void SetView(MenuType _actualMachineview)
        {
            if(_actualMachineview != view) return;
            machine.currentView = this;
        }
    }
}