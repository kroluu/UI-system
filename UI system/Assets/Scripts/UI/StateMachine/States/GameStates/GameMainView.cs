using UnityEngine;
using UnityEngine.UI;

namespace UI.StateMachine.States.GameStates
{
    internal sealed class GameMainView : GameView
    {

        [SerializeField] private Button openPauseViewButton;
        [SerializeField] private Button openInventoryViewButton;
        
        public override void Awake()
        {
            machine.currentView = this;
            view = GameType.Main;
            base.Awake();
        }

        public override void Bind()
        {
            base.Bind();
            openPauseViewButton.onClick.AddListener(() =>
            {
                machine.stateMachine.Fire(GameTrigger.OpenPause);
            });
            openInventoryViewButton.onClick.AddListener(() =>
            {
                machine.stateMachine.Fire(GameTrigger.OpenInventory);
            });
        }

        protected override void ConfigureMachineTransitions()
        {
            base.ConfigureMachineTransitions();
            machine.stateMachine.Configure(view)
                .Permit(GameTrigger.OpenPause, GameType.Pause)
                .Permit(GameTrigger.OpenInventory, GameType.Inventory)
                .IgnoreNotPermittedTriggers();
        }

        public override void ExecuteEscapeBehaviour()
        {
            base.ExecuteEscapeBehaviour();
            machine.stateMachine.Fire(GameTrigger.OpenPause);
        }
        
    }
}