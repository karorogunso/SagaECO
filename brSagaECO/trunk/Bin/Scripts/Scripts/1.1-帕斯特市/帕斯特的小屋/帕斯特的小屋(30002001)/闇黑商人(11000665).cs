using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30002001
{
    public class S11000665 : Event
    {
        public S11000665()
        {
            this.EventID = 11000665;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 10048001) >= 1)
            {
                Say(pc, 190, "謎語團的要塞在北邊！$R;");
                return;
            }
            Say(pc, 11000665, 190, "那麽，東西在那裡…$R;");
            Say(pc, 11000678, 190, "好，拜託了$R;");
            Wait(pc, 2000);
            ShowEffect(pc, 11000678, 4501);
            ShowEffect(pc, 11000665, 4501);
            Wait(pc, 2000);
            Say(pc, 11000678, 190, "什麽事？$R;");
            string a = string.Format(InputBox(pc, "請輸入密碼", InputType.PetRename));
            Wait(pc, 1000);
            if (a == "謎語團和朋友" ||
                a == "フシギだんとおともだち")
            {
                GiveItem(pc, 10048001, 1);
                Say(pc, 190, "...嗖嗚...$R;" +
                    "光榮阿，謎語團！$R;");
                return;
            }
            Say(pc, 190, "奇怪的傢伙!$R;");
            Warp(pc, 10057000, 94, 114);
        }
    }
}