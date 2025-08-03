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

            Say(pc, 11000371, 131, "欢迎来到「阿克罗波利斯」，$R;" +
                                   "我是「向导机器人」。$R;", "向导机器人");

            Say(pc, 11000371, 131, "这裡是「裁缝阿姨的家」。$R;" +
                                   "$R专卖阿姨亲自制作的洋服。$R;" +
                                   "$P只要带材料过来，$R;" +
                                   "无论是制作洋服还是食物，$R;" +
                                   "阿姨都会给您做的。$R;", "向导机器人");

            Say(pc, 11000371, 131, "要带您去其他地方吗?$R;", "向导机器人");

            selection = Select(pc, "请选择想去的地方?", "", "行会宫殿", "白之圣堂", "黑之圣堂", "宝石商店", "放弃");

            while (selection != 5)
            {
                switch (selection)
                {
                    case 1:
                        Navigate(pc, 128, 94);

                        Say(pc, 11000371, 131, "跟着箭头方向走，$R;" +
                                                "它会带您去「行会宫殿」的。$R;", "向导机器人");
                        break;

                    case 2:
                        Navigate(pc, 162, 128);

                        Say(pc, 11000371, 131, "跟着箭头方向走，$R;" +
                                               "它会带您去「白之圣堂」的。$R;", "向导机器人");
                        break;

                    case 3:
                        Navigate(pc, 94, 128);

                        Say(pc, 11000371, 131, "跟着箭头方向走，$R;" +
                                               "它会带您去「黑之圣堂」的。$R;", "向导机器人");
                        break;

                    case 4:
                        Navigate(pc, 153, 99);

                        Say(pc, 11000371, 131, "跟着箭头方向走，$R;" +
                                               "它会带您去「宝石商店」的。$R;", "向导机器人");
                        break;
                }

                selection = Select(pc, "请选择想去的地方?", "", "行会宫殿", "白之圣堂", "黑之圣堂", "裁缝阿姨的家", "宝石商店", "放弃");
            }

            Say(pc, 11000371, 131, "真可惜喔。$R;", "向导机器人");
        }
    }
}
