using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace FaceOff
{
    public static class FileDialog
    {
        [DllImport("user32.dll")]
        private static extern void OpenFileDialog();

        public static OpenFileDialog CreateOpenFileDialog()
        {
            return new OpenFileDialog();
        }
    }
}