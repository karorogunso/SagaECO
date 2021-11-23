using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30001001
{
    public class S11000222 : Event
    {
        public S11000222()
        {
            this.EventID = 11000222;
        }

        public override void OnEvent(ActorPC pc)
        {
            //万圣节部分
            BitMask<wsj> wsj_mask = new BitMask<wsj>(pc.CMask["wsj"]);
            //int selection;
            if (wsj_mask.Test(wsj.界石))
            {
                Say(pc, 131, "破結界石？$R;" +
                "$R珍しいものをお探しね……。$R;" +
                "$Pあれは、軍が管理している$R;" +
                "貴重なアイテムよ。$R;" +
                "$R私たちのような一般人が$R;" +
                "やすやす入手できるものではないわ。$R;" +
                "$Pふふふ……。$R;" +
                "そんなに悲しい顔をしないで。$R;" +
                "$R軍の人からもらえばいいじゃない。$R;" +
                "$Pあそこの人たちは、異性に弱いのよ。$R;" +
                "$Rなにか、プレゼントすれば$R;" +
                "すぐに言うことを聞いてくれるはず。$R;" +
                "$Pそうね、この街に詳しい人なら$R;" +
                "彼らの好みも$R;" +
                "把握してるんじゃなくって？$R;", "魔法屋店員");
                wsj_mask.SetValue(wsj.对话, true);
                wsj_mask.SetValue(wsj.界石, false);
                return;
            }
            
            if (pc.Quest != null)
            {
                if (pc.Quest.ID == 12010017)
                {
                    妈妈的药(pc);
                    return;
                }
            }
            switch (Select(pc, "…欢迎光临", "", "买东西", "卖东西", "什么也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 74);
                    break;
                case 2:
                    OpenShopSell(pc, 74);
                    break;
            }
            Say(pc, 131, "哎...以后再来喔！$R;");
        }

        void 妈妈的药(ActorPC pc)
        {
            BitMask<NDFlags> mask = new BitMask<NDFlags>(pc.CMask["ND"]);
            if (CountItem(pc, 10000109) >= 1)
            {
                Say(pc, 131, "吃完药，安定下来$R;" +
                    "会好起来的$R;");
                return;
            }
            if (mask.Test(NDFlags.諾頓的妙藥))
            {
                if (CheckInventory(pc, 10000109, 1))
                {
                    mask.SetValue(NDFlags.諾頓的妙藥, false);
                    GiveItem(pc, 10000109, 1);
                    Say(pc, 131, "来，接著吧……$R;");
                    Say(pc, 0, 131, "得到了『北国的灵药』$R;");
                    return;
                }
                Say(pc, 131, "现在要给您药呀$R;" +
                    "先减轻行李吧$R;");
                return;
            }
            Say(pc, 131, "『北国的灵药』？$R;" +
                "您找的东西很珍贵呀…$R;" +
                "$P那是这诺森市自古以来$R;" +
                "传下来的魔法之药啊$R;" +
                "$R很抱歉，$R;" +
                "这店没有那种东西$R;" +
                "$P哎…不用失望$R;" +
                "$R我知道制作方法呀$R;" +
                "只要您拿到材料就给您做唷$R;");
            switch (Select(pc, "做北国的灵药吗？", "", "做", "不做"))
            {
                case 1:
                    if (CountItem(pc, 10043201) >= 1 && CountItem(pc, 10043203) >= 1 && CountItem(pc, 10043204) >= 1 && CountItem(pc, 10043207) >= 1 && CountItem(pc, 10043208) >= 1 && CountItem(pc, 10043209) >= 1)
                    {
                        mask.SetValue(NDFlags.諾頓的妙藥, true);
                        TakeItem(pc, 10043201, 1);
                        TakeItem(pc, 10043203, 1);
                        TakeItem(pc, 10043204, 1);
                        TakeItem(pc, 10043207, 1);
                        TakeItem(pc, 10043208, 1);
                        TakeItem(pc, 10043209, 1);
                        Say(pc, 131, "剩下的交给我吧$R;" +
                            "$P唉……有点累了$R;" +
                            "$R先躺下休息一会儿怎么样？$R;" +
                            "我会把药做好的…$R;");
                        PlaySound(pc, 4001, false, 100, 50);
                        Wait(pc, 12000);
                        Say(pc, 131, "哎…疲劳消失了$R;" +
                            "$R药也做好了$R;");
                        if (CheckInventory(pc, 10000109, 1))
                        {
                            mask.SetValue(NDFlags.諾頓的妙藥, false);
                            GiveItem(pc, 10000109, 1);
                            Say(pc, 131, "来，拿着吧……$R;");
                            Say(pc, 0, 131, "得到了『北国的灵药』$R;");
                            return;
                        }
                        Say(pc, 131, "现在要给您药呀$R;" +
                            "先减轻行李吧$R;");
                        return;
                    }
                    Say(pc, 131, "材料需要各种属性$R;" +
                        "共6种的花瓣，各一片$R;" +
                        "$R属性的花$R;" +
                        "生长在阿克罗尼亚大陆各地$R;" +
                        "把那个找来吧$R;");
                    break;
                case 2:
                    Say(pc, 131, "哎...以后再来啊！$R;");
                    break;
            }
        }

    }
}
