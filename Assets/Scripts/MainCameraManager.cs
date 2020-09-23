using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainCameraManager : MonoSingleton<MainCameraManager>
{
    // maincamera
    Camera mainCamera;
    public Camera MainCamera { get => mainCamera; }

    public Transform m_CameraTrans;
    /// <summary>
    /// 是否激活鼠标控制拖拽和缩进功能
    /// </summary>
    public bool m_ActiveMouseCtrl = true;

    /// <summary>
    /// 平行投影
    /// </summary>
    bool m_Orthographic;

    /// <summary>
    /// 拖动速度
    /// </summary>
    float m_DragSpeed;


    /// <summary>
    /// 拖动速度系数
    /// </summary>
    const float m_DragSpeedCoefficient = 2.0f;

    /// <summary>
    /// 缩放系数
    /// </summary>
    const float ZoomFactor = 0.05f;
    const float OrthSizeFactor = 0.05f;


    /// <summary>
    /// 摄像机俯仰角度
    /// </summary>
    const float Pitch = 45.0f;

    /// <summary>
    /// 一些摄像机常量参数
    /// </summary>
    const float DefaultD = 14.0f;
    const float MinD0 = 9f;
    public const float MinD = 9.5f;
    const float MaxD = 14f;
    const float SpringBackDSpeed = 10.0f;

    /// <summary>
    /// OrthographicSize Range
    /// </summary>
    const float DefaultOrthSize = 15.0f;
    const float MinOrthSize0 = 5;
    const float MinOrthSize = 6;
    const float MaxOrthSize = 19f;
    const float SpringBackOrthSizeSpeed = 10.0f;
    const float DefaultUISize = 10.5f;



    /// <summary>
    /// Z值
    /// </summary>
    float m_Y;



    protected override void Init()
    {
        mainCamera = gameObject.GetComponent<Camera>();
        m_Orthographic = mainCamera.orthographic; //是否平行投影
        m_Y = DefaultD * Mathf.Sin(Pitch * Mathf.Deg2Rad);
        m_CameraTrans = MainCamera.transform;
        m_DragSpeed = CalcDragSpeed();
    }

    /// <summary>
    /// 获取拖动速度
    /// </summary>
    /// <returns></returns>
    private float CalcDragSpeed()
    {
        if (m_Orthographic)
        {
            return MainCamera.orthographicSize / Screen.height;
        }
        return Mathf.Abs(m_Y) * Mathf.Tan(Mathf.Deg2Rad * MainCamera.fieldOfView * 0.5f) * 2.0f / Screen.height;
    }


    /// <summary>
    /// 获取平行投影大小
    /// </summary>
    /// <returns></returns>
    float GetOrthSize()
    {
        return mainCamera.orthographicSize;
    }

    /// <summary>
    /// 获取观察距离
    /// </summary>
    /// <returns></returns>
    public float GetLookDistance()
    {
        return m_Y / Mathf.Sin(Pitch * Mathf.Deg2Rad);
    }

    /// <summary>
    /// 设置平行投影大小
    /// </summary>
    /// <param name="s"></param>
    void SetOrthSize(float s)
    {
        //var max = Mathf.Min(MaxOrthSize, m_WorldSize.y * 0.5f);
        mainCamera.orthographicSize = Mathf.Clamp(s, MinOrthSize0, MaxOrthSize);

        // 防止出边界
        //SetPosition(m_CameraTrans.localPosition);

        // 重新计算拖动速度
        //m_DragSpeed = CalcDragSpeed();
    }

    /// <summary>
    /// 设置观察距离
    /// </summary>
    /// <param name="d"></param>
    //void SetLookDistance(float d)
    //{
    //    var d0 = GetLookDistance();
    //    var lookAt = m_CameraTrans.localPosition + m_CameraTrans.forward * d0;
    //    d = Mathf.Clamp(d, MinD0, MaxD);
    //    var newp = lookAt - m_CameraTrans.forward * d;
    //    m_Y = newp.y;
    //    SetPosition(newp);

    //    // 重新计算拖动速度
    //    m_DragSpeed = CalcDragSpeed();

    //    WorldSpaceUI.ScaleRatio = MinD / GetLookDistance();
    //}

    public void Zoom(float obj)
    {
        if (!m_ActiveMouseCtrl)
        {
            return;
        }

        if (m_Orthographic)
        {
            var s = GetOrthSize() + obj * OrthSizeFactor;
            SetOrthSize(s);
        }
        //else
        //{
        //    var d = GetLookDistance() + obj * ZoomFactor;
        //    SetLookDistance(d);
        //}
    }
    public void Drag(PointerEventData obj)
    {   
        var m_DragDelta = new Vector3(-obj.delta.x, -obj.delta.y, 0);
        m_CameraTrans.SetPositionAndRotation((m_CameraTrans.position+m_DragDelta * m_DragSpeed * m_DragSpeedCoefficient), MainCamera.transform.rotation);
        //m_CameraTrans.localPosition += m_DragDelta * m_DragSpeed * m_DragSpeedCoefficient;
    }

}
