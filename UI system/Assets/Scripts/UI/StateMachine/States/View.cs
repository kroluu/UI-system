using System;
using DG.Tweening;
using UI.Interfaces;
using UI.StateMachine.Machines;
using UnityEngine;

namespace UI.StateMachine.States
{
    /// <summary>
    /// Generic abstract class <c>View</c> is representation of specific view in scene
    /// </summary>
    /// <typeparam name="TView">Generic parameter of enum type that defines view types. Each views representation has it's own view types depends on current scene</typeparam>
    /// <typeparam name="TTrigger">Generic parameter of enum type that defines view triggers. Each triggers representation has it's own trigger types depends on current scene</typeparam>
    [RequireComponent(typeof(CanvasGroup))]
    internal abstract class View<TView,TTrigger> : MonoBehaviour, IBindable where TView: struct, Enum where TTrigger: struct, Enum
    {
        [SerializeField, Min(0)] 
        private float timeForPanelAppearance = 0.25f;
        [SerializeField, Min(0)] 
        private float timeForPanelHide = 0.15f;
        
        /// <summary>
        /// State type
        /// </summary>
        protected TView view;
    
        /// <summary>
        /// State type getter
        /// </summary>
        public TView ViewType => view;
    
        /// <summary>
        /// Machine reference currently on scene
        /// </summary>
        protected UIMachine<TView, TTrigger> machine;

        /// <summary>
        /// Canvas group for state visibility
        /// </summary>
        private CanvasGroup canvasGroup;

        /// <summary>
        /// Controls visibility tween
        /// </summary>
        private Tween visibilityTween;
        public virtual void Awake()
        {
            ConfigureMachineTransitions();
            GetComponents();
            Bind();
        }

        public virtual void Bind()
        {
            machine.OnChangeView += SetView;
        }

        public virtual void UnBind()
        {
            machine.OnChangeView -= SetView;
        }

        protected void Start()
        {
            VisibilityValidation();
        }

        /// <summary>
        /// Hides state if machine does not apply to its own representation
        /// </summary>
        private void VisibilityValidation()
        {
            if(machine.currentView.ViewType.Equals(view)) return;
            HideView(true);
        }

        /// <summary>
        /// Gets components from objects
        /// </summary>
        protected virtual void GetComponents()
        {
            TryGetComponent(out canvasGroup);
        }

        public virtual void OnDestroy()
        {
            UnBind();
        }

        /// <summary>
        /// Configure machine transitions for its own representation
        /// </summary>
        protected virtual void ConfigureMachineTransitions()
        {
            machine.stateMachine.Configure(view)
                .OnEntry(() =>
                {
                    machine.OnChangeMachineState(view);
                    AppearView();
                })
                .OnExit(() =>
                {
                    HideView();
                });
        }

        /// <summary>
        /// Alternative to MonoBehaviour <c>Update</c>. Called when state is set in machine.
        /// </summary>
        public virtual void DoActions()
        {
        
        }

        /// <summary>
        /// Called when <c>Escape</c> key was pressed
        /// </summary>
        public virtual void ExecuteEscapeBehaviour()
        {
        }

        /// <summary>
        /// Called when <c>Tab</c> key was pressed
        /// </summary>
        public virtual void ExecuteTabBehaviour()
        {
        }

        protected abstract void SetView(TView _actualMachineview);

        /// <summary>
        /// Appears state representation on UI
        /// </summary>
        /// <param name="_instantShow">Appears state instantly</param>
        protected virtual void AppearView(bool _instantShow = false)
        {
            ClearVisibilityTween();
            visibilityTween = canvasGroup.DOFade(1f, _instantShow ? 0f: timeForPanelAppearance).OnStart(() =>
            {
                canvasGroup.blocksRaycasts = true;
            });
        }

        /// <summary>
        /// Hides state on UI
        /// </summary>
        /// <param name="_instantShow">Hides state instantly</param>
        protected virtual void HideView(bool _instantShow = false)
        {
            ClearVisibilityTween();
            visibilityTween = canvasGroup.DOFade(0f, _instantShow ? 0f: timeForPanelHide).OnStart(() =>
            {
                canvasGroup.blocksRaycasts = false;
            });
        }

        private void ClearVisibilityTween()
        {
            visibilityTween.Kill();
        }

    }
}