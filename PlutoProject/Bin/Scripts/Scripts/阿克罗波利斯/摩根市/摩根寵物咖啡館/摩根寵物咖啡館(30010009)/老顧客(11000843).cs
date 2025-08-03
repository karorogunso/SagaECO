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
            Say(pc, 190, "嗯…过一会儿，是我宠物的生日呢$R;" +
                "$R它叫晓特，这小子挺可爱的！$R;" +
                "$R今年给它做什么好呢？$R;" +
                "$P，虽然有一点困难$R;" +
                "偶尔想给他做一些$R;" +
                "$R……『美味的肉串烧』$R;" +
                "但摩戈这个地方粮食紧缺呀，$R;" +
                "找一些『高级肉』是很费劲的$R;" +
                "$P如果给晓特肉串烧$R他会高兴死吧？$R;");
        }
    }
}