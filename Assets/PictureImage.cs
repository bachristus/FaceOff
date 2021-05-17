using FaceOff.DataObjects;
using UnityEngine;
using UnityEngine.UI;

namespace FaceOff.GUI
{
    public class PictureImage : MonoBehaviour
    {
        public Sprite NoImageSprite;
        public Image Image;

        public Picture Picture
        {
            get
            {
                return new Picture(GetTexturePNG(Image));
            }

            set
            {
                if (value == null||value.Bytes==null)
                {
                    Image.sprite = NoImageSprite;
                }
                else 
                {
                    PutBitmapIntoImage(value.Bytes, Image);
                }
            }
        }

        private static void PutBitmapIntoImage(byte[] bitmap, Image image)
        {

            Texture2D texture = new Texture2D(1920, 1280);
            texture.LoadImage(bitmap);
            image.preserveAspect = true;
            image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.0f), 100.0f);
            
        }

        private static byte[] GetTexturePNG(Image image)
        {
            var texture = image.mainTexture as Texture2D;
            if (texture == null) return null;
            return texture.EncodeToPNG();
        }
    }
}
