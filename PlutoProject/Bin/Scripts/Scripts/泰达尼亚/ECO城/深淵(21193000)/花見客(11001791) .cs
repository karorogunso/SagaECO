using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21193000
{
    public class S11001791 : Event
    {
        public S11001791()
        {
            this.EventID = 11001791;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "下手じゃないんだけど、$R;" +
            "上手くもないのよね…$R;" +
            "$P今日、ここに来てからずっと$R;" +
            "聞かされてるわけ。$R;" +
            "$P例え絶世の歌声だったとしても、$R;" +
            "これだけ聞かされてたら飽きるわよ。$R;", "花見客");
        }
    }
}