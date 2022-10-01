using System;
using DG.Tweening;
using Extensions;
using TMPro;
using UI.Popups.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popups.Types
{
    internal sealed class ChoosePopup : Popup
    {
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private Button acceptButton;
        [SerializeField] private Button declineButton;

        private Action acceptAction;
        private Action declineAction;
        
        public override IPopupVisibility Init(in PopupParameter _popupParameter)
        {
            if (_popupParameter.IsNull())
                return this;
            titleText.text = _popupParameter.TranslateText;
            acceptAction = _popupParameter.OnAcceptAction;
            declineAction = _popupParameter.OnDeclineAction;
            return this;
        }

        public override void Bind()
        {
            acceptButton.onClick.AddListener(()=>
            {
                acceptAction?.Invoke();
                ClosePopup();
            });
            declineButton.onClick.AddListener(()=>
            {
                declineAction?.Invoke();
                ClosePopup();
            });
            base.Bind();
        }

        public override void UnBind()
        {
            acceptButton.onClick = null;
            declineButton.onClick = null;
            base.UnBind();
        }

        public override void Open()
        {
            canvasGroup.DOFade(1f, openTime).OnStart(()=>canvasGroup.blocksRaycasts=true);
        }

        public override Tween Close()
        {
            return canvasGroup.DOFade(0f, closeTime).OnStart(() => canvasGroup.blocksRaycasts = false);
        }
    }
}
