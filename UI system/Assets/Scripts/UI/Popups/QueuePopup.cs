namespace UI.Popups
{
    internal class QueuePopup
    {
        public readonly string popupType;
        public readonly PopupParameter popupParameter;

        public QueuePopup(string _popupType, PopupParameter _popupParameter)
        {
            popupType = _popupType;
            popupParameter = _popupParameter;
        }
    }
}
