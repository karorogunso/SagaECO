using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript
{
    public abstract class 垃圾桶 : Event
    {
        public 垃圾桶()
        {

        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, 65535, "咕咕咕，你你你，你想干什么？！$R$R$CR（仅在此处扔垃圾可以获得CP点数）$CD$R$CR（QAQ为什么你们都不把垃圾扔进来呢？）$CD$R$CR（CP以后会有用的吖）$CD;", "垃圾桶");
            pc.TInt["垃圾箱记录"] = 1;
            NPCTrade(pc);
            pc.TInt["垃圾箱记录"] = 0;
        }
    }
}
