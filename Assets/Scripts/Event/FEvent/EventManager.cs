using UnityEngine;


public class EventManager
{
    public static FEvent<EUICareAboutMoneyType[]> OnMoneyTypeChange = new FEvent<EUICareAboutMoneyType[]>();   //货币栏显示变化

    public static FEvent<Vector2Int> ScreenResolutionEvt = new FEvent<Vector2Int>();   //分辨率变化适配


    public static FEvent<float> OnZoomChange = new FEvent<float>();   //缩放事件
}
