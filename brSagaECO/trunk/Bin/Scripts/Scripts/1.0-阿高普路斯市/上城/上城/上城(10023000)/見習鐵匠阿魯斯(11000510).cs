using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:上城(10023000) NPC基本信息:見習鐵匠阿魯斯(11000510) X:129 Y:201
namespace SagaScript.M10023000
{
    public class S11000510 : Event
    {
        public S11000510()
        {
            this.EventID = 11000510;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<LV35_Clothes_01> LV35_Clothes_01_mask = new BitMask<LV35_Clothes_01>(pc.CMask["LV35_Clothes_01"]);
            BitMask<AYEFlags> mask = new BitMask<AYEFlags>(pc.CMask["AYE"]);
            BitMask<LV85_Clothes> LV85_Clothes_mask = pc.CMask["LV85_Clothes"];
            if (LV85_Clothes_mask.Test(LV85_Clothes.任务结束))
            {
                if (!LV85_Clothes_mask.Test(LV85_Clothes.最後的對話))
                {
                    Say(pc, 131, "あぁ、防具の呪いとけたの？$R;" +
                    "よかったね……。$R;" +
                    "$P俺、呪いとか、幽霊とか怖いし$R;" +
                    "情けない男だけど$R;" +
                    "俺なりに出来ることを、考えてさ。$R;" +
                    "$Pこのあいだの防具にあいそうなものを$R;" +
                    "いくつか作ってみたんだ。$R;" +
                    "$Rよければ、見ていってくれ。$R;", "見習い鍛冶屋アルス");
                }
                else
                {
                    Say(pc, 131, "ああ、どうしよう……。$Rまた材料が足りない……。$R;" +
                    "$P気がする……。$R;", "見習い鍛冶屋アルス");
                }
                switch (Select(pc, "どうする？", "", "鉄製防具", "男物のズボンを買う", "男物の靴を買う", "女物の靴を買う", "何もしない"))
                {
                    case 1:
                        OpenShopBuy(pc, 61);
                        break;
                    case 2:
                        OpenShopBuy(pc, 262);
                        break;
                    case 3:
                        OpenShopBuy(pc, 263);
                        break;
                    case 4:
                        OpenShopBuy(pc, 264);
                        break;
                }
                return;
            }
            if (LV35_Clothes_01_mask.Test(LV35_Clothes_01.見習鐵匠阿魯斯的委託完成) &&
                LV85_Clothes_mask.Test(LV85_Clothes.任务开始) &&
                !LV85_Clothes_mask.Test(LV85_Clothes.已于阿魯斯对话) &&
                ((CountItem(pc, 60002951) > 0) ||
                (CountItem(pc, 60101351) > 0) ||
                (CountItem(pc, 60101451) > 0) ||
                (CountItem(pc, 60101551) > 0) ||
                (CountItem(pc, 60101651) > 0) ||
                (CountItem(pc, 60101751) > 0) ||
                (CountItem(pc, 60101851) > 0) ||
                (CountItem(pc, 60101951) > 0) ||
                (CountItem(pc, 60102051) > 0) ||
                (CountItem(pc, 60102151) > 0) ||
                (CountItem(pc, 60102251) > 0) ||
                (CountItem(pc, 60102351) > 0) ||
                (CountItem(pc, 60102451) > 0) ||
                (CountItem(pc, 60102551) > 0) ||
                (CountItem(pc, 60102651) > 0) ||
                (CountItem(pc, 60102751) > 0) ||
                (CountItem(pc, 60102851) > 0) ||
                (CountItem(pc, 60102951) > 0) ||
                (CountItem(pc, 60103051) > 0) ||
                (CountItem(pc, 60103151) > 0) ||
                (CountItem(pc, 60103251) > 0) ||
                (CountItem(pc, 60103351) > 0) ||
                (CountItem(pc, 60103451) > 0) ||
                (CountItem(pc, 60103551) > 0)))
            {
                LV85_Clothes_mask.SetValue(LV85_Clothes.已于阿魯斯对话, true);
                Say(pc, 131, "ま、まさか！？$R;" +
                "君が持っているその防具は！！！$R;" +
                "$P……。$R;" +
                "$Pああ、きみは$R;" +
                "なんてものを手に入れてしまったんだ。$R;" +
                "$Rもう、おしまいだ……。$R;" +
                "$P……まぁ。$R;" +
                "$R呪いをとく方法が$R;" +
                "ないわけではないんだけど……。$R;" +
                "$Pえ、呪いをときたい？$R;" +
                "$Pおっ、俺……。$R;" +
                "呪いとか、幽霊とか……。$R;" +
                "そういうの、ダメなんだ……。$R;" +
                "$Rほら、怖いし……。$R;" +
                "$Pあ、でも、俺の彼女なら$R;" +
                "たぶん、呪い、とけると思う。$R;" +
                "$R……俺より強いから。$R;" +
                "$Pもし、本当に呪いをときたいのなら$R;" +
                "アイツに頼むといいよ。$R;" +
                "$Rアイツは今、アイアンシティの$R;" +
                "下層階に住んでる。$R;" +
                "青色の屋根の家だよ。$R;" +
                "$Pはぁぁ……。$R;" +
                "俺って、情けない……。$R;", "見習い鍛冶屋アルス");
                return;
            }
            //戰士系45職業裝任務
            if (mask.Test(AYEFlags.尋找阿魯斯開始) && !mask.Test(AYEFlags.找到阿魯斯))
            {
                mask.SetValue(AYEFlags.找到阿魯斯, true);
                //_4a67 = true;
                Say(pc, 131, "說什麼!媽媽?!$R;" +
                    "$P知道了…$R;" +
                    "$R請轉告她，只要材料都收集好了$R;" +
                    "我就回去，可以嗎?$R;" +
                    "$P唉…現在還不想回去$R;" +
                    "$P其實媽媽和我情人的關係不好$R;" +
                    "$R一想到回去又要勸架…$R;" +
                    "$P唉…怎麼辦才好呢$R;" +
                    "這樣也不行那樣也不行啊$R;");
                return;
            }
            if (!LV35_Clothes_01_mask.Test(LV35_Clothes_01.見習鐵匠阿魯斯的委託完成))
            {
                見習鐵匠阿魯斯的委託(pc);
                return;
            }
            if (LV85_Clothes_mask.Test(LV85_Clothes.已于阿魯斯对话))
            {
                Say(pc, 131, "俺の彼女は、アイアンシティの$R;" +
                "下層階に住んでるよ。$R;", "見習い鍛冶屋アルス");
            }
            else
            {
                Say(pc, 11000510, 131, "啊啊…這個怎麼辦啊…$R;" +
                                       "$R材料還是不夠呢…$R;", "見習鐵匠阿魯斯");
            }
            switch (Select(pc, "想要買什麼呢?", "", "鐵製防具", "什麼也不買"))
            {
                case 1:
                    OpenShopBuy(pc, 61);
                    break;

                case 2:
                    break;
            }
        }

        void 見習鐵匠阿魯斯的委託(ActorPC pc)
        {
            if (pc.CInt["LV35_Clothes_01"] == 0)
            {
                見習鐵匠阿魯斯告知缺少的材料(pc);
                return;
            }
            else
            {
                把材料交給見習鐵匠阿魯斯(pc);
                return;
            }
        }

        void 見習鐵匠阿魯斯告知缺少的材料(ActorPC pc)
        {
            int selection;

            if (pc.Level < 20)
            {
                Say(pc, 11000510, 131, "啊啊…怎麼辦呢?$R;" +
                                       "還缺少材料呢…$R;", "見習鐵匠阿魯斯");

                selection = Global.Random.Next(1, 6);

                pc.CInt["LV35_Clothes_01"] = selection;

                switch (pc.CInt["LV35_Clothes_01"])
                {
                    case 1:
                        Say(pc, 11000510, 131, "如果沒有10塊『鐵塊』…$R;" +
                                               "那我…我…會完蛋的…$R;", "見習鐵匠阿魯斯");
                        break;

                    case 2:
                        Say(pc, 11000510, 131, "如果沒有10顆『堅硬鵝卵石』…$R;" +
                                               "那我…我…會完蛋的…$R;", "見習鐵匠阿魯斯");
                        break;

                    case 3:
                        Say(pc, 11000510, 131, "如果沒有10個『木材』…$R;" +
                                               "那我…我…會完蛋的…$R;", "見習鐵匠阿魯斯");
                        break;

                    case 4:
                        Say(pc, 11000510, 131, "如果沒有170根『樹枝』…$R;" +
                                               "那我…我…會完蛋的…$R;", "見習鐵匠阿魯斯");
                        break;

                    case 5:
                        Say(pc, 11000510, 131, "如果沒有40根『骨頭』…$R;" +
                                               "那我…我…會完蛋的…$R;", "見習鐵匠阿魯斯");
                        break;

                    case 6:
                        Say(pc, 11000510, 131, "如果沒有40顆『鵝卵石』…$R;" +
                                               "那我…我…會完蛋的…$R;", "見習鐵匠阿魯斯");
                        break;
                }
            }
            else
            {
                Say(pc, 11000510, 131, "啊啊…怎麼辦呢?$R;" +
                                       "還缺少材料呢…$R;", "見習鐵匠阿魯斯");

                selection = Global.Random.Next(7, 8);

                pc.CInt["LV35_Clothes_01"] = selection;

                switch (pc.CInt["LV35_Clothes_01"])
                {
                    case 7:
                        Say(pc, 11000510, 131, "如果沒有火焰/水靈/大地/神風的$R;" +
                                               "『召喚石』各一顆的話…$R;" +
                                               "那我…我…會完蛋的…$R;", "見習鐵匠阿魯斯");
                        break;

                    case 8:
                        Say(pc, 11000510, 131, "如果沒有光明/黑暗的$R;" +
                                               "『召喚石』各一顆的話…$R;" +
                                               "那我…我…會完蛋的…$R;", "見習鐵匠阿魯斯");
                        break;
                }
            }
        }

        void 把材料交給見習鐵匠阿魯斯(ActorPC pc)
        {
            BitMask<LV35_Clothes_01> LV35_Clothes_01_mask = new BitMask<LV35_Clothes_01>(pc.CMask["LV35_Clothes_01"]);

            switch (pc.CInt["LV35_Clothes_01"])
            {
                case 1:
                    if (CountItem(pc, 10015600) >= 10)
                    {
                        LV35_Clothes_01_mask.SetValue(LV35_Clothes_01.見習鐵匠阿魯斯的委託完成, true);

                        PlaySound(pc, 2040, false, 100, 50);
                        TakeItem(pc, 10015600, 10);
                        Say(pc, 0, 0, "把『鐵塊』交給他。$R;", " ");

                        Say(pc, 11000510, 131, "真的? 沒關係嗎?$R;" +
                                               "最好不會說別的話吧?$R;" +
                                               "$R真的好感謝你啊!$R;" +
                                               "$P到「阿伊恩薩烏斯」的話，$R;" +
                                               "一定要到我的店裡啊!$R;" +
                                               "店舖雖小，但五臟俱全…$R;" +
                                               "$R我會堅持到那時不倒閉的…呵!$R;", "見習鐵匠阿魯斯");
                    }
                    else
                    {
                        Say(pc, 11000510, 131, "如果沒有10塊『鐵塊』…$R;" +
                                               "那我…我…會完蛋的…$R;", "見習鐵匠阿魯斯");
                    }
                    break;

                case 2:
                    if (CountItem(pc, 10014650) >= 10)
                    {
                        LV35_Clothes_01_mask.SetValue(LV35_Clothes_01.見習鐵匠阿魯斯的委託完成, true);

                        PlaySound(pc, 2040, false, 100, 50);
                        TakeItem(pc, 10014650, 10);
                        Say(pc, 0, 0, "把『堅硬鵝卵石』交給他。$R;", " ");

                        Say(pc, 11000510, 131, "真的? 沒關係嗎?$R;" +
                                               "最好不會說別的話吧?$R;" +
                                               "$R真的好感謝你啊!$R;" +
                                               "$P到「阿伊恩薩烏斯」的話，$R;" +
                                               "一定要到我的店裡啊!$R;" +
                                               "店舖雖小，但五臟俱全…$R;" +
                                               "$R我會堅持到那時不倒閉的…呵!$R;", "見習鐵匠阿魯斯");
                    }
                    else
                    {
                        Say(pc, 11000510, 131, "如果沒有10顆『堅硬鵝卵石』…$R;" +
                                               "那我…我…會完蛋的…$R;", "見習鐵匠阿魯斯");
                    }
                    break;

                case 3:
                    if (CountItem(pc, 10016300) >= 10)
                    {
                        LV35_Clothes_01_mask.SetValue(LV35_Clothes_01.見習鐵匠阿魯斯的委託完成, true);

                        PlaySound(pc, 2040, false, 100, 50);
                        TakeItem(pc, 10016300, 10);
                        Say(pc, 0, 0, "把『木材』交給他。$R;", " ");

                        Say(pc, 11000510, 131, "真的? 沒關係嗎?$R;" +
                                               "最好不會說別的話吧?$R;" +
                                               "$R真的好感謝你啊!$R;" +
                                               "$P到「阿伊恩薩烏斯」的話，$R;" +
                                               "一定要到我的店裡啊!$R;" +
                                               "店舖雖小，但五臟俱全…$R;" +
                                               "$R我會堅持到那時不倒閉的…呵!$R;", "見習鐵匠阿魯斯");
                    }
                    else
                    {
                        Say(pc, 11000510, 131, "如果沒有10個『木材』…$R;" +
                                               "那我…我…會完蛋的…$R;", "見習鐵匠阿魯斯");
                    }
                    break;

                case 4:
                    if (CountItem(pc, 10016605) >= 170)
                    {
                        LV35_Clothes_01_mask.SetValue(LV35_Clothes_01.見習鐵匠阿魯斯的委託完成, true);

                        PlaySound(pc, 2040, false, 100, 50);
                        TakeItem(pc, 10016605, 170);
                        Say(pc, 0, 0, "把『樹枝』交給他。$R;", " ");

                        Say(pc, 11000510, 131, "真的? 沒關係嗎?$R;" +
                                               "最好不會說別的話吧?$R;" +
                                               "$R真的好感謝你啊!$R;" +
                                               "$P到「阿伊恩薩烏斯」的話，$R;" +
                                               "一定要到我的店裡啊!$R;" +
                                               "店舖雖小，但五臟俱全…$R;" +
                                               "$R我會堅持到那時不倒閉的…呵!$R;", "見習鐵匠阿魯斯");
                    }
                    else
                    {
                        Say(pc, 11000510, 131, "如果沒有170根『樹枝』…$R;" +
                                               "那我…我…會完蛋的…$R;", "見習鐵匠阿魯斯");
                    }
                    break;

                case 5:
                    if (CountItem(pc, 10006600) >= 40)
                    {
                        LV35_Clothes_01_mask.SetValue(LV35_Clothes_01.見習鐵匠阿魯斯的委託完成, true);

                        PlaySound(pc, 2040, false, 100, 50);
                        TakeItem(pc, 10006600, 40);
                        Say(pc, 0, 0, "把『骨頭』交給他。$R;", " ");

                        Say(pc, 11000510, 131, "真的? 沒關係嗎?$R;" +
                                               "最好不會說別的話吧?$R;" +
                                               "$R真的好感謝你啊!$R;" +
                                               "$P到「阿伊恩薩烏斯」的話，$R;" +
                                               "一定要到我的店裡啊!$R;" +
                                               "店舖雖小，但五臟俱全…$R;" +
                                               "$R我會堅持到那時不倒閉的…呵!$R;", "見習鐵匠阿魯斯");
                    }
                    else
                    {
                        Say(pc, 11000510, 131, "如果沒有40根『骨頭』…$R;" +
                                               "那我…我…會完蛋的…$R;", "見習鐵匠阿魯斯");
                    }
                    break;

                case 6:
                    if (CountItem(pc, 10014600) >= 40)
                    {
                        LV35_Clothes_01_mask.SetValue(LV35_Clothes_01.見習鐵匠阿魯斯的委託完成, true);

                        PlaySound(pc, 2040, false, 100, 50);
                        TakeItem(pc, 10014600, 40);
                        Say(pc, 0, 0, "把『鵝卵石』交給他。$R;", " ");

                        Say(pc, 11000510, 131, "真的? 沒關係嗎?$R;" +
                                               "最好不會說別的話吧?$R;" +
                                               "$R真的好感謝你啊!$R;" +
                                               "$P到「阿伊恩薩烏斯」的話，$R;" +
                                               "一定要到我的店裡啊!$R;" +
                                               "店舖雖小，但五臟俱全…$R;" +
                                               "$R我會堅持到那時不倒閉的…呵!$R;", "見習鐵匠阿魯斯");
                    }
                    else
                    {
                        Say(pc, 11000510, 131, "如果沒有40顆『鵝卵石』…$R;" +
                                               "那我…我…會完蛋的…$R;", "見習鐵匠阿魯斯");
                    }
                    break;

                case 7:
                    if (CountItem(pc, 10011201) > 0 && 
                        CountItem(pc, 10011203) > 0 && 
                        CountItem(pc, 10011205) > 0 && 
                        CountItem(pc, 10011207) > 0)
                    {
                        LV35_Clothes_01_mask.SetValue(LV35_Clothes_01.見習鐵匠阿魯斯的委託完成, true);

                        PlaySound(pc, 2040, false, 100, 50);
                        TakeItem(pc, 10011201, 1);
                        TakeItem(pc, 10011203, 1);
                        TakeItem(pc, 10011205, 1);
                        TakeItem(pc, 10011207, 1);
                        Say(pc, 0, 0, "把火焰/水靈/大地/神風的$R;" +
                                      "『召喚石』交給他。$R;", " ");

                        Say(pc, 11000510, 131, "真的? 沒關係嗎?$R;" +
                                               "最好不會說別的話吧?$R;" +
                                               "$R真的好感謝你啊!$R;" +
                                               "$P到「阿伊恩薩烏斯」的話，$R;" +
                                               "一定要到我的店裡啊!$R;" +
                                               "店舖雖小，但五臟俱全…$R;" +
                                               "$R我會堅持到那時不倒閉的…呵!$R;", "見習鐵匠阿魯斯");
                    }
                    else
                    {
                        Say(pc, 11000510, 131, "如果沒有火焰/水靈/大地/神風的$R;" +
                                               "『召喚石』各一顆的話…$R;" +
                                               "那我…我…會完蛋的…$R;", "見習鐵匠阿魯斯");
                    }
                    break;

                case 8:
                    if (CountItem(pc, 10011209) > 0 && 
                        CountItem(pc, 10011210) > 0)
                    {
                        LV35_Clothes_01_mask.SetValue(LV35_Clothes_01.見習鐵匠阿魯斯的委託完成, true);

                        PlaySound(pc, 2040, false, 100, 50);
                        TakeItem(pc, 10011209, 1);
                        TakeItem(pc, 10011210, 1);
                        Say(pc, 0, 0, "把光明/黑暗的$R;" +
                                      "『召喚石』交給他。$R;", " ");

                        Say(pc, 11000510, 131, "真的? 沒關係嗎?$R;" +
                                               "最好不會說別的話吧?$R;" +
                                               "$R真的好感謝你啊!$R;" +
                                               "$P到「阿伊恩薩烏斯」的話，$R;" +
                                               "一定要到我的店裡啊!$R;" +
                                               "店舖雖小，但五臟俱全…$R;" +
                                               "$R我會堅持到那時不倒閉的…呵!$R;", "見習鐵匠阿魯斯");
                    }
                    else
                    {
                        Say(pc, 11000510, 131, "如果沒有光明/黑暗的$R;" +
                                               "『召喚石』各一顆的話…$R;" +
                                               "那我…我…會完蛋的…$R;", "見習鐵匠阿魯斯");
                    }
                    break;
            }
        }
    }
}