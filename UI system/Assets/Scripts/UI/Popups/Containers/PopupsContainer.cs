using UnityEngine;

namespace UI.Popups.Containers
{
    [CreateAssetMenu(fileName = "PopupsContainer", menuName = "Containers/PopupsContainer", order = 1)]
    internal sealed class PopupsContainer : ScriptableObject
    {
        [SerializeField] private ScriptablePopup[] popups;

        public Transform GetPopupForInstantiating(string _popupName)
        {
            for (int i = 0; i < popups.Length; i++)
            {
                if (popups[i].popupName == _popupName)
                    return popups[i].popupTransform;
            }
            Debug.LogError($"There is no such popup type like {_popupName} in container!");
            return null;
        }
    }
}
