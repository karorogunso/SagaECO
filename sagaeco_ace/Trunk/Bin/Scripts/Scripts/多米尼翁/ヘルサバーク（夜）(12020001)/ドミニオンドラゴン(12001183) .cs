using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M12020001
{
    public class S12001183 : Event
    {
        public S12001183()
        {
            this.EventID = 12001183;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "인간 녀석이 나한테 무슨 용무냐……?$R;", "ドミニオンドラゴン");
            if (Select(pc, "무슨 일이냐……?", "", "힘을……어둠의 힘을 주십시오", "ドミニオンドラゴン") == 1)
            {
                Say(pc, 131, "어리석은……$R;" +
                "인간은 어디까지나 욕심이 많아$R;" +
                "$R힘을 가져서 어떻게 할건가?$R;" +
                "네가 힘을 얻는다고 이 세계는$R;" +
                "변하지 않는다$R;" +
                "$P나한테 힘을 얻으려한 자들이$R;" +
                "몇명인가 왔지만 오히려$R;" +
                "힘에 먹혀갔을 뿐이다……$R;" +
                "$P어리석은 인간이여$R;" +
                "그래도 내힘을 얻으려는가?$R;", "도미니언 드래곤");
                if (Select(pc, "어떻게 할까?", "", "각오는 하고 있다", "제어 불가능한 힘은 필요없다") == 1)
                {
                    Say(pc, 393, "욕심 많은 녀석$R;" +
                    "그러면 내가 몸소 느끼게 해주지$R;" +
                    "어서 덤비는게 좋아$R;" +
                    "$R너의 강함을 나한테 보여라$R;", "도미니언 드래곤");
                    Warp(pc, 50062000, 55, 96);
                    ShowEffect(pc, 10000, 4539);
                }
            }
        }
    }
}


        
   


