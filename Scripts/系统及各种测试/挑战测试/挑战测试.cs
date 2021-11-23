
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S50002009 : Event
    {
        public S50002009()
        {
            this.EventID = 50002009;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "欢迎进行挑战测试$R$R单人排名最佳: $R时间:$R$R4人排名最佳:$R时间:$R$R;", "");
            switch(Select(pc, "要做什么呢", "", "进行单人挑战(复活1次)","进行4人组队挑战(无法复活)","查看排行榜","兑换奖励","离开"))
            {
                case 1:
                    pc.TInt["挑战标记"] = 1;//1是个人挑战 2是组队队长 3是组队队员
                    pc.TInt["个人挑战地图"] = CreateMap1();
                    Warp(pc, (uint)pc.TInt["个人挑战地图"], 200, 200);
                    break;
                case 2:
                    Say(pc, 131,"1a");
                    ChangeBGM(pc, 1168, true, 100, 50);
                    pc.TInt["挑战标记"] = 2;
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
            }
        }
        int CreateMap1()
        {
            return CreateMapInstance(100011000, 90001999, 35, 15, true, 1, true);
        }
    }
}

