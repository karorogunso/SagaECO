using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30157000
{
    public class S11001068 : Event
    {
        public S11001068()
        {
            this.EventID = 11001068;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, 131, "咇！咇咇！咇咇咇~！$R;", "通信機");
            Say(pc, 255, "代表先生。$R;" +
                "$R警備機器人來電話啊。$R;");
            Say(pc, 11001052, 131, "接一下$R;");
            Say(pc, 0, 131, "咇！咇咇！咇咇咇~！$R;" +
                "$P這裡是警備機器人，$R;" +
                "在路上發現停止的機器人。$R;" +
                "$R因為妨礙交通，要撤下來，請確認。$R;", "通信機");
            Say(pc, 11001052, 131, "嗯，$R;" +
                "$P嗯…先等等$R;" +
                "在哪裡發生的事情？$R;");
            Say(pc, 0, 131, "是機械神匠總部前。$R;", "通信機");
            Say(pc, 11001052, 131, "那麼，等等$R;" +
                "$P不是您們的機器人嗎？（悄悄話）$R;");
            Say(pc, 255, "是，對不起（悄悄話）$R;");
            Say(pc, 11001052, 131, "嗚嗚……$R;" +
                "$R警備機器人，聽見我說的話嗎？$R;" +
                "不要撤出，專心警備工作吧。$R;");
            Say(pc, 0, 131, "什麼理由呢？$R;" +
                "$R我是根據規則，$R要把停止在路上的$R機器人撤出的。$R;", "通信機");
            Say(pc, 11001052, 131, "理由嗎…$R;" +
                "是因為…它是我們的機器人…$R;");
            Say(pc, 0, 131, "那個不算理由$R;", "通信機");
            Say(pc, 11001052, 131, "我會馬上清除的…給點面子嘛…$R;");
            Say(pc, 0, 131, "那是違反規則$R;" +
                "不能聽從命令$R;" +
                "$R開始撤除了$R;", "通信機");
            Say(pc, 0, 131, "卡卡卡卡！$R;" +
                "轟轟~~~~~~~！！！！$R;", "從外邊傳來的聲音");
            Say(pc, 255, "以上，報告完畢$R;");
        }
    }
}