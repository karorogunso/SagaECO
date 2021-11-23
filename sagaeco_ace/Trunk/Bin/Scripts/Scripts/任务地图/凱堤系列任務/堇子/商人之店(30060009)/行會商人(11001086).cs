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
                Say(pc, 11001086, 131, "真的快吓死我了$R;" +
                    "$R玛莎带著那孩子$R来到了这里$R;" +
                    "$R什么都没解释$R…就说要藏着这孩子呀$R;");
                return;
            }
            Say(pc, 11001086, 131, "…呀！欢迎光临…$R;");
            //EVENTEND
            //EVT1100108600
            //EVENTEND
        }
    }
}