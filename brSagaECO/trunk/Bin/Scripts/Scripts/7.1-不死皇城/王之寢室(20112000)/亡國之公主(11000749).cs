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
                
                    switch (Select(pc, "怎麼辦?", "", "給她耳環", "什麽都不做"))
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
                                    "$P這裡!媽媽!$R;" +
                                    "$R嗚嗚…$R;" +
                                    "那個裙子還是跟那隻耳環最配了$R;" +
                                    "$P對了!為了表示謝意$R;" +
                                    "送給您我們這的寶物吧$R;" +
                                    "$R感到光榮吧！$R;");
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
                                "丢了嗎？$R;");
                            switch (Select(pc, "怎麼辦?", "", "没丢！", "丢了！"))
                            {
                                case 1:
                                    Say(pc, 131, "那麽快點拿過來吧！$R;");
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
                                    Say(pc, 131, "没辦法$R;" +
                                        "没關係！可以找回來的$R;");
                                    break;
                            }
                            break;
                    }
                return;
            }
            if (mask.Test(BSHCFlags.開始尋找耳環))
            {
                Say(pc, 131, "嗚嗚！嗚嗚…$R;" +
                    "$R找找媽媽的耳環吧！$R;" +
                    "幫我找找耳環吧！$R;" +
                    "幫忙找找！幫忙找找！幫忙…$R;");
                return;
            }
            mask.SetValue(BSHCFlags.開始尋找耳環, true);
            Say(pc, 131, "爸爸和媽媽都在喔！$R;" +
                "今天…真是…好天氣$R;" +
                "$P什麽?媽媽説丢了耳環?!!$R;" +
                "$R幫忙找找…$R;");
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
            Say(pc, 131, "什麽東西掉了…$R;");
            if (mask.Test(BSHCFlags.右耳環位置1) && !mask.Test(BSHCFlags.找到右耳環) && CheckInventory(pc, 10013600, 1))
            {
                mask.SetValue(BSHCFlags.找到右耳環, true);
                GiveItem(pc, 10013600, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 131, "撿到了『王妃的耳環（右）』！$R;");
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
            Say(pc, 131, "什麽東西掉了…$R;");
            if (mask.Test(BSHCFlags.右耳環位置2) && !mask.Test(BSHCFlags.找到右耳環) && CheckInventory(pc, 10013600, 1))
            {
                mask.SetValue(BSHCFlags.找到右耳環, true);
                GiveItem(pc, 10013600, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 131, "撿到了『王妃的耳環（右）』！$R;");
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
            Say(pc, 131, "什麽東西掉了…$R;");
            if (mask.Test(BSHCFlags.右耳環位置3) && !mask.Test(BSHCFlags.找到右耳環) && CheckInventory(pc, 10013600, 1))
            {
                mask.SetValue(BSHCFlags.找到右耳環, true);
                GiveItem(pc, 10013600, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 131, "撿到了『王妃的耳環（右）』！$R;");
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
            Say(pc, 131, "什麽東西掉了…$R;");
            if (mask.Test(BSHCFlags.左耳環位置1) && !mask.Test(BSHCFlags.找到左耳環) && CheckInventory(pc, 10013650, 1))
            {
                mask.SetValue(BSHCFlags.找到左耳環, true);
                GiveItem(pc, 10013650, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 131, "撿到了『王妃的耳環（左）』！$R;");
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
            Say(pc, 131, "什麽東西掉了…$R;");
            if (mask.Test(BSHCFlags.左耳環位置2) && !mask.Test(BSHCFlags.找到左耳環) && CheckInventory(pc, 10013650, 1))
            {
                mask.SetValue(BSHCFlags.找到左耳環, true);
                GiveItem(pc, 10013650, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 131, "撿到了『王妃的耳環（左）』！$R;");
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
            Say(pc, 131, "什麽東西掉了…$R;");
            if (mask.Test(BSHCFlags.左耳環位置3) && !mask.Test(BSHCFlags.找到左耳環) && CheckInventory(pc, 10013650, 1))
            {
                mask.SetValue(BSHCFlags.找到左耳環, true);
                GiveItem(pc, 10013650, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 131, "撿到了『王妃的耳環（左）』！$R;");
                return;
            }
            PlaySound(pc, 2041, false, 100, 50);
            Say(pc, 131, "是玻璃碎片$R;");
        }
    }
}