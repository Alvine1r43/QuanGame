using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainCityScreenParam: UIOpenScreenParameterBase
{

}


public class MainCityScreen : ScreenBase
{
    private InputManager m_CommonInput;//游戏中监控输入输出的组件


    MainCityCtrl mCtrl;

    public MainCityScreen(UIOpenScreenParameterBase param = null) : base(UIConst.UIMainCity,param)
    {
        
    }
    public override void StartLoad(string UIName, UIOpenScreenParameterBase param = null)
    {
        mStrUIName = UIName;
        mOpenParam = param;
        ResourcesMgr.GetInstance().LoadAsset<GameObject>("InputListener", InputListenerLoadComplete);
        ResourcesMgr.GetInstance().LoadAsset<GameObject>(UIName, PanelLoadComplete);
    }

    private void InputListenerLoadComplete(GameObject obj)
    {
        mPanelRoot = UnityEngine.Object.Instantiate(obj, GameUIManager.GetInstance().GetUIRootTransform());
        var camera = GameUIManager.GetInstance().GetUICamera();
        if (camera != null)
        {
            mPanelRoot.GetComponent<Canvas>().worldCamera = camera;
        }
        m_CommonInput = mPanelRoot.GetComponent<InputManager>();

    }

    protected override void OnLoadSuccess()
    {
        base.OnLoadSuccess();

        mCtrl = mCtrlBase as MainCityCtrl;

        mCtrl.txtLv.text = 20.ToString();
        mCtrl.Attribute.onClick.AddListener(OnclickAtteibute);
        mCtrl.AutoRelease(EventManager.OnZoomChange.Subscribe(OnZoom)); //订阅放缩事件
        mCtrl.AutoRelease(EventManager.OnDrag.Subscribe(OnDrag)); //订阅放缩事件

        m_CommonInput.OnContinuousZoomEvent.AddListener(OnScaleAndBroadcast);//监听Unity的放缩事件，然后将这个事件用自己写的事件广播出去
        m_CommonInput.OnDragEvent.AddListener(OnDragAndBroadcast);//监听Unity的Drag事件，广播出去
    }

    private void OnDrag(PointerEventData obj)
    {
        MainCameraManager.GetInstance().Drag(obj);
    }

    private void OnZoom(float obj)
    {   // 在这里接受广播 调用摄像机管理器的方法,当然，摄像机订阅事件也可以
        MainCameraManager.GetInstance().Zoom(obj);
    }


    private void OnDragAndBroadcast(BaseEventData arg0)
    {
        EventManager.OnDrag.BroadCastEvent((PointerEventData)arg0);
    }
    private void OnScaleAndBroadcast(float arg0)
    {
        // 如果当前界面不是主界面则不会广播.todo 这个逻辑需要优化 
        // 这里只是把接收到的Unity消息广播出去
        //if (GameUIManager.mTypeScreens.Count != 1) return;
        EventManager.OnZoomChange.BroadCastEvent(arg0);
    }

    private void OnclickAtteibute()
    {
        var param = new AttributeParam
        {
            Des = "您的幸运值:MAX"
        };
        GameUIManager.GetInstance().OpenUI(typeof(AttributeScreen), param);
    }

    protected override void UIAdapt(Vector2Int res)
    {
        Debug.Log(string.Format("分辨率发生了变化，宽为{0},高为{1}", res.x, res.y));
    }

}
