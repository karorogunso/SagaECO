using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10069001
{
    public class S13000285 : Event
    {
        public S13000285()
        {
            this.EventID = 13000285;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "ソーウェン祭の期間中$R;" +
            "儀式に携わる委員の子達は$R;" +
            "儀式用の衣装を着るの。$R;" +
            "$P制服以外を着ている子は$R;" +
            "みんな何かしら儀式に$R;" +
            "携わってるのよ。$R;" +
            "$P私も去年は儀式に参加したけど$R;" +
            "ちょっと怖かったかな。$R;", "三年生女子");
        }
    }
}