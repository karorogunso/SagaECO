using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M10046000
{
    public class S11000101 : Event
    {
        public S11000101()
        {
            this.EventID = 11000101;
            this.questTransportSource = "在等你了!$R;" +
                                        "来，这个就拜托了!$R;";
            this.transport = "传送地点在『任务窗』确认吧$R;";
            this.questTransportCompleteSrc = "这么快就将物品转交给对方了?$R;" +
                                             "真的谢谢$R;" +
                                             "$R请去任务服务台领取报酬吧！$R;";
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<HZFlags> mask = new BitMask<HZFlags>(pc.CMask["HZ"]);
            if (!mask.Test(HZFlags.瑪爾塔第一次對話))
            {
                Say(pc, 131, "有去过艾恩萨乌斯吗?$R;" +
                    "$R那是这个阿姨的故乡$R是一个非常有趣的村子$R;" +
                    "$P无论你多么的忙$R都要抽空去看一次比较好$R;");
                mask.SetValue(HZFlags.瑪爾塔第一次對話, true);
                return;
            }
            Say(pc, 131, "不热吗?$R;" +
                "当然热了!!热的快要死了~!!$R;");
        }
    }
}

