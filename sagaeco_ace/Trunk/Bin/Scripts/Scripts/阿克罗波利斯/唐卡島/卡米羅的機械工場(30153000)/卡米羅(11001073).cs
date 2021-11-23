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
            Say(pc, 11001131, 255, "卡米罗先生，有客人来了。$R;");
            Say(pc, 131, "嗯？啊…谢谢$R;");
            switch (Select(pc, "欢迎光临", "", "为双足步行机器人涂上颜色", "委托组装机械", "开『集装箱』", "什么也不做"))
            {
                case 1:
                    //_2b41 = true;
                    Say(pc, 131, "为双足步行机器人涂上颜色$R;" +
                        "先全部分解，$R;" +
                        "然后在每个部件涂上颜色，$R;" +
                        "这不是人人都能做的喔。$R;" +
                        "只要拿来顏料就行了$R;" +
                        "就可以把双脚步行机器人的$R颜色换掉哦。$R;");
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