using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:嚮導機械人(11000372) X:150 Y:97
namespace SagaScript.M10023000
{
    public class S11000372 : Event
    {
        public S11000372()
        {
            this.EventID = 11000372;
        }

        public override void OnEvent(ActorPC pc)
        {
            int selection;

            Say(pc, 11000372, 131, "歡迎來到「阿高普路斯市」，$R;" +
                                   "我是「嚮導機械人」。$R;", "嚮導機械人");

            Say(pc, 11000372, 131, "這裡是「寶石商店」。$R;" +
                                   "$R賣著適合送給愛人的漂亮飾品。$R;" +
                                   "$P只要拿材料過來，$R;" +
                                   "店主就會馬上幫您製作。$R;" +
                                   "$R寶石加工、細工等，$R;" +
                                   "也會給您做的。$R;", "嚮導機械人");

            Say(pc, 11000372, 131, "要帶您去其他地方嗎?$R;", "嚮導機械人");

            selection = Select(pc, "請選擇想去的地方?", "", "行會宮殿", "白之聖堂", "黑之聖堂", "裁縫阿姨的家",  "放棄");

            while (selection != 5)
            {
                switch (selection)
                {
                    case 1:
                        Navigate(pc, 128, 94);

                        Say(pc, 11000372, 131, "跟著箭頭方向走，$R;" +
                                                "它會帶您去「行會宮殿」的。$R;", "嚮導機械人");
                        break;

                    case 2:
                        Navigate(pc, 162, 128);

                        Say(pc, 11000372, 131, "跟著箭頭方向走，$R;" +
                                               "它會帶您去「白之聖堂」的。$R;", "嚮導機械人");
                        break;

                    case 3:
                        Navigate(pc, 94, 128);

                        Say(pc, 11000372, 131, "跟著箭頭方向走，$R;" +
                                               "它會帶您去「黑之聖堂」的。$R;", "嚮導機械人");
                        break;

                    case 4:
                        Navigate(pc, 86, 99);

                        Say(pc, 11000372, 131, "跟著箭頭方向走，$R;" +
                                               "它會帶您去「裁縫阿姨的家」的。$R;", "嚮導機械人");
                        break;
                }

                selection = Select(pc, "請選擇想去的地方?", "", "行會宮殿", "白之聖堂", "黑之聖堂", "裁縫阿姨的家", "放棄");
            }

            Say(pc, 11000372, 131, "真可惜喔。$R;", "嚮導機械人");
        }
    }
}
