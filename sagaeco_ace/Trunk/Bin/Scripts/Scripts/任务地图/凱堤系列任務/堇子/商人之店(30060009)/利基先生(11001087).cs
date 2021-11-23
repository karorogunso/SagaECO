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
    public class S11001087 : Event
    {
        public S11001087()
        {
            this.EventID = 11001087;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_03> Neko_03_amask = pc.AMask["Neko_03"];
            BitMask<Neko_03> Neko_03_cmask = pc.CMask["Neko_03"];

            if (Neko_03_amask.Test(Neko_03.堇子任務開始) &&
                !Neko_03_amask.Test(Neko_03.堇子任務完成) &&
                Neko_03_cmask.Test(Neko_03.與商人之家的瑪莎對話))
            {
                Say(pc, 11001087, 131, "那孩子希望以后能成为冒险者$R在阿克罗尼亚自由自在的冒险喔$R;" +
                    "$R常常叫我讲多些故事给他听！$R真的很有鸿图大志吧?$R;" +
                    "$R我一直在想，怎样才能够帮他呀$R;");
                return;
            }
            Say(pc, 11001087, 131, "…嗯…所以…$R;");
        }
    }
}