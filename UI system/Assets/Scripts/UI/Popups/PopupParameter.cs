using System;

namespace UI.Popups
{
    internal sealed class PopupParameter
    {
        public string TranslateText { get; }
        public Action OnDeclineAction { get; }
        public Action OnAcceptAction { get; }
        
        public PopupParameter(string _translateText, Action _onDeclineAction, Action _onAcceptAction)
        {
            TranslateText = _translateText;
            OnDeclineAction = _onDeclineAction;
            OnAcceptAction = _onAcceptAction;
        }
    }
}