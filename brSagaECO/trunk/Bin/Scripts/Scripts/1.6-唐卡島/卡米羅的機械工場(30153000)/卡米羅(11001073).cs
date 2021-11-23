using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30153000
{
    public class S11001073 : Event
    {
        public S11001073()
        {
            this.EventID = 11001073;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 255, "嗯……$R;");
            Say(pc, 11001131, 255, "卡米羅先生，有客人來了。$R;");
            Say(pc, 131, "嗯？啊…謝謝$R;");
            switch (Select(pc, "歡迎光臨", "", "為雙腳步行機器人塗上顏色", "委託組裝機械", "開『集裝箱』", "什麼也不做"))
            {
                case 1:
                    //_2b41 = true;
                    Say(pc, 131, "為雙腳步行機器人塗上顏色$R;" +
                        "先全部分解，$R;" +
                        "然後在每個部件塗上顏色，$R;" +
                        "這不是人人都能做的喔。$R;" +
                        "只要拿來顏料就行了$R;" +
                        "就可以把雙腳步行機器人的$R顏色換掉唷。$R;");
                    //GOTO EVT1100107301
                    break;
                case 2:
                    Synthese(pc, 2039, 3);
                    break;
                case 3:
                    Synthese(pc, 2106, 1);
                    break;
                case 4:
                    break;
            }
        }
    }
}