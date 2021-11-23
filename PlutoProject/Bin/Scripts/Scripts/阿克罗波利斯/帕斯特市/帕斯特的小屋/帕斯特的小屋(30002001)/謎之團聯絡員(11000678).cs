using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30002001
{
    public class S11000678 : Event
    {
        public S11000678()
        {
            this.EventID = 11000678;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 10048001) >= 1)
            {
                Say(pc, 190, "谜之团的要塞在北边！$R;");
                return;
            }
            Say(pc, 11000665, 190, "那么，东西在那里…$R;");
            Say(pc, 11000678, 190, "好，拜托了$R;");
            Wait(pc, 2000);
            ShowEffect(pc, 11000678, 4501);
            ShowEffect(pc, 11000665, 4501);
            Wait(pc, 2000);
            Say(pc, 11000678, 190, "什么事？$R;");
            string a = string.Format(InputBox(pc, "请说出暗号", InputType.PetRename));
            Wait(pc, 1000);
            if (a == "謎語團和朋友" ||
                a == "谜之团和朋友" ||
                a == "フシギだんとおともだち")
            {
                GiveItem(pc, 10048001, 1);
                Say(pc, 190, "...嗖呜...$R;" +
                    "光荣啊，谜之团！$R;");
                return;
            }
            Say(pc, 190, "奇怪的家伙!$R;");
            Warp(pc, 10057000, 94, 114);
        }
    }
}