using UnityEngine;
using UnityEngine.EventSystems;

namespace SimpleMOBA.UI
{
    /// <summary>
    /// Virtual joystick that appears where the player presses on the left side of the screen.
    /// </summary>
    public class MobileJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [SerializeField] private RectTransform handle;
        [SerializeField] private RectTransform background;
        [SerializeField] private float maxRadius = 60f;

        private Vector2 startPosition;
        private bool isHeld;

        public Vector2 Direction { get; private set; }

        private void Awake()
        {
            if (background != null)
            {
                background.gameObject.SetActive(false);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            isHeld = true;
            startPosition = eventData.position;
            if (background != null)
            {
                background.position = startPosition;
                background.gameObject.SetActive(true);
            }

            UpdateHandle(eventData.position);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!isHeld)
            {
                return;
            }

            UpdateHandle(eventData.position);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isHeld = false;
            Direction = Vector2.zero;
            if (handle != null)
            {
                handle.anchoredPosition = Vector2.zero;
            }

            if (background != null)
            {
                background.gameObject.SetActive(false);
            }
        }

        private void UpdateHandle(Vector2 position)
        {
            var delta = position - startPosition;
            delta = Vector2.ClampMagnitude(delta, maxRadius);
            Direction = delta / maxRadius;

            if (handle != null)
            {
                handle.anchoredPosition = delta;
            }
        }
    }
}
