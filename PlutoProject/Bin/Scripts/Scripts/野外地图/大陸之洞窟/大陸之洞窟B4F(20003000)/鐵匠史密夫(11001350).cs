using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20003000
{
    public class S11001350 : Event
    {
        public S11001350()
        {
            this.EventID = 11001350;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<LV85_Clothes> LV85_Clothes_mask = pc.CMask["LV85_Clothes"];
            if (pc.Level >= 85)
            {
                if (!LV85_Clothes_mask.Test(LV85_Clothes.任务开始))
                {
                    LV85_Clothes_mask.SetValue(LV85_Clothes.任务开始, true);
                    Say(pc, 111, "オラ、スミフ……。$R;" +
                        "$R生きているころは$R;" +
                        "腕のいいブラックスミスだった……。$R;" +
                        "$P生きている人間でもかまわねぇ。$R;" +
                        "オラの防具、買ってくれ……。$R;", "鍛冶屋のスミフ");
                }
                switch (Select(pc, "……。", "", "買わない", "男物の服を見る", "女物の服を見る"))
                {
                    case 2:
                        OpenShopBuy(pc, 260);
                        Say(pc, 111, "オラの防具、呪われてる。$R;" +
                        "オラの弟子なら$R;" +
                        "なんとか出来るかもしれない。$R;" +
                        "$P弟子の名前は…見習い鍛冶屋の$R;" +
                        "……あ、アクス？$R;" +
                        "アルツ……？$R;" +
                        "$Rダメだ。思い出せない……。$R;", "鍛冶屋のスミフ");
                        break;
                    case 3:
                        OpenShopBuy(pc, 261);
                        Say(pc, 111, "オラの防具、呪われてる。$R;" +
                        "オラの弟子なら$R;" +
                        "なんとか出来るかもしれない。$R;" +
                        "$P弟子の名前は…見習い鍛冶屋の$R;" +
                        "……あ、アクス？$R;" +
                        "アルツ……？$R;" +
                        "$Rダメだ。思い出せない……。$R;", "鍛冶屋のスミフ");
                        break;
                }
            }
            Say(pc, 111, "……。$R;", "鍛冶屋のスミフ");
        }
    }
}
