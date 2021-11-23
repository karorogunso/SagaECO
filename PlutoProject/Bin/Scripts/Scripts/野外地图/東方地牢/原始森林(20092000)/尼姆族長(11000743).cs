using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20092000
{
    public class S11000743 : Event
    {
        public S11000743()
        {
            this.EventID = 11000743;
        }

        public override void OnEvent(ActorPC pc)
        {
            /*
            if (_2b60 && !_2b61)
            {
                Call(EVT1100074304);
                return;
            }
            */
            if (CountItem(pc, 10034850) >= 1 && CountItem(pc, 10013002) >= 1 && CheckInventory(pc, 10013000, 1))
            {
                switch (Select(pc, "怎么办?", "             ", "给他“翡翠”和", "什么都不做"))
                {
                    case 1:
                        TakeItem(pc, 10034850, 1);
                        TakeItem(pc, 10013002, 1);
                        GiveItem(pc, 10013000, 1);
                        Say(pc, 131, "我的孩子…睁开眼睛吧$R;" +
                            "出去旅游的时间到了！$R;" +
                            "$R跟他一起去吧!$R;");
                        ShowEffect(pc, 5018);
                        Wait(pc, 6000);
                        Say(pc, 131, "这孩子是神风精灵$R;" +
                            "肯定对您有帮助的$R;" +
                            "$R再见!我亲爱的孩子…$R;");
                        PlaySound(pc, 2040, false, 100, 50);
                        Say(pc, 131, "收到“活动木偶小精灵”!$R;");
                        break;
                }
                return;
            }
            if (CountItem(pc, 10013002) >= 1)
            {
                Say(pc, 131, "呼……呼…$R;" +
                    "$R啊啊…现在总算可以喘一口气了$R;" +
                    "您有“翡翠”啊!$R;" +
                    "$P我是很久之前，成就这繁荣树林的$R;" +
                    "小精灵族族长！$R;" +
                    "$R请听听我的话。$R;");
                switch (Select(pc, "怎么办?", "", "听", "不听"))
                {
                    case 1:
                        Say(pc, 131, "您也看到了这个树林$R;" +
                            "因为毒雾的关系，渐渐枯萎了$R;" +
                            "$R过去的文明灭亡时$R;" +
                            "不知是谁散播了毒气$R;" +
                            "$P对小精灵来说树林和空气一样重要$R;" +
                            "出了树林是无法呼吸的$R;" +
                            "所以无法离开这里啊$R;" +
                            "$R这个树林的灭亡…$R;" +
                            "就是我们小精灵的灭亡时期$R;" +
                            "$P只有一个办法可以救我们！$R;" +
                            "$P那个方法就是您的“翡翠”$R;" +
                            "$R那是充满森林力量的石头$R;" +
                            "有了这个石头$R;" +
                            "在什么地方都可以呼吸$R;" +
                            "我们也就可以生存了$R;" +
                            "$P求您把我们装进那“翡翠”里$R;" +
                            "把我们从这个树林里救出去吧$R;" +
                            "$P同伴们为了避开毒气$R;" +
                            "变成蛹睡着了$R;" +
                            "$R求您找出小精灵们$R;" +
                            "拿着“翡翠”过来找我好吗…$R;");
                        break;
                }
                return;
            }
            Say(pc, 131, "咔…咔啊…$R;" +
                "树林里的毒气太重，喘不过气…$R;" +
                "$R求您给我“翡翠”…$R;");
            /*
            //EVT1100074304
            //SWITCH START
            //SWITCH END
            Say(pc, 131, "找我什麽事……？$R;");
            Say(pc, 0, 131, "啊！！找到了！那個就是精靈啊！$R;", "翡翠");
            //EVENTMAP_IN 25 1 168 31 3
            //SWITCH START
            //ME.WORK0 = -1 EVT1100074304a
            //SWITCH END
            //EVENTEND
            //EVT1100074304a
            Say(pc, 131, "找我什麽事……？$R;" +
                "$R有秘密要說的話，請等一下$R;" +
                "$R這裡人多且樹林太吵了$R;" +
                "現在走的話很危險$R;");
            //EVENTEND
            //EVT1100074305
            PlaySound(pc, 2041, false, 100, 50);
            Say(pc, 131, "找我什麽事……？$R;" +
                "$R有秘密的話，得解開憑依啊！$R;");
            //EVENTEND
            */
        }
    }
}