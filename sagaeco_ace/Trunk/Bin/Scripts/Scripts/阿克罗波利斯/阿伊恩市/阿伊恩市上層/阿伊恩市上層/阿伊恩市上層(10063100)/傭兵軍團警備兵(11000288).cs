using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10063100
{
    public class S11000288 : Event
    {
        public S11000288()
        {
            this.EventID = 11000288;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<AYEFlags> mask = new BitMask<AYEFlags>(pc.CMask["AYE"]);
            byte x, y;
            if (mask.Test(AYEFlags.傳送到遺跡))//_0C60)
            {
                mask.SetValue(AYEFlags.傳送到遺跡, false);
                //_0C60 = false;
                Say(pc, 131, "准备好了吗？$R;" +
                    "那么出发吧！$R;");
                x = (byte)Global.Random.Next(22, 28);
                y = (byte)Global.Random.Next(28, 41);
                Warp(pc, 20080014, x, y);
                //GOTO EVT10001111
                return;
            }
            if (mask.Test(AYEFlags.詢問遺跡))//_0C59)
            {
                Say(pc, 131, "这里是佣兵军团团本部$R;" +
                    "$P有什么事吗？$R;");
                switch (Select(pc, "做什么呢?", "", "想去遗迹", "没什么"))
                {
                    case 1:
                        mask.SetValue(AYEFlags.傳送到遺跡, true);
                        //_0C60 = true;
                        Say(pc, 131, "对了，要把握时机阿$R;" +
                            "现在就出发吧。$R;" +
                            "$R…什么?太快了？$R;" +
                            "可以，那我就等您准备好了再走吧。$R;" +
                            "准备好了就跟我说$R;");
                        break;
                    case 2:
                        break;
                }
                return;
            }
            if (pc.Fame < 10)
            {
                Say(pc, 131, "这里是佣兵军团本部$R;");
                return;
            }
            Say(pc, 131, "这裡是佣兵军团团本部$R;" +
                "…$R;" +
                "啊……$R;" +
                "听说最近发现了远古时代的遗迹，$R知道吗？$R;");
            switch (Select(pc, "知道吗？", "", "知道", "不知道"))
            {
                case 1:
                    break;
                case 2:
                    mask.SetValue(AYEFlags.詢問遺跡, true);
                    //_0C59 = true;
                    Say(pc, 131, "好像是挺大的遗跡唷，$R;" +
                        "各国都派了调查团$R;" +
                        "$P所以我们也要去呢。$R;" +
                        "您想和我们一起去吗？$R;");
                    switch (Select(pc, "怎么办呢？", "", "一起去", "不去"))
                    {
                        case 1:
                            mask.SetValue(AYEFlags.傳送到遺跡, true);
                            //_0C60 = true;
                            Say(pc, 131, "对了，要把握时机啊$R;" +
                                "现在就出发吧。$R;" +
                                "$R…什么?太快了？$R;" +
                                "可以，那我就等您准备好了再走吧。$R;" +
                                "准备好了就跟我说$R;");
                            break;
                        case 2:
                            break;
                    }
                    break;
            }
        }
    }
}
