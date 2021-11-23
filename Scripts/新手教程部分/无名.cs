
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
    public class S60000010 : Event
    {
        public S60000010()
        {
            this.EventID = 60000010;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.CInt["无名的初次对话"] == 0)
            {
                Say(pc, 60000010, 0, "嗯……？", "无名");
                Say(pc, 60000010, 0, "少女上下打量着你", "");
                Say(pc, 60000010, 0, "你是谁？我似乎没见过你啊？", "无名");
                Say(pc, 60000010, 0, "你连这里是哪你都不知道？$R那你怎么过来的……？", "无名");
                Say(pc, 60000010, 0, "……啊，$R说起来有个人曾经和我说过，$R如果在岛上见到一脸懵逼的人就让我带去找她！", "");
                Say(pc, 60000010, 0, "那么，跟我来吧？$R反正我也闲着，就带你过去好了。", "");
                Warp(pc, 30010007, 3, 5);
                pc.CInt["无名的初次对话"] = 1;
                return;
            }
            Say(pc, 60000010, 0, "今天的天气真不错啊……", "无名");
        }
    }
}