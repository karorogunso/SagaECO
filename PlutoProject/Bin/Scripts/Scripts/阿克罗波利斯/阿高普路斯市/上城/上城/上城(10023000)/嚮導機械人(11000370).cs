using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:嚮導機械人(11000370) X:96 Y:130
namespace SagaScript.M10023000
{
    public class S11000370 : Event
    {
        public S11000370()
        {
            this.EventID = 11000370;
        }

        public override void OnEvent(ActorPC pc)
        {
            int selection;

            Say(pc, 11000370, 131, "欢迎来到「阿克罗波利斯」，$R;" +
                                   "我是「向导机器人」。$R;", "向导机器人");

            Say(pc, 11000370, 131, "这里是「黑之圣堂」。$R;" +
                                   "$在这里$P可以转职成为『暗术使』。$R;" +
                                   "$R成为暗术使的话，可以执行职业任务$R;" +
                                   "$P听说暗术使是适合多米尼翁的职业。$R;", "向导机器人");

            Say(pc, 11000370, 131, "要带您去其他地方吗?$R;", "向导机器人");

            selection = Select(pc, "请选择想去的地方?", "", "行会宫殿", "白之圣堂", "裁缝阿姨的家", "宝石商店", "放弃");

            while (selection != 5)
            {
                switch (selection)
                {
                    case 1:
                        Navigate(pc, 128, 94);

                        Say(pc, 11000370, 131, "跟着箭头方向走，$R;" +
                                                "它会带您去「行会宫殿」的。$R;", "向导机器人");
                        break;

                    case 2:
                        Navigate(pc, 162, 128);

                        Say(pc, 11000370, 131, "跟着箭头方向走，$R;" +
                                               "它会带您去「白之圣堂」的。$R;", "向导机器人");
                        break;

                    case 3:
                        Navigate(pc, 86, 99);

                        Say(pc, 11000370, 131, "跟着箭头方向走，$R;" +
                                               "它会带您去「裁缝阿姨的家」的。$R;", "向导机器人");
                        break;

                    case 4:
                        Navigate(pc, 153, 99);

                        Say(pc, 11000370, 131, "跟着箭头方向走，$R;" +
                                               "它会带您去「宝石商店」的。$R;", "向导机器人");
                        break;
                }

                selection = Select(pc, "请选择想去的地方?", "", "行会宫殿", "白之圣堂", "裁缝阿姨的家", "宝石商店", "放弃");
            }

            Say(pc, 11000370, 131, "真可惜喔。$R;", "向导机器人");
        }
    }
}
