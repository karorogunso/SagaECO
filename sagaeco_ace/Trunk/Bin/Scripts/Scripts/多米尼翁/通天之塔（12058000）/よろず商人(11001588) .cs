using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M12002101
{
    public class S11001588 : Event
    {
        public S11001588()
        {
            this.EventID = 11001588;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_09> Neko_09_mask = new BitMask<Neko_09>(pc.CMask["Neko_09"]);
            //int selection;
            Say(pc, 131, "欢迎光临。$R;" +
            "$R累的话、可以进帐篷を$R;" +
            "休息下出来。$R;", "万能商人");
            switch (Select(pc, "有什么可以帮忙？", "", "买东西", "卖东西", "到银行存款", "到银行取款", "使用仓库（500G）", "休息（300G）", "什么也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 265);
                    break;
                case 2:
                    OpenShopSell(pc, 265);
                    break;
                case 3:
                    BankDeposit(pc);
                    break;
                case 4:
                    BankWithdraw(pc);
                    break;
                case 5:
                    OpenWareHouse(pc, SagaDB.Item.WarehousePlace.TowerGoesToHeaven);
                    break;
                case 6:
                    if (CountItem(pc, 10002002) > 0)
                    {
                        if (Neko_09_mask.Test(Neko_09.获得灵魂碎片_01) &&
                            Neko_09_mask.Test(Neko_09.获得灵魂碎片_02) &&
                            Neko_09_mask.Test(Neko_09.获得灵魂碎片_03) &&
                            Neko_09_mask.Test(Neko_09.获得灵魂碎片_04))
                        {
                            if (pc.Gold < 300)
                            {
                                Say(pc, 131, "金幣不足$R;");
                                return;
                            }
                            Say(pc, 131, "……えっ！？$R;" +
                            "夜になったら起こして欲しい？$R;" +
                            "$Rいや、お金さえ払ってくれれば$R;" +
                            "別にかまいませんけどね。$R;" +
                            "$R軍艦島への飛空庭は動いてませんよ？$R;", "よろず商人");
                            if (Select(pc, "どうする？", "", "じゃあ、やっぱやめる。", "かまわない") == 2)
                            {
                                Say(pc, 131, "じゃあ、夜になったら起こしますね。$R;", "よろず商人");

                                Say(pc, 131, "それじゃ、おやすみなさい～。$R;", "よろず商人");
                                pc.Gold -= 300;
                                PlaySound(pc, 4001, false, 100, 50);
                                Warp(pc, 50063000, 121, 236);
                            }
                            return;
                        }
                    }
                    if (pc.Gold < 300)
                    {
                        Say(pc, 131, "金幣不足$R;");
                        return;
                    }
                    pc.Gold -= 300;
                    PlaySound(pc, 4001, false, 100, 50);
                    Fade(pc, FadeType.Out, FadeEffect.Black);
                    //FADE OUT BLACK
                    Wait(pc, 10000);
                    Fade(pc, FadeType.In, FadeEffect.Black);
                    //FADE IN
                    Heal(pc);
                    Wait(pc, 1000);
                    Say(pc, 131, "那么、晚安～。$R;", "万能商人");
                    Wait(pc, 8250);
                    Wait(pc, 990);
                    break;
            }

        }

    }

}
            
            
        
     
    