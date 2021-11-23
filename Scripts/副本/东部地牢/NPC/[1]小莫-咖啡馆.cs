
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
    public partial class S60000130 : Event
    {
        void 东牢进入任务(ActorPC pc)
        {
            ChangeMessageBox(pc);
                Say(pc, 0, "嗯……？", "小莫");
                Say(pc, 0, "那个…请问，你是冒险者吗？", "小莫");
                switch (Select(pc, " ", "", "是的，你是…？", "不，我只是来打个酱油的"))
                {
                    case 1:
                        Say(pc, 0, "我……？我是这座岛上的医生。实际上，我有件事情想要拜托你们。", "小莫");
                        Say(pc, 0, "是的……有些紧急，可以请你听我说吗？", "小莫");
                        Say(pc, 0, "就在这几天，岛上的一些居民忽然开始原因不明地发起了高烧。$R$R虽然在医生的照料下暂时控制住了病情，但是这么多天过去却也没有一点好转的迹象。", "小莫");
                        Say(pc, 0, "之前从来没有出现过这种情况，并且所有的患者都有一个共同的特点，就是在发病之前曾去过东国。$R$R考虑到东国的现状，恐怕是在那里被什么东西所感染了吧。", "小莫");
                        Say(pc, 0, "以我们现在的能力，对这样的状况已经完全束手无策了。仅仅只能确保病人的状况不继续恶化下去，但是一直这样下去是不行的。", "小莫");
                        Say(pc, 0, "据我所知，与东之国紧密相邻的东方地牢深处生存着魔狼一族。$R$R东国的瘟疫爆发后，周边所有的生物几乎无一幸免，但是魔狼一族却安然无恙。", "小莫");
                        Say(pc, 0, "在整个东国周边范围内，只有它们成功地避开了瘟疫的影响，因此拯救病人们的关键钥匙大概就在它们的手上。", "小莫");
                        Say(pc, 0, "虽然是个不情之请，但是……$R$R冒险者，可以拜托你们同我一起，进入东方地牢的最深处，找到魔狼一族，并想办法得到对抗瘟疫的方法吗？", "小莫");
                        pc.CInt["东牢进入任务"] = 1;
                        return;
                    case 2:
                   
                        Say(pc, 0, "这……这样啊……抱歉。", "小莫");
                        return;
                }
        }




    }
}