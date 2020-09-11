using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeParam : UIOpenScreenParameterBase
{
    public string Des;
}


public class AttributeScreen : ScreenBase
{
    AttributeCtrl mCtrl;
    AttributeParam mParam;
    public AttributeScreen(UIOpenScreenParameterBase param = null) : base(UIConst.UIAttribute,param)
    {
        
    }
    protected override void OnLoadSuccess()
    {
        base.OnLoadSuccess();
        mCtrl = mCtrlBase as AttributeCtrl;
        mParam = mOpenParam as AttributeParam;
        mCtrl.Des.text = mParam.Des;
        mCtrl.CloseButton.onClick.AddListener(OnCloseClick);
    }

    void OnCloseClick()
    {
        OnClose();
    }


}
