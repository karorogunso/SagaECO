using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10056000
{
    public class S11000643 : Event
    {
        public S11000643()
        {
            this.EventID = 11000643;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<NNCYFlags> mask = pc.CMask["NNCY"];
            if (!mask.Test(NNCYFlags.得到歡迎之花))//_4a85)
            {
                Say(pc, 131, "欢迎光临！$R;" +
                    "农业王国法伊斯特！$R;" +
                    "$R请收下欢迎花饰！$R;" +
                    "$R法伊斯特市在东边$R;" +
                    "沿着路走过去就可以！$R;");
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
                 "农业王国法伊斯特！$R;" +
                 "$R法伊斯特市在东边$R;" +
                 "沿着路走过去就可以！$R;");

        }
    }
}