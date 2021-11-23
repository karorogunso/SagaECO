using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10069001
{
    public class S13000284 : Event
    {
        public S13000284()
        {
            this.EventID = 13000284;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 13000283, 131, "ノーザン学校は$R;" +
            "古城を改装して作られたんだ。$R;", "二年生男子");

            Say(pc, 131, "へ〜。$R;" +
            "先輩、物知りですね。$R;" +
            "$P……ところで$R;" +
            "誰の城だったんですか？$R;", "一年生男子");

            Say(pc, 13000283, 131, "それは俺も知らん！$R;", "二年生男子");
            ShowEffect(pc, 13000284, 4506);

            Say(pc, 111, "……。$R;", "一年生男子");

            Say(pc, 13000283, 111, "……。$R;", "二年生男子");

            Say(pc, 131, "先輩……その……。$R;" +
            "堂々と開き直るのは$R;" +
            "どうかと思います。$R;", "一年生男子");
        }
    }
}