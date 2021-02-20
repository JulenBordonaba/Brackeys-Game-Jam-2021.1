using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace ExtraUI
{

    public class Slider : Selectable, IDragHandler, IEventSystemHandler, IInitializePotentialDragHandler, ICanvasElement
    {
        public SliderMode mode = SliderMode.filled;

        public Image fillImage;


        public float minValue = 0;
        public float maxValue = 1;

        [Range(0f, 1f)]
        [SerializeField]
        private float _value;

        
        public UnityEvent OnPointerEnterEvent = new UnityEvent();
        public UnityEvent OnValueChanged = new UnityEvent();

        protected void Update()
        {
            switch (mode)
            {
                case SliderMode.filled:
                    FilledUpdate();
                    break;
                case SliderMode.scaled:
                    ScaledUpdate();
                    break;
                default:
                    break;
            }
        }

        private void ScaledUpdate()
        {

        }

        private void FilledUpdate()
        {
            fillImage.type = Image.Type.Filled;
            fillImage.fillAmount = Value / maxValue;
        }

        public void OnDrag(PointerEventData pointerEventData)
        {
            Value = SetValue(pointerEventData);
        }



        public void OnInitializePotentialDrag(PointerEventData pointerEventData)
        {

        }

        public void Rebuild(CanvasUpdate canvasUpdate)
        {

        }

        public void LayoutComplete()
        {

        }

        public void GraphicUpdateComplete()
        {

        }

        private float SetValue(PointerEventData data)
        {
            float x = data.position.x;



            float aux = ((x - (Left + (Screen.width / 2))) / Width) * maxValue;

            return Mathf.Clamp(aux, minValue, maxValue);

        }


        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            OnPointerEnterEvent?.Invoke();
        }

        private float Width
        {
            get
            {
                return Right - Left;
            }
        }

        private float Left
        {
            get
            {
                return fillImage.rectTransform.rect.xMin;
            }
        }

        private float Right
        {
            get
            {
                return fillImage.rectTransform.rect.xMax;
            }
        }

        public float Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (_value == value) return;

                _value = value;

#if UNITY_EDITOR
                OnValueChanged.Invoke();
#endif
            }
        }

    }
}
