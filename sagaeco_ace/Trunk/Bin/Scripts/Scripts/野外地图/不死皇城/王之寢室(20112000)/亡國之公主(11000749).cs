using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20112000
{
    public class S11000749 : Event
    {
        public S11000749()
        {
            this.EventID = 11000749;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<BSHCFlags> mask = pc.CMask["BSHC"];
            int a;
            if (mask.Test(BSHCFlags.找到右耳環) && mask.Test(BSHCFlags.找到左耳環))
            {
                
                    switch (Select(pc, "怎么办?", "", "给她耳环", "什么都不做"))
                    {
                        case 1:
                            if (CountItem(pc, 10013600) >= 1 && CountItem(pc, 10013650) >= 1)
                            {
                                TakeItem(pc, 10013600, 1);
                                TakeItem(pc, 10013650, 1);
                                mask.SetValue(BSHCFlags.開始尋找耳環, false);
                                mask.SetValue(BSHCFlags.找到右耳環, false);
                                mask.SetValue(BSHCFlags.找到左耳環, false);
                                mask.SetValue(BSHCFlags.右耳環位置1, false);
                                mask.SetValue(BSHCFlags.右耳環位置2, false);
                                mask.SetValue(BSHCFlags.右耳環位置3, false);
                                mask.SetValue(BSHCFlags.左耳環位置1, false);
                                mask.SetValue(BSHCFlags.左耳環位置2, false);
                                mask.SetValue(BSHCFlags.左耳環位置3, false);
                                Say(pc, 131, "啊！找到了！$R;" +
                                    "辛苦了！$R;" +
                                    "$P这里!妈妈!$R;" +
                                    "$R呜呜…$R;" +
                                    "那个裙子还是跟那只耳环最配了$R;" +
                                    "$P对了!为了表示谢意$R;" +
                                    "送给您我们这的宝物吧$R;" +
                                    "$R接受这份荣耀吧！$R;");
                                a = Global.Random.Next(1, 3);
                                switch (a)
                                {
                                    case 1:
                                        OpenShopBuy(pc, 143);
                                        break;
                                    case 2:
                                        OpenShopBuy(pc, 144);
                                        break;
                                    case 3:
                                        OpenShopBuy(pc, 145);
                                        break;
                                }
                                return;
                            }
                            Say(pc, 131, "没有啊！$R;" +
                                "丢了吗？$R;");
                            switch (Select(pc, "怎么办?", "", "没丢！", "丢了！"))
                            {
                                case 1:
                                    Say(pc, 131, "那么快点拿过来吧！$R;");
                                    break;
                                case 2:
                                    mask.SetValue(BSHCFlags.開始尋找耳環, false);
                                    mask.SetValue(BSHCFlags.找到右耳環, false);
                                    mask.SetValue(BSHCFlags.找到左耳環, false);
                                    mask.SetValue(BSHCFlags.右耳環位置1, false);
                                    mask.SetValue(BSHCFlags.右耳環位置2, false);
                                    mask.SetValue(BSHCFlags.右耳環位置3, false);
                                    mask.SetValue(BSHCFlags.左耳環位置1, false);
                                    mask.SetValue(BSHCFlags.左耳環位置2, false);
                                    mask.SetValue(BSHCFlags.左耳環位置3, false);
                                    TakeItem(pc, 10013600, 1);
                                    TakeItem(pc, 10013650, 1);
                                    Say(pc, 131, "没办法$R;" +
                                        "没关系！可以找回來的$R;");
                                    break;
                            }
                            break;
                    }
                return;
            }
            if (mask.Test(BSHCFlags.開始尋找耳環))
            {
                Say(pc, 131, "呜呜！呜呜…$R;" +
                    "$R找找妈妈的耳环吧！$R;" +
                    "帮我找找耳环吧！$R;" +
                    "帮忙找找！帮忙找找！帮忙…$R;");
                return;
            }
            mask.SetValue(BSHCFlags.開始尋找耳環, true);
            Say(pc, 131, "爸爸和妈妈都在喔！$R;" +
                "今天…真是…好天气$R;" +
                "$P什么?妈妈说丢了耳环?!!$R;" +
                "$R帮忙找找…$R;");
            a = Global.Random.Next(1, 3);
            switch (a)
            {
                case 1:
                    mask.SetValue(BSHCFlags.右耳環位置1, true);
                    break;
                case 2:
                    mask.SetValue(BSHCFlags.右耳環位置2, true);
                    break;
                case 3:
                    mask.SetValue(BSHCFlags.右耳環位置3, true);
                    break;
            }

            a = Global.Random.Next(1, 3);
            switch (a)
            {
                case 1:
                    mask.SetValue(BSHCFlags.左耳環位置1, true);
                    break;
                case 2:
                    mask.SetValue(BSHCFlags.左耳環位置2, true);
                    break;
                case 3:
                    mask.SetValue(BSHCFlags.左耳環位置3, true);
                    break;
            }
        }
    }

    public class S12001091 : Event
    {
        public S12001091()
        {
            this.EventID = 12001091;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<BSHCFlags> mask = pc.CMask["BSHC"];
            if (!mask.Test(BSHCFlags.開始尋找耳環))
            {
                return;
            }
            Say(pc, 131, "什么东西掉了…$R;");
            if (mask.Test(BSHCFlags.右耳環位置1) && !mask.Test(BSHCFlags.找到右耳環) && CheckInventory(pc, 10013600, 1))
            {
                mask.SetValue(BSHCFlags.找到右耳環, true);
                GiveItem(pc, 10013600, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 131, "捡到了『王妃的耳环（右）』！$R;");
                return;
            }
            PlaySound(pc, 2041, false, 100, 50);
            Say(pc, 131, "是玻璃碎片$R;");
        }
    }

    public class S12001092 : Event
    {
        public S12001092()
        {
            this.EventID = 12001092;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<BSHCFlags> mask = pc.CMask["BSHC"];
            if (!mask.Test(BSHCFlags.開始尋找耳環))
            {
                return;
            }
            Say(pc, 131, "什么东西掉了…$R;");
            if (mask.Test(BSHCFlags.右耳環位置2) && !mask.Test(BSHCFlags.找到右耳環) && CheckInventory(pc, 10013600, 1))
            {
                mask.SetValue(BSHCFlags.找到右耳環, true);
                GiveItem(pc, 10013600, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 131, "捡到了『王妃的耳环（右）』！$R;");
                return;
            }
            PlaySound(pc, 2041, false, 100, 50);
            Say(pc, 131, "是玻璃碎片$R;");
        }
    }

    public class S12001093 : Event
    {
        public S12001093()
        {
            this.EventID = 12001093;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<BSHCFlags> mask = pc.CMask["BSHC"];
            if (!mask.Test(BSHCFlags.開始尋找耳環))
            {
                return;
            }
            Say(pc, 131, "什么东西掉了…$R;");
            if (mask.Test(BSHCFlags.右耳環位置3) && !mask.Test(BSHCFlags.找到右耳環) && CheckInventory(pc, 10013600, 1))
            {
                mask.SetValue(BSHCFlags.找到右耳環, true);
                GiveItem(pc, 10013600, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 131, "捡到了『王妃的耳環（右）』！$R;");
                return;
            }
            PlaySound(pc, 2041, false, 100, 50);
            Say(pc, 131, "是玻璃碎片$R;");
        }
    }

    public class S12001094 : Event
    {
        public S12001094()
        {
            this.EventID = 12001094;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<BSHCFlags> mask = pc.CMask["BSHC"];
            if (!mask.Test(BSHCFlags.開始尋找耳環))
            {
                return;
            }
            Say(pc, 131, "什么东西掉了…$R;");
            if (mask.Test(BSHCFlags.左耳環位置1) && !mask.Test(BSHCFlags.找到左耳環) && CheckInventory(pc, 10013650, 1))
            {
                mask.SetValue(BSHCFlags.找到左耳環, true);
                GiveItem(pc, 10013650, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 131, "捡到了『王妃的耳环（左）』！$R;");
                return;
            }
            PlaySound(pc, 2041, false, 100, 50);
            Say(pc, 131, "是玻璃碎片$R;");
        }
    }

    public class S12001095 : Event
    {
        public S12001095()
        {
            this.EventID = 12001095;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<BSHCFlags> mask = pc.CMask["BSHC"];
            if (!mask.Test(BSHCFlags.開始尋找耳環))
            {
                return;
            }
            Say(pc, 131, "什么东西掉了…$R;");
            if (mask.Test(BSHCFlags.左耳環位置2) && !mask.Test(BSHCFlags.找到左耳環) && CheckInventory(pc, 10013650, 1))
            {
                mask.SetValue(BSHCFlags.找到左耳環, true);
                GiveItem(pc, 10013650, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 131, "捡到了『王妃的耳环（左）』！$R;");
                return;
            }
            PlaySound(pc, 2041, false, 100, 50);
            Say(pc, 131, "是玻璃碎片$R;");
        }
    }

    public class S12001096 : Event
    {
        public S12001096()
        {
            this.EventID = 12001096;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<BSHCFlags> mask = pc.CMask["BSHC"];
            if (!mask.Test(BSHCFlags.開始尋找耳環))
            {
                return;
            }
            Say(pc, 131, "什么东西掉了…$R;");
            if (mask.Test(BSHCFlags.左耳環位置3) && !mask.Test(BSHCFlags.找到左耳環) && CheckInventory(pc, 10013650, 1))
            {
                mask.SetValue(BSHCFlags.找到左耳環, true);
                GiveItem(pc, 10013650, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 131, "捡到了『王妃的耳环（左）』！$R;");
                return;
            }
            PlaySound(pc, 2041, false, 100, 50);
            Say(pc, 131, "是玻璃碎片$R;");
        }
    }
}