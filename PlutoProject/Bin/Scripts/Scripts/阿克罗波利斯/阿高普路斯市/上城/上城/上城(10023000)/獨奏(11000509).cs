using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:上城(10023000) NPC基本信息:獨奏(11000509) X:61 Y:129
namespace SagaScript.M10023000
{
    public class S11000509 : Event
    {
        public S11000509()
        {
            this.EventID = 11000509;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<LV35_Clothes_02> LV35_Clothes_02_mask = new BitMask<LV35_Clothes_02>(pc.CMask["LV35_Clothes_02"]);

            if (!LV35_Clothes_02_mask.Test(LV35_Clothes_02.獨奏的委託完成))
            {
                獨奏的委託(pc);
                return;
            }

            Say(pc, 11000509, 131, "嗨! 先看看再走吧?$R;", "独奏");

            switch (Select(pc, "想要买什么呢?", "", "皮鞋", "什么都不买"))
            {
                case 1:
                    OpenShopBuy(pc, 59);
                    break;

                case 2:
                    break;
            }
        }

        void 獨奏的委託(ActorPC pc)
        {
            if (pc.CInt["LV35_Clothes_02"] == 0)
            {
                獨奏告知缺少的材料(pc);
                return;
            }
            else
            {
                把材料交給獨奏(pc);
                return;
            }
        }

        void 獨奏告知缺少的材料(ActorPC pc)
        {
            int selection;

            if (pc.Level < 20)
            {
                Say(pc, 11000509, 131, "好久没来过「阿克罗波利斯」了，$R;" +
                                       "还是很多人啊!$R;", "独奏");

                selection = Global.Random.Next(1, 4);

                pc.CInt["LV35_Clothes_02"] = selection;

                switch (pc.CInt["LV35_Clothes_02"])
                {
                    case 1:
                        Say(pc, 11000509, 131, "……$R;" +
                                               "$P有60瓶『混浊的水』吗?$R;", "独奏");
                        break;

                    case 2:
                        Say(pc, 11000509, 131, "……$R;" +
                                               "$P有60个『植物的刺』吗?$R;", "独奏");
                        break;

                    case 3:
                        Say(pc, 11000509, 131, "……$R;" +
                                               "$P有20个『木炭』吗?$R;", "独奏");
                        break;

                    case 4:
                        Say(pc, 11000509, 131, "……$R;" +
                                               "$P有170个『投掷海胆』吗?$R;", "独奏");
                        break;
                }
            }
            else
            {
                Say(pc, 11000509, 131, "好久没来过「阿克罗波利斯」了，$R;" +
                                       "还是很多人啊!$R;", "独奏");

                selection = Global.Random.Next(5, 6);

                pc.CInt["LV35_Clothes_02"] = selection;

                switch (pc.CInt["LV35_Clothes_02"])
                {
                    case 5:
                        Say(pc, 11000509, 131, "……$R;" +
                                               "$P有60瓶『魔法水』吗?$R;", "独奏");
                        break;

                    case 6:
                        Say(pc, 11000509, 131, "……$R;" +
                                               "$P有10份『燐粉』吗?$R;", "独奏");
                        break;
                }
            }
        }

        void 把材料交給獨奏(ActorPC pc)
        {
            BitMask<LV35_Clothes_02> LV35_Clothes_02_mask = new BitMask<LV35_Clothes_02>(pc.CMask["LV35_Clothes_02"]);

            switch (pc.CInt["LV35_Clothes_02"])
            {
                case 1:
                    if (CountItem(pc, 10000210) >= 60)
                    {
                        LV35_Clothes_02_mask.SetValue(LV35_Clothes_02.獨奏的委託完成, true);

                        PlaySound(pc, 2040, false, 100, 50);
                        TakeItem(pc, 10000210, 60);
                        Say(pc, 0, 0, "把『混浊的水』交给他。$R;", " ");

                        Say(pc, 11000509, 131, "…真是感谢啊!$R;", "独奏");
                    }
                    else
                    {
                        Say(pc, 11000509, 131, "……$R;" +
                                               "$P有60瓶『混浊的水』吗?$R;", "独奏");
                    }
                    break;
                case 2:
                    if (CountItem(pc, 10025205) >= 60)
                    {
                        LV35_Clothes_02_mask.SetValue(LV35_Clothes_02.獨奏的委託完成, true);

                        PlaySound(pc, 2040, false, 100, 50);
                        TakeItem(pc, 10025205, 60);
                        Say(pc, 0, 0, "把『植物的刺』交给他。$R;", " ");

                        Say(pc, 11000509, 131, "…真是感谢啊!$R;", "独奏");
                    }
                    else
                    {
                        Say(pc, 11000509, 131, "……$R;" +
                                               "$P有60个『植物的刺』吗?$R;", "独奏");
                    }
                    break;
                case 3:
                    if (CountItem(pc, 10016800) >= 20)
                    {
                        LV35_Clothes_02_mask.SetValue(LV35_Clothes_02.獨奏的委託完成, true);

                        PlaySound(pc, 2040, false, 100, 50);
                        TakeItem(pc, 10016800, 20);
                        Say(pc, 0, 0, "把『木炭』交给他。$R;", " ");

                        Say(pc, 11000509, 131, "…真是感谢啊!$R;", "独奏");
                    }
                    else
                    {
                        Say(pc, 11000509, 131, "……$R;" +
                                               "$P有20个『木炭』吗?$R;", "独奏");
                    }
                    break;
                case 4:
                    if (CountItem(pc, 61010300) >= 170)
                    {
                        LV35_Clothes_02_mask.SetValue(LV35_Clothes_02.獨奏的委託完成, true);

                        PlaySound(pc, 2040, false, 100, 50);
                        TakeItem(pc, 61010300, 170);
                        Say(pc, 0, 0, "把『投掷海胆』交给他。$R;", " ");

                        Say(pc, 11000509, 131, "…真是感谢啊!$R;", "独奏");
                    }
                    else
                    {
                        Say(pc, 11000509, 131, "……$R;" +
                                               "$P有170个『投掷海胆』吗?$R;", "独奏");
                    }
                    break;
                case 5:
                    if (CountItem(pc, 10002202) >= 60)
                    {
                        LV35_Clothes_02_mask.SetValue(LV35_Clothes_02.獨奏的委託完成, true);

                        PlaySound(pc, 2040, false, 100, 50);
                        TakeItem(pc, 10002202, 60);
                        Say(pc, 0, 0, "把『魔法水』交给他。$R;", " ");

                        Say(pc, 11000509, 131, "…真是感谢啊!$R;", "独奏");
                    }
                    else
                    {
                        Say(pc, 11000509, 131, "……$R;" +
                                               "$P有60瓶『魔法水』吗?$R;", "独奏");
                    }
                    break;
                case 6:
                    if (CountItem(pc, 10001150) >= 10)
                    {
                        LV35_Clothes_02_mask.SetValue(LV35_Clothes_02.獨奏的委託完成, true);

                        PlaySound(pc, 2040, false, 100, 50);
                        TakeItem(pc, 10001150, 10);
                        Say(pc, 0, 0, "把『燐粉』交给他。$R;", " ");

                        Say(pc, 11000509, 131, "…真是感谢啊!$R;", "独奏");
                    }
                    else
                    {
                        Say(pc, 11000509, 131, "……$R;" +
                                               "$P有10份『燐粉』吗?$R;", "独奏");
                    }
                    break;
            }
        }
    }
}
