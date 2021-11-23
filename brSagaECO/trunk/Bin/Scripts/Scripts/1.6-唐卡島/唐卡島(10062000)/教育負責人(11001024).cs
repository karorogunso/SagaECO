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
                Say(pc, 131, "現在是訓練時間，$R;" +
                    "請不要妨礙我們阿。$R;");
            }
            else
            {
                Say(pc, 131, "如果敵方的兵士憑依過來，$R;" +
                    "一定要集中精神喔，$R;" +
                    "$P要不就不能知道對方是否存在的$R;" +
                    "即使被憑依了，也不要放棄呀，$R;" +
                    "$R一定要努力抵抗到底，$R;" +
                    "敵人就不能操縱你們了$R;" +
                    "知道了嗎？$R;" +
                    "$P那麼開始下一步訓練，$R;" +
                    "下一步是…$R;");
            }
        }
    }
}