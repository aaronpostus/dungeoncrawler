
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Loading_Scripts
{
    public class ImageCycler : MonoBehaviour
    {
        [SerializeField] string xmlFileName;
        [SerializeField] Image imageOutput;
        // name of image (in ImageCyclerImages path) and duration in milliseconds
        private OrderedDictionary imageWithDuration;
        private int totalDuration = 0;
        private void Awake()
        {
            TextAsset file = Resources.Load<TextAsset>(xmlFileName);
            XmlDocument xmlFile = new XmlDocument();
            xmlFile.LoadXml(file.text);

            XmlNodeList images = xmlFile.SelectNodes("images/image");
            this.imageWithDuration = new OrderedDictionary();

            foreach (XmlNode image in images)
            {
                string imageName = image.Attributes["name"].Value;
                int imageDuration = int.Parse(image.Attributes["timeduration"].Value);
                this.imageWithDuration.Add(imageName, imageDuration);
                totalDuration += imageDuration;
            }
        }
        public void Start()
        {
            StartCoroutine(CycleImages());
        }
        private IEnumerator CycleImages()
        {
            foreach (DictionaryEntry entry in imageWithDuration)
            {
                string imageName = (string)entry.Key;
                int imageDuration = (int)entry.Value;

                // Load the image from Resources or wherever it is stored
                Sprite imageSprite = Resources.Load<Sprite>("XML/ImageCyclerImages/" + imageName);

                // Set the imageOutput to the loaded sprite
                imageOutput.sprite = imageSprite;

                // Wait for the specified duration before moving to the next image
                yield return new WaitForSeconds(imageDuration / 1000f);
            }
        }
        public int GetTotalDuration() {
            return totalDuration;
        }
    }
}
