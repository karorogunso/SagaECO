using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21003000
{
    public class S11001493 : Event
    {
        public S11001493()
        {
            this.EventID = 11001493;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 133, "用雙腳走路是什麽感覺呢ー？$R;", "美人魚");

        }


    }
}


