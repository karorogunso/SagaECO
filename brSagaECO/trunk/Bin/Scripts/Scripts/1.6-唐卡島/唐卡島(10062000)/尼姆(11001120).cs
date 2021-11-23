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
                    "$R我……我們的主人……$R;" +
                    "一…一定會成為世界最棒的工匠…$R;");
                Wait(pc, 0);
                ShowEffect(pc, 11001120, 4539);
                Wait(pc, 1000);
                Say(pc, 131, "嗯？$R;" +
                    "好像有人類，$R;" +
                    "$R是我的錯覺嗎？$R;");
                return;
            }
            Say(pc, 131, "……$R;");
            Say(pc, 11001069, 131, "這孩子可能怕人類，所以不跟人類說話。$R;" +
                "$R請不要生氣唷$R;");
            Say(pc, 131, "什麼？怎麼胡亂說話呢？$R;" +
                "$R我這個身體沒有理由怕人類呀$R;" +
                "$P像您一樣的東西，我看到都煩！$R;" +
                "滾！！$R;");
            Say(pc, 11001069, 131, "知道了，是，夫人$R;");
        }
    }
}