using System;

namespace UI.Signals
{
    public sealed class FirePopupWithEnqueueSignal : FirePopupSignal
    {
        public FirePopupWithEnqueueSignal(string _firePopupName,
            string _firePopupTranslateText,
            Action _onDeclineAction,
            Action _onAcceptAction) : base(_firePopupName,
            _firePopupTranslateText,
            _onDeclineAction,
            _onAcceptAction)
        {
        }
    }
}