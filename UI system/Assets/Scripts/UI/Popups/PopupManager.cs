using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Inputs;
using JetBrains.Annotations;
using UI.Interfaces;
using UI.Popups.Containers;
using UI.Popups.Interfaces;
using UI.Popups.Types;
using UI.Signals;
using UnityEngine;
using Zenject;

namespace UI.Popups
{
    [DisallowMultipleComponent]
    internal sealed class PopupManager : MonoBehaviour, IBindable
    {
        /// <summary>
        /// Container for all popup types
        /// </summary>
        [Inject]
        private PopupsContainer popupsContainer;
        /// <summary>
        /// Input's detection
        /// </summary>
        [Inject]
        private PopupInputDetection popupInputDetection;
        /// <summary>
        /// Factory for instantiating popup prefabs and injecting instances
        /// </summary>
        [Inject] 
        private Popup.Factory popupFactory;
        private InputReader inputReader;
        /// <summary>
        /// Current spawned popup
        /// </summary>
        [CanBeNull] private static IPopupVisibility PopupVisibility;
        
        /// <summary>
        /// Queues holding popups to spawn
        /// </summary>
        private readonly Queue<QueuePopup> queuePopups = new();
        private static bool IsPopupFired => PopupVisibility is not null;

        private void Awake()
        {
            Bind();
        }

        private void OnDestroy()
        {
            inputReader.InputsForUI();
            UnBind();
            ResetStaticFields();
        }

        private void ResetStaticFields()
        {
            PopupVisibility = null;
        }
        
        [Inject]
        private void InputsInit(InputReader _inputReader)
        {
            inputReader = _inputReader;
        }
        
        public void Bind()
        {
            popupInputDetection.OnQuitPressed += TryClose;
        }

        public void UnBind()
        {
            popupInputDetection.OnQuitPressed -= TryClose;
        }

        public void ReceiveFirePopupSignal(FirePopupSignal _firePopupSignal)
        {
            TryFire(_firePopupSignal.FirePopupName,new PopupParameter(_firePopupSignal.FirePopupTranslateText, _firePopupSignal.OnDeclineAction,
                _firePopupSignal.OnAcceptAction));
        }

        public void ReceiveFirePopupSignal(FirePopupWithEnqueueSignal _firePopupSignal)
        {
            TryFireOrEnqueue(_firePopupSignal.FirePopupName,new PopupParameter(_firePopupSignal.FirePopupTranslateText, _firePopupSignal.OnDeclineAction,
                _firePopupSignal.OnAcceptAction));
        }

        public void ReceiveClosePopupSignal(ClosePopupSignal _closePopupSignal)
        {
            TryClose();
        }

        /// <summary>
        /// Enqueues popups
        /// <example>
        /// <code>
        ///    if(!TryFire(PopupType.SampleType))
        ///        Enqueue(PopupType.SampleType) 
        /// </code>
        /// </example>
        /// </summary>
        private void EnqueuePopup(QueuePopup _queuePopup)
        {
            queuePopups.Enqueue(_queuePopup);
        }

        /// <summary>
        /// Tries to fire popup. Fires if no popup is on screen, false otherwise
        /// </summary>
        /// <param name="_popupMeanToFire">Type of popup mean to fire</param>
        /// <param name="_popupParameter"></param>
        /// <returns>Whether popup has been fired or not</returns>
        public bool TryFire(string _popupMeanToFire, PopupParameter _popupParameter = null)
        {
            if (IsPopupFired) return false;
            inputReader.InputsForPopup();
            Fire(_popupMeanToFire, _popupParameter);
            return true;
        }

        public void TryFireOrEnqueue(string _popupMeanToFire, PopupParameter _popupParameter=null)
        {
            if (IsPopupFired)
            {
                EnqueuePopup(new QueuePopup(_popupMeanToFire,_popupParameter));
                return;
            }
            
            inputReader.InputsForPopup();
            Fire(_popupMeanToFire, _popupParameter);
        }

        /// <summary>
        /// Fires popup on screen
        /// </summary>
        /// <param name="_popupToFire">Type of popup mean to fire</param>
        /// <param name="_popupParameter"></param>
        private void Fire(string _popupToFire, PopupParameter _popupParameter = null)
        {
            if (popupsContainer.GetPopupForInstantiating(_popupToFire) is { } popup 
                && popup.TryGetComponent(out PopupVisibility))
            {
                PopupVisibility = popupFactory.Create(popup);
                //Instantiate(popup, /*uiGameMachine.*/transform).TryGetComponent(out PopupVisibility);
                PopupVisibility!.ParentTo(transform).Init(_popupParameter).Open();
            }
        }

        /// <summary>
        /// Tries to close already opened popup
        /// </summary>
        public void TryClose()
        {
            if (!IsPopupFired) return;
            
            Close();
        }

        /// <summary>
        /// Checks if any popup is queued
        /// </summary>
        /// <returns>Whether queue contains any popup</returns>
        private bool CheckIfPopupQueued() => queuePopups.Any();

        /// <summary>
        /// Closes popup in time, destroys at the end and fires popup from queue if any exist
        /// </summary>
        private void Close()
        {
            PopupVisibility!.Close().OnComplete(()=>
            {
                DestroyPopup();
                if (CheckIfPopupQueued())
                {
                    QueuePopup queuePopup = queuePopups.Dequeue();
                    Fire(queuePopup.popupType, queuePopup.popupParameter);
                    return;
                }
                inputReader.InputsForUI();
            });
        }

        /// <summary>
        /// Destroys visible popup and nulls <c>popupVisibility</c>
        /// </summary>
        private void DestroyPopup()
        {
            PopupVisibility!.DestroyPopup();
            PopupVisibility = null;
        }
    }
}
