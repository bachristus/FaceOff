using UnityEngine;

namespace FaceOff.GUI
{
    public abstract class View: MonoBehaviour
    {
        private void OnEnable()
        {
            ClearContent();
        }

        public virtual void ClearContent() { }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}