using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeParam : UIOpenScreenParameterBase
{

}


public class AttributeScreen : ScreenBase
{
    AttributeCtrl mCtrl;

    public AttributeScreen(UIOpenScreenParameterBase param = null) : base(UIConst.UIAttribute,param)
    {
        
    }
    protected override void OnLoadSuccess()
    {
        base.OnLoadSuccess();
        mCtrl = mCtrlBase as AttributeCtrl;
        mCtrl.CloseButton.onClick.AddListener(OnCloseClick);
    }

    void OnCloseClick()
    {
        OnClose();
    }


}
