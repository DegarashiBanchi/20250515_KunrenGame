// Selectableを単体で付与するためのスクリプト

using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomSelectable : Selectable, ISubmitHandler, ICancelHandler, IPointerClickHandler, ISelectHandler
{
    public UnityEvent customSubmitEvent = new UnityEvent();
    public UnityEvent customPointerClickEvent = new UnityEvent();
    public UnityEvent customCancelEvent = new UnityEvent();
    public UnityEvent customSelectEvent = new UnityEvent();


    public void OnSubmit(BaseEventData eventData)
    {
        customSubmitEvent?.Invoke();
    }

    public void OnCancel(BaseEventData eventData)
    {
        customCancelEvent?.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        customPointerClickEvent?.Invoke();
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData); // 基底クラスのOnSelectを呼び出すことで、Selectableが正常に起動するようにしておく。
        customSelectEvent?.Invoke();
    }
}