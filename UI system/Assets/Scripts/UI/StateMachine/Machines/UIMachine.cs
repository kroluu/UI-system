using System;
using Inputs;
using Stateless;
using UI.Interfaces;
using UI.Signals;
using UI.StateMachine.States;
using UnityEngine;
using Zenject;

namespace UI.StateMachine.Machines
{
    /// <summary>
    /// Generic abstract class <c>UIMachine</c> that manages changing views in scene 
    /// </summary>
    /// <typeparam name="TView">Generic parameter of enum type that defines states in machine. Each machine has it's own enum states</typeparam>
    /// <typeparam name="TTrigger">Generic parameter of enum type that defines triggers in machine. Each machine has it's own enum states</typeparam>
    [DisallowMultipleComponent]
    internal abstract class UIMachine<TView,TTrigger> : MonoBehaviour,IBindable,IInheritAwake where TView: struct, Enum where TTrigger: struct, Enum
    {
        /// <summary>
        /// State machine that holds UI states and their triggers
        /// </summary>
        public readonly StateMachine<TView, TTrigger> stateMachine = new(default);
    
        /// <summary>
        /// Current UI state in scene
        /// </summary>
        public View<TView,TTrigger> currentView;
    
        /// <summary>
        /// Event holding methods that checks if changed state applies to it's state 
        /// </summary>
        public event Action<TView> OnChangeView;
        
        /// <summary>
        /// Input's detector
        /// </summary>
        [Inject]
        private UIInputDetection uiInputDetection;

        private void Awake()
        {
            InheritAwake();
            AssignComponents();
            Bind();
        }
        
        public virtual void InheritAwake()
        {
        }

        private void OnDestroy()
        {
            UnBind();
        }

        protected virtual void AssignComponents()
        {
        }
        
        public void ReceiveChangeViewSignal(ChangeStateSignal _changeViewSignal)
        {
            if (Enum.TryParse(_changeViewSignal.TriggerName, true, out TTrigger triggerToFire))
            {
                stateMachine.Fire(triggerToFire);
            }
        }
    
        /// <summary>
        /// Invokes all methods subscribed to event
        /// </summary>
        /// <param name="_stateToSet"></param>
        public void OnChangeMachineState(TView _stateToSet)
        {
            OnChangeView?.Invoke(_stateToSet);
        }

        protected virtual void Update()
        {
            if (currentView is {})
            {
                currentView.DoActions();
            }
        }
        

        public void Bind()
        {
            uiInputDetection.OnEscapePressed += EscapeDetection;
            uiInputDetection.OnTabPressed += TabDetection;
        }

        public void UnBind()
        {
            uiInputDetection.OnEscapePressed -= EscapeDetection;
            uiInputDetection.OnTabPressed -= TabDetection;
        }

        private void EscapeDetection()
        {
            currentView.ExecuteEscapeBehaviour();
        }

        private void TabDetection()
        {
            currentView.ExecuteTabBehaviour();
        }
    }
}