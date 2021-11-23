using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10017001
{
    public class S11000537 : Event
    {
        public S11000537()
        {
            this.EventID = 11000537;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Account.GMLevel >= 1)
            {
                switch (Select(pc, "回去吗？", "", "回去", "交换种子", "还不回去"))
                {
                    case 1:
                        Warp(pc, 10017000, 134, 69);
                        break;
                    case 2:
                        Synthese(pc, 2025, 1);
                        break;
                    case 3:
                        break;
                }
                return;
            }
            switch (Select(pc, "回去吗?", "", "回去", "还不回去"))
            {
                case 1:
                    Warp(pc, 10017000, 134, 69);
                    break;
                case 2:
                    break;
            }
            /*
            switch (Select(pc, "回去嗎?", "", "回去", "還不回去", "問關於櫻花的問題"))
            {
                case 1:
                    Warp(pc, 10017000, 134, 69);
                    break;
                case 2:
                    break;
                case 3:
                    ShowEffect(pc, 11000537, 4504);
                    Say(pc, 131, "這個櫻花是以前戰爭結束後$R;" +
                        "農夫們看到荒廢的農地感到心疼$R;" +
                        "爲了祈願平和而種的樹$R;" +
                        "$P之後很多農夫們在駐守著這段路$R;");
                    break;
            }//*/
        }
    }
}