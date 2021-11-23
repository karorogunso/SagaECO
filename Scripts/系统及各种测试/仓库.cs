
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
    public class S50003002 : Event
    {
        public S50003002()
        {
            this.EventID = 50003002;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "你好我是宝箱❤，不对我是仓库QAQ", "", "打开仓库", "设置背包上限", "设置达到上限后获取物品的仓库"))
            {
                case 1:
                    NPCMotion(pc, 50003002, 622);
                    OpenWareHouse(pc, WarehousePlace.Acropolis);
                    break;
                case 2:
                    Say(pc, 0, "设置上限后，$R获得物品时背包数量超过$R该数值将会送入指定仓库");
                    int count = int.Parse(InputBox(pc, "请输入一个20-300的数", InputType.Bank));
                    if (count < 20 || count > 300)
                    {
                        Say(pc, 0, "请输入一个20-300的数");
                        return;
                    }
                    else
                        pc.CInt["背包上限"] = count;
                    break;
                case 3:
                    switch(Select(pc,"请选择背包达到上限后，将物品送入哪个仓库","","第一页","第二页","第三页","第四页"))
                    {
                        case 1:
                            pc.CInt["背包满后仓库"] = 1;
                            break;
                        case 2:
                            pc.CInt["背包满后仓库"] = 2;
                            break;
                        case 3:
                            pc.CInt["背包满后仓库"] = 3;
                            break;
                        case 4:
                            pc.CInt["背包满后仓库"] = 4;
                            break;
                    }
                    break;
            }

        }
    }
}

