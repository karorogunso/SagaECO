using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10062000
{
    public class S11001024 : Event
    {
        public S11001024()
        {
            this.EventID = 11001024;
        }

        public override void OnEvent(ActorPC pc)
        {
            int a = Global.Random.Next(1, 2);
            if (a == 1)
            {
                Say(pc, 131, "现在是训练时间，$R;" +
                    "请不要妨碍我们阿。$R;");
            }
            else
            {
                Say(pc, 131, "如果敌方的兵士凭依过来，$R;" +
                    "一定要集中精神喔，$R;" +
                    "$P要不就不能知道对方是否存在的$R;" +
                    "即使被凭依了，也不要放弃呀，$R;" +
                    "$R一定要努力抵抗到底，$R;" +
                    "敌人就不能操纵你们了$R;" +
                    "知道了吗？$R;" +
                    "$P那么开始下一步训练，$R;" +
                    "下一步是…$R;");
            }
        }
    }
}