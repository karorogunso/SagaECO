using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30010009
{
    public class S11000843 : Event
    {
        public S11000843()
        {
            this.EventID = 11000843;

        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 190, "嗯…過一會兒，是我寵物的生日唷$R;" +
                "$R牠叫曉特，這小子挺可愛的！$R;" +
                "$R今年給牠做什麼好呢？$R;" +
                "$P，雖然有一點困難$R;" +
                "偶爾想給他做一些$R;" +
                "$R……『美味的肉串燒』$R;" +
                "但摩根這個地方糧食緊缺呀，$R;" +
                "找一些『高級肉』是很費勁的$R;" +
                "$P如果給曉特肉串燒$R他會高興死吧？$R;");
        }
    }
}