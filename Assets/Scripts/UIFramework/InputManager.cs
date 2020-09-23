using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.EventTrigger;

//自定义事件类，继承Unity内置事件
public class DoubleFingerEvent : UnityEvent<float> { } //Unity内置双指事件
public class ZoomEvent : UnityEvent<bool> { } //unity内置事件


public class InputManager :    MonoBehaviour, IPointerEnterHandler,
                               IPointerExitHandler,
                               IPointerDownHandler,
                               IPointerUpHandler,
                               IMoveHandler,
                               IDragHandler,
                               IBeginDragHandler,
                               IEndDragHandler,
                               IPointerClickHandler
{

    //以下是一些事件的参数
    // Variables used for continuous zoom events.
    private float touch_distance_ = 0;
    private bool zooming_ = false;

    // Variables used for discrete zoom event.
    private float ZoomSensitivity = 100f;
    private float zoom_distance_ = 0f;

#if UNITY_EDITOR
    private float mouse_scroll_difference_ = 0f;
#endif


    /// <summary>
    /// 放缩事件
    /// </summary>
    public DoubleFingerEvent OnContinuousZoomEvent { get; } = new DoubleFingerEvent(); 

    /// <summary>
    /// 拖动事件
    /// </summary>
    public TriggerEvent OnDragEvent { get; } = new TriggerEvent();

    /// <summary>
    /// 放缩停止事件
    /// </summary>
    public UnityEvent OnEndContinuousZoomEvent { get; } = new UnityEvent();


    //下面这些方法，都是Unity内置的监听，是因为这个类实现了一些UI方面的接口，所以必须实现

    public void OnBeginDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        if (zooming_)
            return;

        OnDragEvent.Invoke(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnMove(AxisEventData eventData)
    {   

        throw new System.NotImplementedException();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 1)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            var current_distance = (touch0.position - touch1.position).magnitude;

            if (touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began)
            {
                zooming_ = true;
                touch_distance_ = current_distance;
                zoom_distance_ = current_distance;

            }
            else if ((touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved))
            {
                var distance_delta = touch_distance_ - current_distance;
                touch_distance_ = current_distance;
                OnContinuousZoomEvent.Invoke(distance_delta);
            }

        }
        else
        {
            if (zooming_)
            {
                OnEndContinuousZoomEvent.Invoke();
                zooming_ = false;
            }
        }
#if UNITY_EDITOR
        if (Mathf.Abs(Input.mouseScrollDelta.y) > 0.001f)
        {
            OnContinuousZoomEvent.Invoke(Input.mouseScrollDelta.y * 10);
            zooming_ = true;

        }
        else
        {
            if (zooming_)
            {
                OnEndContinuousZoomEvent.Invoke();
                zooming_ = false;
            }
        }
#endif

    }
}
