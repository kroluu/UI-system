using UnityEngine;
using UnityEngine.UI;

namespace UI.StateMachine.States.MenuStates
{
    internal sealed class MenuOptionsView : MenuView
    {
        [SerializeField] private Button backButton;
        
        public override void Awake()
        {
            view = MenuType.Options;
            base.Awake();
        }

        public override void Bind()
        {
            base.Bind();
            backButton.onClick.AddListener(() =>
            {
                machine.stateMachine.Fire(MenuTrigger.BackToMain);
            });
        }

        protected override void ConfigureMachineTransitions()
        {
            base.ConfigureMachineTransitions();
            machine.stateMachine.Configure(view)
                .Permit(MenuTrigger.BackToMain, MenuType.Main)
                .IgnoreNotPermittedTriggers();
        }

        public override void ExecuteEscapeBehaviour()
        {
            base.ExecuteEscapeBehaviour();
            machine.stateMachine.Fire(MenuTrigger.BackToMain);
        }
    }
}