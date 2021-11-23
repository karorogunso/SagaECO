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
            Say(pc, 0, 131, "咇！咇咇！咇咇咇~！$R;", "通信机");
            Say(pc, 255, "代表先生。$R;" +
                "$R警备机器人来电话啊。$R;");
            Say(pc, 11001052, 131, "接一下$R;");
            Say(pc, 0, 131, "咇！咇咇！咇咇咇~！$R;" +
                "$P这里是警备机器人，$R;" +
                "在路上发现停止的机器人。$R;" +
                "$R因为妨碍交通，要撤下来，请确认。$R;", "通信机");
            Say(pc, 11001052, 131, "嗯，$R;" +
                "$P嗯…先等等$R;" +
                "在哪里发生的事情？$R;");
            Say(pc, 0, 131, "是机械神匠总部前。$R;", "通信机");
            Say(pc, 11001052, 131, "那么，等等$R;" +
                "$P不是您们的机器人吗？（悄悄话）$R;");
            Say(pc, 255, "是，对不起（悄悄话）$R;");
            Say(pc, 11001052, 131, "呜呜……$R;" +
                "$R警备机器人，听见我说的话吗？$R;" +
                "不要撤出，专心警备工作吧。$R;");
            Say(pc, 0, 131, "什么理由呢？$R;" +
                "$R我是根据规则，$R要把停止在路上的$R机器人撤出的。$R;", "通信机");
            Say(pc, 11001052, 131, "理由吗…$R;" +
                "是因为…它是我们的机器人…$R;");
            Say(pc, 0, 131, "那个不算理由$R;", "通信机");
            Say(pc, 11001052, 131, "我会马上清除的…给点面子嘛…$R;");
            Say(pc, 0, 131, "那是违反规则$R;" +
                "不能听从命令$R;" +
                "$R开始撤除了$R;", "通信机");
            Say(pc, 0, 131, "卡卡卡卡！$R;" +
                "轰轰~~~~~~~！！！！$R;", "从外边传来的声音");
            Say(pc, 255, "以上，报告完毕$R;");
        }
    }
}