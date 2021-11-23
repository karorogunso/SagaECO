using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10065000
{
    public class S11000244 : Event
    {
        public S11000244()
        {
            this.EventID = 11000244;
        }

        public override void OnEvent(ActorPC pc)
        {
            NavigateCancel(pc);
            Say(pc, 131, "欢迎光临$R;" +
                "$R这里是女王陛下统治的$R;" +
                "魔法城『诺森市』喔$R;");
            switch (Select(pc, "要带路吗？", "", "宫殿", "魔法行会总部", "商人区域", "市民街道", "不委托"))
            {
                case 1:
                    Say(pc, 131, "女王陛下起居的宫殿就在$R;" +
                        "这海边长廊的最里面的$R;" +
                        "白色发亮漂亮的宫殿$R;" +
                        "$R跟着箭头方向走吧$R;");
                    Navigate(pc, 51, 20);
                    break;
                case 2:
                    Say(pc, 131, "魔法行会总部是一座$R;" +
                        "深蓝色和金色装饰的华丽建筑$R;" +
                        "入口有4处，全都都可以使用$R;" +
                        "$R跟着箭头方向走吧$R;");
                    Navigate(pc, 42, 77);
                    break;
                case 3:
                    Say(pc, 131, "商人区就是$R;" +
                        "前面很大的楼梯周围的街道$R;" +
                        "左右排列着武器商店、宝石商店等$R;" +
                        "很多商家。$R;" +
                        "$R应该不用箭头表示了吧？$R;");
                    break;
                case 4:
                    Say(pc, 131, "市民区除了『诺森原居民』$R;" +
                        "只要不是土生土长的$R;" +
                        "诺顿公民，是进不去的$R;");
                    break;
            }

            //EVT1100024405
            /*
            Say(pc, 131, "實實？$R;" +
                "$R巴列麗拿的泰迪$R;" +
                "好像叫實實？$R;" +
                "$P巴列麗拿是咖啡館的老闆$R;" +
                "給您標箭頭$R;" +
                "跟著箭頭方向去找吧$R;");
            */
            //GUIDE ON 37 133
            //EVENTEND

        }
    }
}