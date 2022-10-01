using DG.Tweening;
using UnityEngine;

namespace UI.Popups.Interfaces
{
    internal interface IPopupVisibility
    {
        IPopupVisibility Init(in PopupParameter _popupParameter);
        IPopupVisibility ParentTo(Transform _parent);
        void Open();
        Tween Close();
        void DestroyPopup();
    }
}
