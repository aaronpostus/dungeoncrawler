
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Loading_Scripts
{
    public class ImageCycler : MonoBehaviour
    {
        [SerializeField] string xmlFileName;
        [SerializeField] Image imageOutput;
        // name of image (in ImageCyclerImages path) and duration in milliseconds
        private OrderedDictionary imagesWithDuration;
        private int totalDuration = 0;
        private void Awake()
        {
            imageOutput.preserveAspect = true;
            TextAsset file = Resources.Load<TextAsset>("Assets/Essentials/Created/XML/" + xmlFileName);
            XmlDocument xmlFile = new XmlDocument();
            xmlFile.LoadXml(file.text);

            XmlNodeList images = xmlFile.SelectNodes("images/image");
            this.imagesWithDuration = new OrderedDictionary();

            foreach (XmlNode image in images)
            {
                string imageName = image.Attributes["name"].Value;
                int imageDuration = int.Parse(image.Attributes["timeduration"].Value);
                this.imagesWithDuration.Add(imageName, imageDuration);
                totalDuration += imageDuration;
            }
        }
        public void Start()
        {
            StartCoroutine(CycleImages());
        }
        private IEnumerator CycleImages()
        {
            foreach (DictionaryEntry entry in imagesWithDuration)
            {
                var imageName = (string) entry.Key;
                var imageDuration = (int) entry.Value;
                Debug.Log(imageName + " : " + imageDuration);
                // Load the image from Resources or wherever it is stored
                var imageSprite = Resources.Load<Sprite>("XML/ImageCyclerImages/" + imageName);
                Debug.Log(imageSprite);

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
