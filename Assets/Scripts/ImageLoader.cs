using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace FaceOff {
    public class ImageLoader : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void PickImageFromFile(Image image)
        {

            string[] filters = new[] { "Image files", "png,jpg,jpeg,bmp", "All files", "*" };
            string path = EditorUtility.OpenFilePanelWithFilters("Pick avatar picture", "", filters);

            byte[] fileContent = null;
            if (path.Length != 0)
            {
                fileContent = File.ReadAllBytes(path);
                PutBitmapIntoImage(fileContent, image);
            }

            //FileDialog.CreateOpenFileDialog().ShowDialog();
        }

        public void PutBitmapIntoImage(byte[] bitmap, Image image)
        {
            Texture2D texture = new Texture2D(1920, 1280);
            texture.LoadImage(bitmap);
            image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.0f), 100.0f);
        }

        internal byte[] GetTexturePNG(Image image)
        {
            return (image.mainTexture as Texture2D)?.EncodeToPNG();
        }
    }
}