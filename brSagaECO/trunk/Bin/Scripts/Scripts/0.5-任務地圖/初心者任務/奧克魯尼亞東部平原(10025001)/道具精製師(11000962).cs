using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞東部平原(10025001) NPC基本信息:道具精製師(11000962) X:47 Y:120
namespace SagaScript.M10025001
{
    public class S11000962 : Event
    {
        public S11000962()
        {
            this.EventID = 11000962;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000962, 131, "您好!$R;" +
                                   "$R我是阿高普路斯行會評議會，$R;" +
                                   "派來的道具精製師喔!$R;" +
                                   "$R我是負責「精製」道具的，$R;" +
                                   "將道具減輕重量或者是$R;" +
                                   "打開『木箱』或『寶物箱』。$R;" +
                                   "$P我也做「合成」和$R;" +
                                   "「木材加工」之類的工作喔!$R;" +
                                   "需要幫忙的話，隨時過來吧。$R;" +
                                   "$P偷偷告訴您「秘方」吧!$R;" +
                                   "$R用滑鼠右鍵點擊道具圖示，$R;" +
                                   "打開「道具資料」視窗的話，$R;" +
                                   "會看到「秘方」的按鍵。$R;" +
                                   "$P按下 「秘方」後，$R;" +
                                   "「秘方」視窗就可以打開。$R;" +
                                   "$P在「秘方」視窗中，$R;" +
                                   "可以確認製作道具時，$R;" +
                                   "需要的「技能」「材料」跟「工具」。$R;" +
                                   "$R要製作道具的時候，$R;" +
                                   "先去看看秘方比較好呀!$R;", "道具精製師");
        }
    }
}
