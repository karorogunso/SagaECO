using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:嚮導機械人(11000371) X:89 Y:97
namespace SagaScript.M10023000
{
    public class S11000371 : Event
    {
        public S11000371()
        {
            this.EventID = 11000371;
        }

        public override void OnEvent(ActorPC pc)
        {
            int selection;

            Say(pc, 11000371, 131, "歡迎來到「阿高普路斯市」，$R;" +
                                   "我是「嚮導機械人」。$R;", "嚮導機械人");

            Say(pc, 11000371, 131, "這裡是「裁縫阿姨的家」。$R;" +
                                   "$R專賣阿姨親自製作的洋服。$R;" +
                                   "$P只要帶材料過來，$R;" +
                                   "無論是製作洋服還是食物，$R;" +
                                   "阿姨都會給您做的。$R;", "嚮導機械人");

            Say(pc, 11000371, 131, "要帶您去其他地方嗎?$R;", "嚮導機械人");

            selection = Select(pc, "請選擇想去的地方?", "", "行會宮殿", "白之聖堂", "黑之聖堂", "寶石商店", "放棄");

            while (selection != 5)
            {
                switch (selection)
                {
                    case 1:
                        Navigate(pc, 128, 94);

                        Say(pc, 11000371, 131, "跟著箭頭方向走，$R;" +
                                                "它會帶您去「行會宮殿」的。$R;", "嚮導機械人");
                        break;

                    case 2:
                        Navigate(pc, 162, 128);

                        Say(pc, 11000371, 131, "跟著箭頭方向走，$R;" +
                                               "它會帶您去「白之聖堂」的。$R;", "嚮導機械人");
                        break;

                    case 3:
                        Navigate(pc, 94, 128);

                        Say(pc, 11000371, 131, "跟著箭頭方向走，$R;" +
                                               "它會帶您去「黑之聖堂」的。$R;", "嚮導機械人");
                        break;

                    case 4:
                        Navigate(pc, 153, 99);

                        Say(pc, 11000371, 131, "跟著箭頭方向走，$R;" +
                                               "它會帶您去「寶石商店」的。$R;", "嚮導機械人");
                        break;
                }

                selection = Select(pc, "請選擇想去的地方?", "", "行會宮殿", "白之聖堂", "黑之聖堂", "裁縫阿姨的家", "寶石商店", "放棄");
            }

            Say(pc, 11000371, 131, "真可惜喔。$R;", "嚮導機械人");
        }
    }
}
