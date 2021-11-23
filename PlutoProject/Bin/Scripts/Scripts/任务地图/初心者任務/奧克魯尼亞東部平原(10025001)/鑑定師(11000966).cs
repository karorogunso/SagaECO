using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞東部平原(10025001) NPC基本信息:鑑定師(11000966) X:31 Y:121
namespace SagaScript.M10025001
{
    public class S11000966 : Event
    {
        public S11000966()
        {
            this.EventID = 11000966;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000966, 131, "我是鉴定师。$R;" +
                                   "$R正所谓老马识途，$R;" +
                                   "以我人生的经验，$R;" +
                                   "给未鉴定的道具来鉴定喔!$R;" +
                                   "$P未鉴定的道具，$R;" +
                                   "不先鉴定的话，是无法使用的。$R;" +
                                   "$P要鉴定的话，$R;" +
                                   "只有去学相关技能，$R;" +
                                   "或者叫我帮忙这两种方法。$R;" +
                                   "$P我会一直在平原中央附近喔!$R;" +
                                   "发现未鉴定道具的话，$R;" +
                                   "就拿过来给我鉴定吧!$R;" +
                                   "$P告诉您一个诀窍吧!$R;" +
                                   "$R按住「Shift」键，$R;" +
                                   "同时点击角色的周边。$R;" +
                                   "$P可以简单的改变角色的方向!$R;" +
                                   "$R即使在躺着或坐下的状态，$R;" +
                                   "也可以变更方向呢!$R;", "鉴定师");
        }
    }
}
