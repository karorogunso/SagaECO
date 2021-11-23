using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M20070050
{
    public class S11003463 : Event
    {
        public S11003463()
        {
            this.EventID = 11003463;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "稍微在繁忙的每天喘息一下喔。$R;" +
            "$R雖然很不好意思、但可不可以讓我獨自一人？$R;" +
            "只有在這個瞬間、不想回到現實。$R;", "放鬆的女子");

            //
            /*
            Say(pc, 0, "忙しい毎日のちょっとした息抜きよ。$R;" +
            "$R悪いけど、一人にしてくれる？$R;" +
            "この瞬間だけは、現実に帰りたく無いの。$R;", "くつろぐ女");
            */
        }
    }


}

