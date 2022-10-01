using DG.Tweening;
using UI.Interfaces;
using UI.Popups.Interfaces;
using UI.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Popups.Types
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CanvasGroup))]
    internal abstract class Popup : MonoBehaviour, IPopupVisibility, IBindable
    {
        internal class Factory : PlaceholderFactory<Transform, IPopupVisibility>{}

        internal class PopupFactory : IFactory<Transform, IPopupVisibility>
        {
            private readonly DiContainer container;

            public PopupFactory(DiContainer _diContainer)
            {
                container = _diContainer;
            }

            public IPopupVisibility Create(Transform _prefab)
            {
                return container.InstantiatePrefabForComponent<IPopupVisibility>(_prefab);
            }
        }
        
        [SerializeField] protected Button closeButton;
        [SerializeField] protected CanvasGroup canvasGroup;
        [SerializeField] protected RectTransform popupContentRect;
        [SerializeField] protected float openTime;
        [SerializeField] protected float closeTime;

        [Inject]
        private SignalBus signalBus;
        public abstract IPopupVisibility Init(in PopupParameter _popupParameter);
        public IPopupVisibility ParentTo(Transform _parent)
        {
            RectTransform popupTransform = transform as RectTransform;
            Vector3 spawnPosition = popupTransform!.anchoredPosition;
            popupTransform.SetParent(_parent);
            popupTransform.anchoredPosition = spawnPosition;
            return this;
        }

        public abstract void Open();
        public abstract Tween Close();
        public void DestroyPopup()
        {
            Destroy(gameObject);
        }
        
        public void Awake()
        {
            Bind();
        }

        public void OnDestroy()
        {
            UnBind();
        }

        public virtual void Bind()
        {
            closeButton.onClick.AddListener(ClosePopup);
        }

        public virtual void UnBind()
        {
            closeButton.onClick = null;
        }

        protected void ClosePopup()
        {
            signalBus.TryFire<ClosePopupSignal>();
        }
    }
}