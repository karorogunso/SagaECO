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
                    Say(pc, 131, "哇，那是『飛空庭鑰匙』呢。$R;" +
                        "$R那麼就代表擁有飛空庭呢$R;" +
                        "現在還這麼年輕，真是太能幹了！$R;" +
                        "我在您這個年紀的時候$R;" +
                        "還在漫無目的地流浪呢$R;" +
                        "現在的年輕人都很了不起阿$R;" +
                        "$P那麼我這個大哥$R;" +
                        "就給能幹的弟弟一些禮物吧$R;" +
                        "$R雖然是禮物$R;" +
                        "但不是什麼隨便的小東西呢$R;" +
                        "$P哈哈，送您一份禮物$R;" +
                        "就是『小屋』1間！！$R;");
                    if (CheckInventory(pc, 30001200, 1))
                    {
                        Say(pc, 131, "『小屋』是相當重的，搬得動嗎？$R;");
                        switch (Select(pc, "想怎麼做呢？", "", "先去整理東西", "接收小屋"))
                        {
                            case 1:
                                break;
                            case 2:
                                mask.SetValue(JJJRFlags.获得小屋, true);
                                GiveItem(pc, 30001200, 1);
                                Say(pc, 131, "要改建、拆除房子或是購買傢俱$R;" +
                                    "歡迎來到我們的店啊！$R;");
                                PlaySound(pc, 2040, false, 100, 50);
                                Say(pc, 131, "得到『小屋』$R;");
                                break;
                        }
                        return;
                    }
                    Say(pc, 131, "去把行李減少一些，再來吧。$R;");
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