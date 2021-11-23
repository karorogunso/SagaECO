
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
    public class S910000115 : Event
    {
        public S910000115()
        {
            this.EventID = 910000115;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 910000115) >= 1)//检查玩家身上红包道具，数量大于等于1
            {
                TakeItem(pc, 910000115, 1);//拿走玩家身上1个红包
                int ran = SagaLib.Global.Random.Next(1, 100);//随机生成一个1-100之间的数字,并保存在ran
                if (ran < 10)//如果随机数少于10
                {
                    pc.Gold += 888;//玩家获得888
                }
                else if (ran < 20)//否则如果随机数小于20
                {
                    pc.Gold += 1888;//玩家获得1888
                }
                else if (ran < 45)//如果随机数小于45
                {
                    pc.Gold += 2017;//玩家获得2017
                }
                else if (ran < 60)//如果随机数小于60
                {
                    pc.Gold += 2888;//玩家获得2888
                }
                else if (ran < 75)//如果随机数小于75
                {
                    pc.Gold += 5888;//玩家获得5888
                }
                else if (ran < 90)//如果随机数小于90
                {
                    pc.Gold += 6666;//玩家获得6666
                }
                else if (ran <= 100)//如果随机数小于等于100
                {
                    pc.Gold += 8888;//玩家获得8888
                }

            }
        }
    }
}

