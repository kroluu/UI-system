using UI.Signals;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace UI.StateMachine.States.MenuStates
{
    internal sealed class MenuMainView : MenuView
    {
        [Inject] private SignalBus signalBus;
        [SerializeField] private Button openOptionsViewButton;
        [SerializeField] private Button confirmQuitButton;
        [SerializeField] private Button playButton;
        
        private const string POPUP_NAME = "Choose";
        private const string POPUP_DESCRIPTION = "Are you sure you want to quit?";
        private const string SCENE_GAME = "Sce_Game";
        public override void Awake()
        {
            machine.currentView = this;
            view = MenuType.Main;
            base.Awake();
        }

        public override void Bind()
        {
            base.Bind();
            openOptionsViewButton.onClick.AddListener(() =>
            {
                machine.stateMachine.Fire(MenuTrigger.ToOptions);
            });
            confirmQuitButton.onClick.AddListener(ConfirmQuit);
            playButton.onClick.AddListener(Play);
        }

        protected override void ConfigureMachineTransitions()
        {
            base.ConfigureMachineTransitions();
            machine.stateMachine.Configure(view)
                .Permit(MenuTrigger.ToOptions, MenuType.Options)
                .IgnoreNotPermittedTriggers();
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
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }

        private void Play()
        {
            SceneManager.LoadScene(SCENE_GAME);
        }
    }
}
