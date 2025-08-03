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
                Say(pc, 131, "欢迎光临！$R;" +
                    "这里是农业王国法伊斯特！$R;" +
                    "$R欢迎，请接受欢迎花饰！$R;" +
                    "$P去首都法伊斯特市，从这里往东走$R;" +
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
                            Say(pc, 131, "欢迎，给您欢迎花饰！$R;");
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
                            Say(pc, 131, "得到欢迎花饰！$R;");
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
                            Say(pc, 131, "得到欢迎花饰！$R;");
                            return;
                        }
                        PlaySound(pc, 2041, false, 100, 50);
                        Say(pc, 131, "行李太多了$R;");
                        break;
                }
                return;
            }
            Say(pc, 131, "欢迎光临！$R;" +
                "这里是农业王国法伊斯特！$R;" +
                "$R法伊斯特是是群山环绕的$R田园风格的国家$R;" +
                "您肯定会喜欢的！$R;");
        }
    }
}