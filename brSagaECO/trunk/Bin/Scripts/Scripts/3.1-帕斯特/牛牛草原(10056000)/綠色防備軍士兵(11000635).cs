using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10056000
{
    public class S11000635 : Event
    {
        public S11000635()
        {
            this.EventID = 11000635;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "唉！$R;" +
                "$R就是最近的事$R;" +
                "大量的『豬』走過來，就被困在這了$R;" +
                "$P為了趕走他們，把橋都給封鎖掉了$R;" +
                "$R如果寵物神仙爺爺没有來的話$R;" +
                "可能要花更長時間$R;");
            ShowEffect(pc, 11000635, 4516);
            Wait(pc, 2000);
            Say(pc, 131, "寵物神仙是誰？$R;" +
                "$P寵物神仙是馴服魔物$R;" +
                "把魔物變成寵物的人$R;" +
                "$R他就住在那個山坡上$R;" +
                "$P這附近的爬爬蟲凱莉娥$R;" +
                "全部都是那位神仙馴服的$R;");
            Say(pc, 131, "$R他要是喜歡您，或許還會送您一隻$R;" +
                "爬爬蟲凱莉娥$R;"//#↓消しちゃ駄目！
            );
        }
    }
}