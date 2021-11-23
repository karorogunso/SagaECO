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
            Say(pc, 131, "歡迎光臨$R;" +
                "$R這裡是女王陛下統治的$R;" +
                "魔法城『諾頓市』喔$R;");
            switch (Select(pc, "要帶路嗎？", "", "宮殿", "魔法行會總部", "商人區域", "市民街道", "不委託"))
            {
                case 1:
                    Say(pc, 131, "女王陛下起居的宮殿就在$R;" +
                        "這海邊長廊的最裡面的$R;" +
                        "白色發亮漂亮的宮殿$R;" +
                        "$R跟著箭頭方向走吧$R;");
                    Navigate(pc, 51, 20);
                    break;
                case 2:
                    Say(pc, 131, "魔法行會總部是一座$R;" +
                        "深藍色和金色裝飾的華麗建築$R;" +
                        "入口有4處，全都都可以使用$R;" +
                        "$R跟著箭頭方向走吧$R;");
                    Navigate(pc, 42, 77);
                    break;
                case 3:
                    Say(pc, 131, "商人區就是$R;" +
                        "前面很大的樓梯周圍的街道$R;" +
                        "左右排列著武器商店、寶石商等$R;" +
                        "很多商家。$R;" +
                        "$R應該不用箭頭表示了吧？$R;");
                    break;
                case 4:
                    Say(pc, 131, "市民區除了『諾頓原居民』$R;" +
                        "只要不是土生土長的$R;" +
                        "諾頓公民，是進不去的$R;");
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