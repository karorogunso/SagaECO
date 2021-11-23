using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10056000
{
    public class S11000642 : Event
    {
        public S11000642()
        {
            this.EventID = 11000642;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<NNCYFlags> mask = pc.CMask["NNCY"];
            if (!mask.Test(NNCYFlags.得到歡迎之花))//_4a85)
            {
                Say(pc, 131, "歡迎光臨！$R;" +
                    "這裡是農業王國帕斯特！$R;" +
                    "$R歡迎，請接受歡迎之花！$R;" +
                    "$P去首都帕斯特市，從這裡往東走$R;" +
                    "很快就到了$R;" +
                    "$R一定要去看看啊！$R;");
                int a = Global.Random.Next(1, 3);
                switch (a)
                {
                    case 1:
                        if (CheckInventory(pc, 50051450, 1))
                        {
                            mask.SetValue(NNCYFlags.得到歡迎之花, true);
                            //_4a85 = true;
                            GiveItem(pc, 50051450, 1);
                            PlaySound(pc, 2040, false, 100, 50);
                            Say(pc, 131, "歡迎，收到歡迎之花！$R;");
                            return;
                        }
                        PlaySound(pc, 2041, false, 100, 50);
                        Say(pc, 131, "行李太多了$R;");
                        break;
                    case 2:
                        if (CheckInventory(pc, 50051451, 1))
                        {
                            mask.SetValue(NNCYFlags.得到歡迎之花, true);
                            //_4a85 = true;
                            GiveItem(pc, 50051451, 1);
                            PlaySound(pc, 2040, false, 100, 50);
                            Say(pc, 131, "得到歡迎之花！$R;");
                            return;
                        }
                        PlaySound(pc, 2041, false, 100, 50);
                        Say(pc, 131, "行李太多了$R;");
                        break;
                    case 3:
                        if (CheckInventory(pc, 50051452, 1))
                        {
                            mask.SetValue(NNCYFlags.得到歡迎之花, true);
                            //_4a85 = true;
                            GiveItem(pc, 50051452, 1);
                            PlaySound(pc, 2040, false, 100, 50);
                            Say(pc, 131, "得到歡迎之花！$R;");
                            return;
                        }
                        PlaySound(pc, 2041, false, 100, 50);
                        Say(pc, 131, "行李太多了$R;");
                        break;
                }
                return;
            }
            Say(pc, 131, "歡迎光臨！$R;" +
                "這裡是農業王國帕斯特！$R;" +
                "$R帕斯特是是群山環繞的$R田園風格的國家$R;" +
                "您肯定會喜歡的！$R;");
        }
    }
}