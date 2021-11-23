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
                Say(pc, 131, "準備好了嗎？$R;" +
                    "那麼出發吧！$R;");
                x = (byte)Global.Random.Next(22, 28);
                y = (byte)Global.Random.Next(28, 41);
                Warp(pc, 20080014, x, y);
                //GOTO EVT10001111
                return;
            }
            if (mask.Test(AYEFlags.詢問遺跡))//_0C59)
            {
                Say(pc, 131, "這裡是傭兵軍團團本部$R;" +
                    "$P有什麼事嗎？$R;");
                switch (Select(pc, "做什麼呢?", "", "想去遺跡", "沒什麼"))
                {
                    case 1:
                        mask.SetValue(AYEFlags.傳送到遺跡, true);
                        //_0C60 = true;
                        Say(pc, 131, "對了，要把握時機阿$R;" +
                            "現在就出發吧。$R;" +
                            "$R…什麼?太快了？$R;" +
                            "可以，那我就等您準備好了再走吧。$R;" +
                            "準備好了就跟我說$R;");
                        break;
                    case 2:
                        break;
                }
                return;
            }
            if (pc.Fame < 10)
            {
                Say(pc, 131, "這裡是傭兵軍團本部$R;");
                return;
            }
            Say(pc, 131, "這裡是傭兵軍團團本部$R;" +
                "…$R;" +
                "啊……$R;" +
                "聽說最近發現了遠古時代的遺跡，$R知道嗎？$R;");
            switch (Select(pc, "知道嗎？", "", "知道", "不知道"))
            {
                case 1:
                    break;
                case 2:
                    mask.SetValue(AYEFlags.詢問遺跡, true);
                    //_0C59 = true;
                    Say(pc, 131, "好像是挺大的遺跡唷，$R;" +
                        "各國都派了調查團$R;" +
                        "$P所以我們也要去呢。$R;" +
                        "您想和我們一起去嗎？$R;");
                    switch (Select(pc, "怎麼辦呢？", "", "一起去", "不去"))
                    {
                        case 1:
                            mask.SetValue(AYEFlags.傳送到遺跡, true);
                            //_0C60 = true;
                            Say(pc, 131, "對了，要把握時機阿$R;" +
                                "現在就出發吧。$R;" +
                                "$R…什麼?太快了？$R;" +
                                "可以，那我就等您準備好了再走吧。$R;" +
                                "準備好了就跟我說$R;");
                            break;
                        case 2:
                            break;
                    }
                    break;
            }
        }
    }
}
