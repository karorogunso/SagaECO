using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30060009
{
    public class S11001086 : Event
    {
        public S11001086()
        {
            this.EventID = 11001086;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_03> Neko_03_amask = pc.AMask["Neko_03"];
            BitMask<Neko_03> Neko_03_cmask = pc.CMask["Neko_03"];

            if (Neko_03_amask.Test(Neko_03.堇子任務開始) &&
                !Neko_03_amask.Test(Neko_03.堇子任務完成) &&
                Neko_03_cmask.Test(Neko_03.與商人之家的瑪莎對話))
            {
                Say(pc, 11001086, 131, "真的快嚇死我了$R;" +
                    "$R瑪莎帶著那孩子$R來到了這裡$R;" +
                    "$R甚麼都沒解釋$R…就說要藏著這孩子呀$R;");
                return;
            }
            Say(pc, 11001086, 131, "…呀！歡迎光臨…$R;");
            //EVENTEND
            //EVT1100108600
            //EVENTEND
        }
    }
}