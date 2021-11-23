using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10024000
{
    public class S11001975 : Event
    {
        public S11001975()
        {
            this.EventID = 11001975;
        }

        public override void OnEvent(ActorPC pc)
        {
            //Say(pc, 131, "抱歉$R;" +
            //    "研究所在整修$R;"+
            //    "杰尼斯博士引发了爆炸$R;", "帕基亚");
            BitMask<Iris_1> Iris_1_mask = new BitMask<Iris_1>(pc.CMask["Iris_1"]);
            //int selection;
            if (Iris_1_mask.Test(Iris_1.第一次对话后))
            {
                Say(pc, 131, "啊！辛苦你了$R;" +
                "去研究所？$R;", "破坏者巴吉亚");
                if (Select(pc, "去研究所？", "", "去", "不去") == 1)
                {
                    Warp(pc, 30166000, 9, 16);
                    return;
                }
                Say(pc, 131, "不去。$R;" +
                "偶然都来一下嘛、$R;" +
                "都会很欢迎你的$R;", "破坏者巴吉亚");

            }
        }
    }
}