using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M50062000
{
    public class S10001645 : Event
    {
        public S10001645()
        {
            this.EventID = 10001645;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, 0, "……$R;" +
            "류석으로 도망가는것은$R위험한 느낌이 든다$R;", "");

            Say(pc, 0, 0, "원래의 맵으로 돌아가면$R;" +
            "이벤트를 처음부터$R다시 시작해야 합니다$R;" +
            "상관 없으십니까?$R;", "주의!");
            if (Select(pc, "어떻게 할까?", "", "돌아간다", "돌아 가지 않는다") == 1)
            {
                Warp(pc, 12020001, 51, 95);
            }

        }
    }
}


        
   


