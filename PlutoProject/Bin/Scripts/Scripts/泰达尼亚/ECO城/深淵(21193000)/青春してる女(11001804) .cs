using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21193000
{
    public class S11001804 : Event
    {
        public S11001804()
        {
            this.EventID = 11001804;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "秋は好きだな。$R;" +
            "静かな活発さがあって、$R;" +
            "何だかウキウキしちゃうの。$R;" +
            "$P…いつまでも、$R;" +
            "このドキドキが続けば良いのにな。$R;", "青春してる女");
        }
    }
}