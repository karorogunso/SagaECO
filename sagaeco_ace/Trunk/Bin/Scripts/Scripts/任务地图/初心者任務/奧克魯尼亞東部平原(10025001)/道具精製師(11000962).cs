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
                                   "$R我是阿克罗波利斯行会评议会，$R;" +
                                   "派来的道具精制师喔!$R;" +
                                   "$R我是负责「精制」道具的，$R;" +
                                   "将道具减轻重量或者是$R;" +
                                   "打开『木箱』或『宝物箱』。$R;" +
                                   "$P我也做「合成」和$R;" +
                                   "「木材加工」之类的工作喔!$R;" +
                                   "需要帮忙的话，随时过来吧。$R;" +
                                   "$P偷偷告诉您「秘方」吧!$R;" +
                                   "$R用滑鼠右键点击道具图示，$R;" +
                                   "打开「道具资料」视窗的话，$R;" +
                                   "会看到「秘方」的按键。$R;" +
                                   "$P按下 「秘方」后，$R;" +
                                   "「秘方」视窗就可以打开。$R;" +
                                   "$P在「秘方」视窗中，$R;" +
                                   "可以确认制作道具时，$R;" +
                                   "需要的「技能」「材料」跟「工具」。$R;" +
                                   "$R要制作道具的时候，$R;" +
                                   "先去看看秘方比较好呀!$R;", "道具精制师");
        }
    }
}
