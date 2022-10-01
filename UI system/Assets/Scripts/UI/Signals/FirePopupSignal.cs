using System;

namespace UI.Signals
{
    public class FirePopupSignal
    {
        public FirePopupSignal(
            string _firePopupName,
            string _firePopupTranslateText,
            Action _onDeclineAction, 
            Action _onAcceptAction)
        {
            FirePopupName = _firePopupName;
            FirePopupTranslateText = _firePopupTranslateText;
            OnDeclineAction = _onDeclineAction;
            OnAcceptAction = _onAcceptAction;
        }
        
        public string FirePopupName { get; }
        public string FirePopupTranslateText { get; }
        public Action OnDeclineAction { get; }
        public Action OnAcceptAction { get; }
    }
}
