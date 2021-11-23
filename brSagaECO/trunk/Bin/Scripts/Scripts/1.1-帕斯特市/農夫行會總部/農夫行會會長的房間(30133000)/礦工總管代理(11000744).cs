using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30133000
{
    public class S11000744 : Event
    {
        public S11000744()
        {
            this.EventID = 11000744;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000744, 190, "那邊的總管過得好嗎?$R;" +
                "$R嗚呼呼!這真是秘密喔$R;" +
                "我們總管好像喜歡盜賊總管！$R;");
            ShowEffect(pc, 11000745, 4501);
            Say(pc, 11000745, 190, "什麽？真的嗎？$R;" +
                "$R我們總管也好像看上了$R;" +
                "$R盜賊總管呢$R;");
            Say(pc, 11000744, 190, "那樣的話，是對手嗎？$R;" +
                "$R盜賊總管會對誰動心呢?$R;");
            Say(pc, 11000745, 190, "那樣阿！$R;" +
                "聽説最近總管和解答大叔很要好啊…$R;");
            Say(pc, 11000744, 190, "哇!$R總管人氣真旺啊!$R;");
        }
    }
}