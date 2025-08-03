using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30165000
{
    public class S11000385 : Event
    {
        public S11000385()
        {
            this.EventID = 11000385;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "哎呀，今天客人真多呀！$R;");
            /*
            //EVT1100038507
            Say(pc, 131, "今天有何貴幹呢？$R;");
            switch (Select(pc, "怎麼辦呢？", "", "買東西", "什麼也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 111);
            Say(pc, 131, "歡迎隨時光顧$R;");
                    break;
                case 2:Say(pc, 131, "哎呀，太遺憾了$R;");
                    break;
            }
            //EVENTEND
            Say(pc, 131, "對了，$R;" +
                "把這個東西給您吧$R;");
            //EVT1100038509
            if (CheckInventory(pc, 60080900, 1))
            {
            //EVT1100038510
            GiveItem(pc, 60080900, 1);
            Say(pc, 131, "拿到了『教科書』$R;");
            Say(pc, 131, "對我已經沒有用了$R;");
            _4A42 = false;
            _0C30 = true;
            _4A40 = true;
            Say(pc, 131, "不知賢者首領身體怎麼樣$R;" +
                "$R只要我不在，他就會把房間弄髒$R;");
                return;
            }
            _4A42 = true;
            Say(pc, 131, "哎呀，東西太多了$R;");
            //EVENTEND
            //EVT1100038503
            Say(pc, 131, "對了，$R;" +
                "先等會兒$R;" +
                "$P最近對買賣感興趣了，$R;" +
                "不知怎麼才能學好，$R;" +
                "結果就想到具體實行吧$R;" +
                "$P有空過來一趟吧$R;");
            switch (Select(pc, "怎麼辦呢？", "", "買東西", "什麼也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 111);
            Say(pc, 131, "歡迎隨時光顧$R;");
                    break;
                case 2:Say(pc, 131, "哎呀，太遺憾了$R;");
                    break;
            }


            Say(pc, 131, "啊，稍等$R;");
            SkillPointBonus(pc, 1);
            Wait(pc, 2000);
            PlaySound(pc, 3087, false, 100, 50);
            ShowEffect(pc, 4131);
            Wait(pc, 2000);
            Say(pc, 131, "技能點數上升了1$R;");
            Say(pc, 131, "謝謝唷$R;");
            _0C30 = true;
            _4A41 = true;
            Say(pc, 131, "不知賢者首領身體怎麼樣$R;" +
                "$R只要我不在，他就會把房間弄髒$R;");
            //EVENTEND
            */
        }
    }
}