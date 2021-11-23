
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using WeeklyExploration;
using System.Globalization;
namespace SagaScript.M30210000
{
    public class S80000800 : Event
    {
        public S80000800()
        {
            this.EventID = 80000800;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "不来买点烟花吗？嗯？", "行脚商人");
            switch (Select(pc, "请选择购买种类", "", "基础装备", "买烟花", "买特效物品", "离开"))
            {
                case 1:
                    OpenShopByList(pc, 10000, SagaDB.Npc.ShopType.None, 10001911, 60030000, 50090100, 60050100, 60050800, 60060500, 60071100, 60080900);
                    break;
                case 2:
                   // OpenShopByList(pc, 10000, SagaDB.Npc.ShopType.None, 10048500, 10048550, 10048551, 10048552, 10048554, 10048560, 10048562, 10048565, 10048567);
                    break;
                case 3:
                   // OpenShopByList(pc, 10000, SagaDB.Npc.ShopType.None, 10048200,10051200, 10052500, 10055200, 10055300, 10057500, 20050052);
                    break;
            }
        }
    }
}