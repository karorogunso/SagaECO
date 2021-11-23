using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10062000
{
    public class S11000870 : Event
    {
        public S11000870()
        {
            this.EventID = 11000870;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<JJJRFlags> mask = pc.AMask["JJJR"];
            BitMask<FGarden> fgarden = pc.AMask["FGarden"];
            BitMask<AYEFlags> mask_0 = new BitMask<AYEFlags>(pc.CMask["AYE"]);

            if (!mask_0.Test(AYEFlags.傢俱匠人第一次对话))//_6a06)
            {
                mask_0.SetValue(AYEFlags.傢俱匠人第一次对话, true);
                Say(pc, 131, "おや、お客さん。$R;" +
                    "はじめて見る顔だねぇ。$R;" +
                    "$R私は飛空庭に飾るアイテムを扱う$R;" +
                    "家具職人だ。$R;" +
                    "よければ、お得意さんになっとくれ！$R;");
            }
            else
            {
                if (fgarden.Test(FGarden.得到飛空庭鑰匙)
                   && !mask.Test(JJJRFlags.获得小屋)
                   && CountItem(pc, 10022700) >= 1)
                {
                    Say(pc, 131, "哇，那是『飞空庭钥匙』呢。$R;" +
                        "$R那么就代表拥有飞空庭呢$R;" +
                        "现在还这么年轻，真是太能干了！$R;" +
                        "我在您这个年纪的时候$R;" +
                        "还在漫无目的地流浪呢$R;" +
                        "现在的年轻人都很了不起阿$R;" +
                        "$P那么我这个大哥$R;" +
                        "就给能干的弟弟一些礼物吧$R;" +
                        "$R虽然是礼物$R;" +
                        "但不是什么随便的小东西呢$R;" +
                        "$P哈哈，送您一份礼物$R;" +
                        "就是『小屋』1间！！$R;");
                    if (CheckInventory(pc, 30001200, 1))
                    {
                        Say(pc, 131, "『小屋』是相当重的，搬得动吗？$R;");
                        switch (Select(pc, "想怎么做呢？", "", "先去整理东西", "接收小屋"))
                        {
                            case 1:
                                break;
                            case 2:
                                mask.SetValue(JJJRFlags.获得小屋, true);
                                GiveItem(pc, 30001200, 1);
                                Say(pc, 131, "要改建、拆除房子或是购买家具$R;" +
                                    "欢迎来到我们的店啊！$R;");
                                PlaySound(pc, 2040, false, 100, 50);
                                Say(pc, 131, "得到『小屋』$R;");
                                break;
                        }
                        return;
                    }
                    Say(pc, 131, "去把行李减少一些，再来吧。$R;");
                    return;
                }
                Say(pc, 131, "家具～、家具はいかがですか？$R;" +
                    "20年前からかわらないお値段ですよ！$R;");
            }

            switch (Select(pc, "どうする？", "", "家具を買う", "何もしない"))
            {
                case 1:
                    OpenShopBuy(pc, 154);
                    break;
                case 2:
                    break;
            }
        }
    }
}