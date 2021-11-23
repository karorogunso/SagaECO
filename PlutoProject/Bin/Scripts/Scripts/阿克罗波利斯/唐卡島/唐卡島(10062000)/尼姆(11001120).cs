using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10062000
{
    public class S11001120 : Event
    {
        public S11001120()
        {
            this.EventID = 11001120;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Marionette != null)
            {
                Say(pc, 131, "哎呀~真是…$R;" +
                    "$R我……我们的主人……$R;" +
                    "一…一定会成为世界最棒的工匠…$R;");
                Wait(pc, 0);
                ShowEffect(pc, 11001120, 4539);
                Wait(pc, 1000);
                Say(pc, 131, "嗯？$R;" +
                    "好像有人类，$R;" +
                    "$R是我的错觉吗？$R;");
                return;
            }
            Say(pc, 131, "……$R;");
            Say(pc, 11001069, 131, "这孩子可能怕人类，所以不跟人类说话。$R;" +
                "$R请不要生气唷$R;");
            Say(pc, 131, "什么？怎么胡乱说话呢？$R;" +
                "$R我这个身体没有理由怕人类呀$R;" +
                "$P像你一样的东西，我看到都烦！$R;" +
                "滚！！$R;");
            Say(pc, 11001069, 131, "知道了，是，夫人$R;");
        }
    }
}