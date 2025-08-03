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

            switch (Select(pc, "想找什么呢?", "", "买东西", "卖东西", "委托「饰品制作」", "什么也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 18);

                    Say(pc, 11000043, 131, "下次还要再来喔!$R;", "宝石商人");
                    break;

                case 2:
                    OpenShopSell(pc, 18);

                    Say(pc, 11000043, 131, "下次还要再来喔!$R;", "宝石商人");
                    break;

                case 3:
                    Synthese(pc, 2018, 5);
                    break;

                case 4:
                    Say(pc, 11000043, 131, "下次还要再来喔!$R;", "宝石商人");
                    break;
            }
        }

        void 初次與寶石商人進行對話(ActorPC pc)
        {
            BitMask<Acropolisut_01> Acropolisut_01_mask = new BitMask<Acropolisut_01>(pc.CMask["Acropolisut_01"]);                                                      //一般：阿高普路斯市

            Acropolisut_01_mask.SetValue(Acropolisut_01.已經與寶石商人進行第一次對話, true);

            Say(pc, 11000043, 131, "这里提供「饰品制作」、「宝石研磨」$R;" +
                                   "以及「金属加工」。$R;" +
                                   "$P如果客人亲自带材料过来的话，$R;" +
                                   "我们会免费帮你制作的。$R;", "宝石商人");
        }

        void 製作活動木偶礦石精靈(ActorPC pc)
        {
            Say(pc, 11000043, 131, "客人!$R;" +
                                   "$R你手上拿的那个是$R;" +
                                   "『含有不明物体的宝石』吗?$R;", "宝石商人");

            switch (Select(pc, "手上拿的是『含有不明物体的宝石』吗?", "", "是", "不是"))
            {
                case 1:
                    Say(pc, 11000043, 131, "在这颗宝石里面，$R;" +
                                           "附有在其他世界中，$R;" +
                                           "生活在异界生物的灵魂。$R;" +
                                           "$P如果诚心擦拭的话，$R;" +
                                           "传说会借给擦拭的人$R;" +
                                           "他们强大的力量。$R;" +
                                           "$P怎么样?$R;" +
                                           "$R虽然不知道能不能成功，$R;" +
                                           "但是想不想擦擦看?$R;", "宝石商人");

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
                            Say(pc, 0, 0, "『含有不明物体的宝石』$R;" +
                                          "变成『活动木偶 矿石精灵』了!$R;", " ");

                            Say(pc, 11000043, 131, "哇阿…真是太美丽了!$R;" +
                                                   "$R一定要好好珍藏啊!!$R;", "宝石商人");
                            break;

                        case 2:
                            Say(pc, 11000043, 131, "觉得原来的样子也很漂亮啊?$R;", "宝石商人");
                            break;
                    }
                    return;

                case 2:
                    Say(pc, 11000043, 131, "啊…不好意思!!$R;" +
                                           "$R好像是…错觉。$R;", "宝石商人");
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

            Say(pc, 11000043, 131, "好漂亮的金属啊!$R;" +
                                   "你还有『别针』啊?!$R;" +
                                   "$R我帮你加工做成『项链坠』吧!$R;" +
                                   "$P只要你把『那个金属』、『别针』$R;" +
                                   "以及『10000金币』的制作费用给我，$R;" +
                                   "我就能帮你制作成『项链坠』喔!!", "宝石商人");

            switch (Select(pc, "想要加工吗?", "", "加工", "不加工"))
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
                        Say(pc, 0, 0, "给他『不明的合金破片』和『别针』!$R;", " ");

                        Say(pc, 11000043, 131, "来! 做好了!$R;", "宝石商人");

                        if (CheckInventory(pc, 50050102, 1))
                        {
                            詩迪的項鏈墜製作完成(pc);
                        }
                        else
                        {
                            Say(pc, 11000043, 131, "行李太多，无法给您啊!$R;", "宝石商人");                  
                        }
                    }
                    else
                    {
                        Say(pc, 11000043, 131, "真是的! 钱带不够啊!$R;", "宝石商人");
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
            Say(pc, 11000043, 131, "得到『诗迪的项链坠』!$R;", " ");

            Say(pc, 11000043, 131, "这个金属中好像拥有特别的力量啊!$R;" +
                                   "$R这个力量一定会好好的保护好你的!$R;" +
                                   "$P那么欢迎下次再度光临!!$R;", "宝石商人");
        }
    }
}
