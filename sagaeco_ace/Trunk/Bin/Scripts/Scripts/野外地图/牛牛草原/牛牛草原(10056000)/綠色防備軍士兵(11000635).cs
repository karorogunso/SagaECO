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
                "大量的『猪』走过来，就被困在这了$R;" +
                "$P为了赶走他们，把桥都给封锁掉了$R;" +
                "$R如果宠物神仙爷爷没有来的话$R;" +
                "可能要花更长时间$R;");
            ShowEffect(pc, 11000635, 4516);
            Wait(pc, 2000);
            Say(pc, 131, "宠物神仙是谁？$R;" +
                "$P宠物神仙是驯服魔物$R;" +
                "把魔物变成宠物的人$R;" +
                "$R他就住在那个山坡上$R;" +
                "$P这附近的搬运用爬爬虫$R;" +
                "全部都是那位神仙驯服的$R;");
            Say(pc, 131, "$R他要是喜欢您，或许还会送您一只$R;" +
                "搬运用爬爬虫$R;"//#↓消しちゃ駄目！
            );
        }
    }
}