using UI.StateMachine.Machines;
using Zenject;

namespace UI.StateMachine.States.GameStates
{
    /// <summary>
    /// Class <c>GameState</c> is <c>State</c> representation for each state in game scene
    /// </summary>
    internal class GameView : View<GameType, GameTrigger>
    {
        /// <summary>
        /// Machine currently on scene
        /// </summary>
        /// <param name="_machine">Machine of <c>UIGameMachine</c> type</param>
        [Inject]
        private void Init(UIGameMachine _machine)
        {
            machine = _machine;
        }
        
        /// <summary>
        /// Sets own reference to state in UI machine 
        /// </summary>
        /// <param name="_actualMachineview">Actual state in machine</param>
        protected override void SetView(GameType _actualMachineview)
        {
            if(_actualMachineview != view) return;
            machine.currentView = this;
        }
    }
}