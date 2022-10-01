using UI.Signals;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace UI.StateMachine.States.GameStates
{
    internal sealed class GamePauseView : GameView
    {
        [Inject] 
        private SignalBus signalBus;
        /// <summary>
        /// Example solution to show how changing view works.
        /// If you want to change state inside any view scripts like buttons or sub-views, use signal instead.
        /// <example>
        /// signalBus.TryFire(new ChangeStateSignal("Choose"));
        /// </example>
        /// </summary>
        [SerializeField] private Button backToMainButton;
        [SerializeField] private Button quitButton;
        private const string POPUP_NAME = "Choose";
        private const string POPUP_DESCRIPTION = "Are you sure you want to quit?";
        private const string MENU_SCENE = "Sce_Menu";
        public override void Awake()
        {
            view = GameType.Pause;
            base.Awake();
        }

        public override void Bind()
        {
            base.Bind();
            backToMainButton.onClick.AddListener(() =>
            {
                machine.stateMachine.Fire(GameTrigger.BackToMain);
            });
            quitButton.onClick.AddListener(ConfirmQuit);
        }

        public override void ExecuteEscapeBehaviour()
        {
            base.ExecuteEscapeBehaviour();
            machine.stateMachine.Fire(GameTrigger.BackToMain);
        }

        protected override void ConfigureMachineTransitions()
        {
            base.ConfigureMachineTransitions();
            machine.stateMachine.Configure(view)
                .Permit(GameTrigger.BackToMain, GameType.Main);
        }

        private void ConfirmQuit()
        {
            signalBus.TryFire(new FirePopupSignal(POPUP_NAME,
                POPUP_DESCRIPTION,
                null, 
                Quit));
        }

        private void Quit()
        {
            SceneManager.LoadScene(MENU_SCENE);
        }
    }
}