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

            Say(pc, 11000372, 131, "欢迎来到「阿克罗波利斯」，$R;" +
                                   "我是「向导机器人」。$R;", "向导机器人");

            Say(pc, 11000372, 131, "这里是「宝石商店」。$R;" +
                                   "$R卖着适合送给爱人的漂亮饰品。$R;" +
                                   "$P只要拿材料过来，$R;" +
                                   "店主就会马上帮您制作。$R;" +
                                   "$R宝石加工、细工等，$R;" +
                                   "也会给您做的。$R;", "向导机器人");

            Say(pc, 11000372, 131, "要带您去其他地方吗?$R;", "向导机器人");

            selection = Select(pc, "请选择想去的地方?", "", "行会宫殿", "白之圣堂", "黑之圣堂", "裁缝阿姨的家",  "放弃");

            while (selection != 5)
            {
                switch (selection)
                {
                    case 1:
                        Navigate(pc, 128, 94);

                        Say(pc, 11000372, 131, "跟著箭头方向走，$R;" +
                                                "它会带您去「行会宫殿」的。$R;", "向导机器人");
                        break;

                    case 2:
                        Navigate(pc, 162, 128);

                        Say(pc, 11000372, 131, "跟著箭头方向走，$R;" +
                                               "它会带您去「白之圣堂」的。$R;", "向导机器人");
                        break;

                    case 3:
                        Navigate(pc, 94, 128);

                        Say(pc, 11000372, 131, "跟著箭头方向走，$R;" +
                                               "它会带您去「黑之圣堂」的。$R;", "向导机器人");
                        break;

                    case 4:
                        Navigate(pc, 86, 99);

                        Say(pc, 11000372, 131, "跟著箭头方向走，$R;" +
                                               "它会带您去「裁缝阿姨的家」的。$R;", "向导机器人");
                        break;
                }

                selection = Select(pc, "请选择想去的地方?", "", "行会宫殿", "白之圣堂", "黑之圣堂", "裁缝阿姨的家", "放弃");
            }

            Say(pc, 11000372, 131, "真可惜喔。$R;", "向导机器人");
        }
    }
}
