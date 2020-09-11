using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    protected override void OnLoadSuccess()
    {
        base.OnLoadSuccess();
        m_CommonInput = InputManager.GetInstance();
        mCtrl = mCtrlBase as MainCityCtrl;

        mCtrl.txtLv.text = 20.ToString();
        mCtrl.Attribute.onClick.AddListener(OnclickAtteibute);
        mCtrl.AutoRelease(EventManager.OnZoomChange.Subscribe(OnZoom)); //订阅放缩事件

        m_CommonInput.OnContinuousZoomEvent.AddListener(OnScale);//监听Unity的放缩事件，然后将这个事件广播出去
    }

    private void OnZoom(float obj)
    {   // 在这里接受广播 调用摄像机管理器的方法
        MainCameraManager.GetInstance().Zoom(obj);
    }

    private void OnScale(float arg0)
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
