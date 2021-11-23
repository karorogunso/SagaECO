using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:寶石商店(30021000) NPC基本信息:寶石商人(11000043) X:3 Y:1
namespace SagaScript.M30021000
{
    public class S11000043 : Event
    {
        public S11000043()
        {
            this.EventID = 11000043;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Acropolisut_01> Acropolisut_01_mask = new BitMask<Acropolisut_01>(pc.CMask["Acropolisut_01"]);                                                      //一般：阿高普路斯市
            BitMask<Sinker> Sinker_mask = pc.AMask["Sinker"];                                                                                      //任務：取得詩迪的項鏈墜

            //int selection;

            if (Sinker_mask.Test(Sinker.獲得別針) && 
                !Sinker_mask.Test(Sinker.寶石商人給予詩迪的項鏈墜) &&
                CountItem(pc, 10019302) > 0 &&
                CountItem(pc, 10038101) > 0)
            {
                製作詩迪的項鏈墜(pc);
            }

            if (CountItem(pc, 10011501) > 0)
            {
                製作活動木偶礦石精靈(pc);
            }

            if (!Acropolisut_01_mask.Test(Acropolisut_01.已經與寶石商人進行第一次對話))
            {
                初次與寶石商人進行對話(pc);
            }

            switch (Select(pc, "想找什麼呢?", "", "買東西", "賣東西", "委託「飾品製作」", "什麼也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 18);

                    Say(pc, 11000043, 131, "下次還要再來喔!$R;", "寶石商人");
                    break;

                case 2:
                    OpenShopSell(pc, 18);

                    Say(pc, 11000043, 131, "下次還要再來喔!$R;", "寶石商人");
                    break;

                case 3:
                    Synthese(pc, 2018, 5);
                    break;

                case 4:
                    Say(pc, 11000043, 131, "下次還要再來喔!$R;", "寶石商人");
                    break;
            }
        }

        void 初次與寶石商人進行對話(ActorPC pc)
        {
            BitMask<Acropolisut_01> Acropolisut_01_mask = new BitMask<Acropolisut_01>(pc.CMask["Acropolisut_01"]);                                                      //一般：阿高普路斯市

            Acropolisut_01_mask.SetValue(Acropolisut_01.已經與寶石商人進行第一次對話, true);

            Say(pc, 11000043, 131, "這裡提供「飾品製作」、「寶石研磨」$R;" +
                                   "以及「金屬加工」。$R;" +
                                   "$P如果客人親自帶材料過來的話，$R;" +
                                   "我們會免費幫你製作的。$R;", "寶石商人");
        }

        void 製作活動木偶礦石精靈(ActorPC pc)
        {
            Say(pc, 11000043, 131, "客人!$R;" +
                                   "$R你手上拿的那個是$R;" +
                                   "『含有不明物體的寶石』嗎?$R;", "寶石商人");

            switch (Select(pc, "手上拿的是『含有不明物體的寶石』嗎?", "", "是", "不是"))
            {
                case 1:
                    Say(pc, 11000043, 131, "在這顆寶石裡面，$R;" +
                                           "附有在其他世界中，$R;" +
                                           "生活的異界生物的靈魂。$R;" +
                                           "$P如果誠心擦拭的話，$R;" +
                                           "傳說會借給擦拭的人，$R;" +
                                           "牠們強大的力量。$R;" +
                                           "$P怎麼樣?$R;" +
                                           "$R雖然不知道能不能成功，$R;" +
                                           "但是想不想擦擦看?$R;", "寶石商人");

                    switch (Select(pc, "想不想擦擦看?", "", "擦擦看", "算了"))
                    {
                        case 1:
                            Fade(pc, FadeType.Out, FadeEffect.Black);

                            Wait(pc, 1000);

                            PlaySound(pc, 2213, false, 100, 50);
                            Wait(pc, 3000);

                            PlaySound(pc, 2213, false, 100, 50);
                            Wait(pc, 3000);

                            Fade(pc, FadeType.In, FadeEffect.Black);

                            PlaySound(pc, 4006, false, 100, 50);
                            TakeItem(pc, 10011501, 1);
                            GiveItem(pc, 10011500, 1);
                            Say(pc, 0, 0, "『含有不明物體的寶石』$R;" +
                                          "變成『活動木偶 礦石精靈』了!$R;", " ");

                            Say(pc, 11000043, 131, "哇阿…真是太美麗了!$R;" +
                                                   "$R一定要好好珍藏啊!!$R;", "寶石商人");
                            break;

                        case 2:
                            Say(pc, 11000043, 131, "覺得原來的樣子也很漂亮啊?$R;", "寶石商人");
                            break;
                    }
                    return;

                case 2:
                    Say(pc, 11000043, 131, "啊…不好意思!!$R;" +
                                           "$R好像是…錯覺。$R;", "寶石商人");
                    break;
            }
        }

        void 製作詩迪的項鏈墜(ActorPC pc)
        {
            BitMask<Sinker> Sinker_mask = new BitMask<Sinker>(pc.CMask["Sinker"]);                                                                                      //任務：取得詩迪的項鏈墜

            if (Sinker_mask.Test(Sinker.詩迪的項鏈墜製作完成) &&
                !Sinker_mask.Test(Sinker.寶石商人給予詩迪的項鏈墜))
            {
                詩迪的項鏈墜製作完成(pc);
                return;
            }
            委託寶石商人製作詩迪的項鏈墜(pc);
        }

        void 委託寶石商人製作詩迪的項鏈墜(ActorPC pc)
        {
            BitMask<Sinker> Sinker_mask = new BitMask<Sinker>(pc.CMask["Sinker"]);                                                                                      //任務：取得詩迪的項鏈墜

            Say(pc, 11000043, 131, "真漂亮的金屬啊!$R;" +
                                   "你還有『別針』啊?!$R;" +
                                   "$R我幫你加工做成『項鏈墜』吧!$R;" +
                                   "$P只要你把『那個金屬』、『別針』$R;" +
                                   "以及『10000金幣』的製作費用給我，$R;" +
                                   "我就能幫你製作成『項鏈墜』喔!!", "寶石商人");

            switch (Select(pc, "想要加工嗎?", "", "加工", "不加工"))
            {
                case 1:
                    Say(pc, 11000043, 131, "那就把東西都給我吧!$R;", "寶石商人");

                    if (pc.Gold >= 10000)
                    {
                        Sinker_mask.SetValue(Sinker.詩迪的項鏈墜製作完成, true); 

                        PlaySound(pc, 2040, false, 100, 50);
                        TakeItem(pc, 10019302, 1);
                        TakeItem(pc, 10038101, 1);
                        pc.Gold -= 10000;
                        Say(pc, 0, 0, "給他『不明的合金破片』和『別針』!$R;", " ");

                        Say(pc, 11000043, 131, "來! 做好了!$R;", "寶石商人");

                        if (CheckInventory(pc, 50050102, 1))
                        {
                            詩迪的項鏈墜製作完成(pc);
                        }
                        else
                        {
                            Say(pc, 11000043, 131, "行李太多，無法給您啊!$R;", "寶石商人");                  
                        }
                    }
                    else
                    {
                        Say(pc, 11000043, 131, "真是的! 錢帶不夠啊!$R;", "寶石商人");
                    }
                    break;

                case 2:
                    break;
            }
        }

        void 詩迪的項鏈墜製作完成(ActorPC pc)
        {
            BitMask<Sinker> Sinker_mask = new BitMask<Sinker>(pc.CMask["Sinker"]);                                                                                      //任務：取得詩迪的項鏈墜

            Sinker_mask.SetValue(Sinker.寶石商人給予詩迪的項鏈墜, true); 

            PlaySound(pc, 2040, false, 100, 50);
            GiveItem(pc, 50050102, 1);
            Say(pc, 11000043, 131, "得到『詩迪的項鏈墜』!$R;", " ");

            Say(pc, 11000043, 131, "這個金屬中好像擁有特別的力量啊!$R;" +
                                   "$R這個力量一定會好好的保護好你的!$R;" +
                                   "$P那麼歡迎下次再度光臨啊!!$R;", "寶石商人");
        }
    }
}
