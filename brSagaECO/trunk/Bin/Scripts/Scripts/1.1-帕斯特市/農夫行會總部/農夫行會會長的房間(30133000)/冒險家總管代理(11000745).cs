using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30133000
{
    public class S11000745 : Event
    {
        public S11000745()
        {
            this.EventID = 11000745;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000745, 190, "啊～吃飽了！真的很好吃！！$R;" +
                "$R農夫行會會長，真的很會做菜耶！$R;");
            Say(pc, 190, "不是！也不是那麽回事！$R;" +
                "要成為農夫，這些只是基本功喔$R;");
            Say(pc, 11000745, 190, "我真的很受感動啊！$R;" +
                "$R料理煮得棒的人$R;" +
                "真帥唷～$R;");
            Say(pc, 11000744, 190, "知道嗎？$R;" +
                "$R料理煮得好的男人$R;" +
                "很有人氣哦！$R;");
            ShowEffect(pc, 11000745, 4505);
            Say(pc, 11000745, 190, "把我作為情人替補吧!$R;");
            Say(pc, 11000744, 190, "不行！我先……啊！$R;");
            Wait(pc, 1000);
            ShowEffect(pc, 11000746, 4510);
            Wait(pc, 1000);
            Say(pc, 11000746, 190, "(哦呼…想離開這裡)$R;");
        }
    }
}