using UnityEngine;
using UnityEngine.UI;

namespace UI.StateMachine.States.GameStates
{
    internal sealed class GameInventoryView : GameView
    {
        [SerializeField] private Button backToMainButton;
        
        public override void Awake()
        {
            view = GameType.Inventory;
            base.Awake();
        }

        public override void Bind()
        {
            base.Bind();
            backToMainButton.onClick.AddListener(() =>
            {
                machine.stateMachine.Fire(GameTrigger.BackToMain);
            });
        }

        protected override void ConfigureMachineTransitions()
        {
            base.ConfigureMachineTransitions();
            machine.stateMachine.Configure(view)
                .Permit(GameTrigger.BackToMain, GameType.Main)
                .IgnoreNotPermittedTriggers();
        }

        public override void ExecuteEscapeBehaviour()
        {
            base.ExecuteEscapeBehaviour();
            machine.stateMachine.Fire(GameTrigger.BackToMain);
        }
    }
}