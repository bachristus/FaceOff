using UnityEngine;

namespace FaceOff.GUI
{
    public class StatusView : View
    {
        [SerializeField] private TMPro.TMP_Text text;

        public void DisplayError(string message)
        {
            DisplayMessage(message, Color.red);
        }

        public void DisplayMessage(string message)
        {
            DisplayMessage(message, Color.black);
        }

        public void DisplayMessage(string message, Color textColor)
        {
            text.text = message;
            text.faceColor = textColor;
        }

        public override void ClearContent()
        {
            text.text = "";
        }
    }
}
