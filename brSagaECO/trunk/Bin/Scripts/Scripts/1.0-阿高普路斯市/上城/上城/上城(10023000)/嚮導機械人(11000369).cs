using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:嚮導機械人(11000369) X:159 Y:130
namespace SagaScript.M10023000
{
    public class S11000369 : Event
    {
        public S11000369()
        {
            this.EventID = 11000369;
        }

        public override void OnEvent(ActorPC pc)
        {
            int selection;

            Say(pc, 11000369, 131, "歡迎來到「阿高普路斯市」，$R;" +
                                   "我是「嚮導機械人」。$R;", "嚮導機械人");

            Say(pc, 11000369, 131, "這裡是「白之聖堂」。$R;" +
                                   "$P這裡可以轉職為『祭司』。$R;" +
                                   "$R成為祭司的話，可以執行職業任務。$R;" +
                                   "$P聽說祭司是適合塔妮亞的職業呢!$R;", "嚮導機械人");

            Say(pc, 11000369, 131, "要帶您去其他地方嗎?$R;", "嚮導機械人");

            selection = Select(pc, "請選擇想去的地方?", "", "行會宮殿", "黑之聖堂", "裁縫阿姨的家", "寶石商店", "放棄");

            while (selection != 5)
            {
                switch (selection)
                {
                    case 1:
                        Navigate(pc, 128, 94);

                        Say(pc, 11000369, 131, "跟著箭頭方向走，$R;" +
                                                "它會帶您去「行會宮殿」的。$R;", "嚮導機械人");
                        break;

                    case 2:
                        Navigate(pc, 94, 128);

                        Say(pc, 11000369, 131, "跟著箭頭方向走，$R;" +
                                               "它會帶您去「黑之聖堂」的。$R;", "嚮導機械人");
                        break;

                    case 3:
                        Navigate(pc, 86, 99);

                        Say(pc, 11000369, 131, "跟著箭頭方向走，$R;" +
                                               "它會帶您去「裁縫阿姨的家」的。$R;", "嚮導機械人");
                        break;

                    case 4:
                        Navigate(pc, 153, 99);

                        Say(pc, 11000369, 131, "跟著箭頭方向走，$R;" +
                                               "它會帶您去「寶石商店」的。$R;", "嚮導機械人");
                        break;
                }

                selection = Select(pc, "請選擇想去的地方?", "", "行會宮殿", "黑之聖堂", "裁縫阿姨的家", "寶石商店", "放棄");
            }

            Say(pc, 11000369, 131, "真可惜喔。$R;", "嚮導機械人");
        }
    }
}