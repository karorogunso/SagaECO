using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S11000680 : Event
    {
        public S11000680()
        {
            this.EventID = 11000680;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Level < 45)
            {
                Say(pc, 131, "從這裡開始就是東方地牢$R;" +
                    "被毒菌污染的樹林$R;" +
                    "危險的魔物到處亂竄$R;" +
                    "$R您的等級應該没有問題$R;" +
                    "最好繞路走$R;");
                return;
            }
            Say(pc, 131, "從這裡開始就是東方地牢$R;" +
                "被毒菌污染的樹林$R;" +
                "危險的魔物到處亂竄$R;" +
                "$R您的等級應該没有問題$R;" +
                "但是要小心！$R;");
        }
    }
}