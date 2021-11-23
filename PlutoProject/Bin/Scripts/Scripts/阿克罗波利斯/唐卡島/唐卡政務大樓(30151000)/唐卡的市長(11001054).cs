using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30151000
{
    public class S11001054 : Event
    {
        public S11001054()
        {
            this.EventID = 11001054;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<FGarden> fgarden = pc.AMask["FGarden"];
            Say(pc, 131, "欢迎，$R;" +
                "冒险者，欢迎光临$R;" +
                "$R今天有何贵干？$R;");
            switch (Select(pc, "给我介绍介绍吧", "", "清洁计划", "想注册飞空庭", "街灯的灯坏了", "警备机器人好烦", "什么也不做"))
            {
                case 1:
                    //GOTO EVT1100105408;
                    break;
                case 2:

                    if (!fgarden.Test(FGarden.唐卡注册飞空庭))
                    {
                        if (fgarden.Test(FGarden.得到飛空庭鑰匙)) 
                        {
                            Say(pc, 131, "注册需要50000金币哦$R;");
                            switch (Select(pc, "怎么办呢?", "", "付50000金币", "放弃"))
                            {
                                case 1:
                                    if (pc.Gold > 49999)
                                    {
                                        if (CheckInventory(pc, 31110709, 1))
                                        {
                                            fgarden.SetValue(FGarden.唐卡注册飞空庭, true);
                                            GiveItem(pc, 31110709, 1);
                                            pc.Gold -= 50000;
                                            PlaySound(pc, 2060, false, 100, 50);
                                            Say(pc, 131, "付了50000金币！$R;");
                                            Say(pc, 131, "名字…是…$R;" +
                                                pc.Name + "是吧?$R;" +
                                                "$P注册结束了$R;" +
                                                "现在您的飞空庭以$R;" +
                                                "唐卡这里为根据地喔$R;" +
                                                "$R这是给您$R;" +
                                                "代替证明书的唐卡国旗。$R;" +
                                                "$R请收下$R;" +
                                                "$P这里定为根据地，可以自由利用$R;" +
                                                "飞空庭大工厂的各种服务哦$R;" +
                                                "$R那么拜托了。$R;");
                                            return;
                                        }
                                        Say(pc, 131, "你怎么会有我们的国旗啊?$R;");
                                        return;
                                    }
                                    Say(pc, 131, "嗯~钱不够呀。$R;");
                                    break;
                                case 2:
                                    break;
                            }
                            return;
                        }
                        Say(pc, 131, "嗯~看样子您好像没有飞空庭呀$R;");
                        return;
                    }
                    Say(pc, 131, "嗯，已经注册了$R;");
                    break;
                case 3:
                    Say(pc, 131, "谢谢您告诉我$R;" +
                        "以后会修理的。$R;");
                    break;
                case 4:
                    Say(pc, 131, "哈哈哈！$R;" +
                        "听起来真烦！$R;" +
                        "$R我也天天被质问业务上的事情$R;" +
                        "不会害您的，好好对它吧。$R;");
                    break;
                case 5:
                    break;
            }
        }
    }
}