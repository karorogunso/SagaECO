using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20003000
{
    public class S11001351 : Event
    {
        public S11001351()
        {
            this.EventID = 11001351;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "ギヒヒヒヒヒ。$R;" +
                "オマエ、ツイテコイ……。$R;", "リヴィングデッド");
            if (pc.Level >= 85)
            {
                switch (Select(pc, "……。", "", "逃げる", "ついていく"))
                {
                    case 2:
                        Warp(pc, 20003000, 82, 23);
                        break;
                }
            }
        }
    }
}
