using UnityEngine;

namespace FaceOff
{
    public abstract class Form: MonoBehaviour
    {
        [SerializeField] private TMPro.TMP_Text Error;
        public virtual void DisplayError(string message) {
            if (Error == null) return;
            Error.text = message;
        }            

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