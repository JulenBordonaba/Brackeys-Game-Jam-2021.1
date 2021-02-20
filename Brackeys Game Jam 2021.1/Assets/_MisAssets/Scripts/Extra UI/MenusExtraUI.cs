using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace ExtraUI
{
    public class ExtraUIMenus
    {
        public static GameObject SelectedGameObject
        {
            get
            {
                return Selection.activeGameObject;
            }
        }


        [MenuItem("GameObject/Extra\u00A0UI/Slider", false, 0)]
        public static void CreateExtraSlider()
        {
            GameObject currentSelected = SelectedGameObject;

            GameObject sliderObject = new GameObject("Slider", typeof(Slider), typeof(RectTransform));
            GameObject backgroundObject = new GameObject("Background", typeof(Image), typeof(RectTransform));
            GameObject fillObject = new GameObject("Fill", typeof(Image), typeof(RectTransform));


            fillObject.transform.SetParent(backgroundObject.transform);
            backgroundObject.transform.SetParent(sliderObject.transform);

            Slider mySlidier = sliderObject.GetComponent<Slider>();
            Image backgroundImage = backgroundObject.GetComponent<Image>();
            Image fillImage = fillObject.GetComponent<Image>();
            RectTransform sliderTransform = sliderObject.GetComponent<RectTransform>();
            RectTransform backgroundTransform = backgroundObject.GetComponent<RectTransform>();
            RectTransform fillTransform = fillObject.GetComponent<RectTransform>();

            mySlidier.targetGraphic = fillImage;
            mySlidier.fillImage = fillImage;
            sliderTransform.sizeDelta = new Vector2(160, 20);

            backgroundTransform.anchorMin = new Vector2(0, 0.25f);
            backgroundTransform.anchorMax = new Vector2(1, 0.75f);
            backgroundTransform.pivot = new Vector2(0.5f, 0.5f);
            backgroundTransform.SetBorders(0);

            fillTransform.anchorMin = new Vector2(0, 0.25f);
            fillTransform.anchorMax = new Vector2(1, 0.75f);
            fillTransform.pivot = new Vector2(0.5f, 0.5f);
            fillTransform.SetBorders(0);

            if (currentSelected!=null)
            {
                sliderObject.transform.SetParent(currentSelected.transform);
            }
        }

        [MenuItem("GameObject/Extra\u00A0UI/Button", false, 0)]
        public static void CreateExtraButton()
        {

        }

        [MenuItem("GameObject/Extra\u00A0UI/Button (Text Mesh Pro)", false, 0)]
        public static void CreateExtraButtonTextMeshPro()
        {

        }
        
    }
}
