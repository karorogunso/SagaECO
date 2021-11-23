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
                switch (Select(pc, "怎麽辦?", "             ", "給他『翡翠』和", "什麽都不做"))
                {
                    case 1:
                        TakeItem(pc, 10034850, 1);
                        TakeItem(pc, 10013002, 1);
                        GiveItem(pc, 10013000, 1);
                        Say(pc, 131, "我的孩子…睜開眼睛吧$R;" +
                            "出去旅遊的時間到了！$R;" +
                            "$R跟他一起去吧!$R;");
                        ShowEffect(pc, 5018);
                        Wait(pc, 6000);
                        Say(pc, 131, "這孩子是神風精靈$R;" +
                            "肯定對您有幫助的$R;" +
                            "$R再見!我親愛的孩子…$R;");
                        PlaySound(pc, 2040, false, 100, 50);
                        Say(pc, 131, "收到『活動木偶尼姆』!$R;");
                        break;
                }
                return;
            }
            if (CountItem(pc, 10013002) >= 1)
            {
                Say(pc, 131, "呼……呼…$R;" +
                    "$R啊啊…現在總算可以喘一口氣了$R;" +
                    "您有『翡翠』啊!$R;" +
                    "$P我是很久之前，成就這繁榮樹林的$R;" +
                    "尼姆族族長！$R;" +
                    "$R請聽聽我的話。$R;");
                switch (Select(pc, "怎麼辦?", "", "聽", "不聽"))
                {
                    case 1:
                        Say(pc, 131, "您也看到了這個樹林$R;" +
                            "因為毒霧的關係，漸漸枯萎了$R;" +
                            "$R以前過去的文明滅亡時$R;" +
                            "不知是誰散播了毒氣$R;" +
                            "$P對尼姆來説樹林和空氣一樣重要$R;" +
                            "出了樹林是無法呼吸的$R;" +
                            "所以無法離開這裡阿$R;" +
                            "$R這個樹林的滅亡…$R;" +
                            "就是我們尼姆的滅亡時期$R;" +
                            "$P只有一個辦法可以救我們！$R;" +
                            "$P那個方法就是您的『翡翠』$R;" +
                            "$R那是充滿森林力量的石頭$R;" +
                            "有了這個石頭$R;" +
                            "在什麽地方都可以呼吸$R;" +
                            "我們也就可以生存了$R;" +
                            "$P求您把我們裝進那『翡翠』裡$R;" +
                            "把我們從這個樹林裡救出去吧$R;" +
                            "$P同伴尼姆們為了避開毒氣$R;" +
                            "變成蛹睡著了$R;" +
                            "$R求您找出尼姆們$R;" +
                            "拿著『翡翠』過來找我好嗎…$R;");
                        break;
                }
                return;
            }
            Say(pc, 131, "咔…咔啊…$R;" +
                "樹林裡的毒氣太重，喘不過氣…$R;" +
                "$R求您給我『翡翠』…$R;");
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