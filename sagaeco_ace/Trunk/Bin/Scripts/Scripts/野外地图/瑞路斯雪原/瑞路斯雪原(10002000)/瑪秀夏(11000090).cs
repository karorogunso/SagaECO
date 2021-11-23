using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10002000
{
    public class S11000090 : Event
    {
        public S11000090()
        {
            this.EventID = 11000090;
            this.questTransportSource = "小鬼，你要帮我转交吗?$R;" +
                                        "怎么感觉到有些不安啊…$R;" +
                                        "$R没办法…只好将就一下了!拜托你啦$R;";
            this.transport = "小鬼，那就拜託你啦$R;";
            this.questTransportDest = "您带了东西给我?$R;" +
                                      "年纪轻轻，不过小姐可真是厉害$R谢谢您了喔$R;";
            this.questTransportCompleteSrc = "已经把东西交给对方了?$R;" +
                                             "小鬼动作挺快的嘛，谢谢啦$R;" +
                                             "$R请去任务服务台领取报酬吧！$R;";
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<RLSXYFlags> mask = new BitMask<RLSXYFlags>(pc.CMask["RLSXY"]);
            if (!mask.Test(RLSXYFlags.瑪秀夏第一次對話))
            {
                Say(pc, 131, "这里就是『冰洞』装备都准备好了?$R;" +
                    "如果小看这里的话，很容易发生意外$R;");
                mask.SetValue(RLSXYFlags.瑪秀夏第一次對話, true);
                return;
            }
            Say(pc, 131, "不久前因为从地牢里传出了惨叫声$R;" +
                "我赶紧进去看了一下，结果……$R;" +
                "$P故事暂时就到这里停止!$R想要继续往下听的话，请付钱！$R;" +
                "想怎么做呢?$R;");
            mask.SetValue(RLSXYFlags.瑪秀夏第一次對話, false);
            switch (Select(pc, "要支付5000金币吗?", "", "支付", "不支付"))
            {
                case 1:

                    if (pc.Gold > 4999)
                    {
                        Say(pc, 131, "是啊，好好考虑了吧$R;" +
                            "我就先把钱收下吧$R;");
                        pc.Gold -= 5000;
                        PlaySound(pc, 2030, false, 100, 50);
                        Say(pc, 131, "给了他5000金币$R;");
                        Say(pc, 131, "嘿嘿嘿…贪财贪财，那我就继续说吧$R;" +
                            "当时我就赶紧到里面看看情况…$R;" +
                            "$P那小子就在那里!$R;" +
                            "$R外貌看起来虽然可爱，可是超强啊!$R;" +
                            "真是太可爱了，都到了讨厌的程度$R;" +
                            "$P他的名字?我不知道阿…$R;" +
                            "要不您亲自去确认吧?$R;" +
                            "我在地下2楼见过他$R;");
                        return;
                    }
                    PlaySound(pc, 2041, false, 100, 50);
                    Say(pc, 131, "钱不够啊…真是可惜啊$R;" +
                        "这样的话，那我就不能讲给您听啰$R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}
