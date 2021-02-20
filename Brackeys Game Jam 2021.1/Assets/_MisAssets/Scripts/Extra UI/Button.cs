using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ExtraUI
{
    
    public class Button : Selectable, IPointerClickHandler, IEventSystemHandler, ISubmitHandler
    {

        public UnityEvent OnClick = new UnityEvent();
        public UnityEvent OnPointerEnterEvent = new UnityEvent();

        public void OnPointerClick(PointerEventData pointerEventData)
        {
            OnClick?.Invoke();
        }

        public void OnSubmit(BaseEventData baseEventData)
        {
            OnClick?.Invoke();
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            OnPointerEnterEvent?.Invoke();
        }


    }
}


