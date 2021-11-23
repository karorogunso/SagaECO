using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50008000
{
    public class S11001084 : Event
    {
        public S11001084()
        {
            this.EventID = 11001084;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_03> Neko_03_amask = pc.AMask["Neko_03"];
            BitMask<Neko_03> Neko_03_cmask = pc.CMask["Neko_03"];
            //EVT11001084
            if (Neko_03_amask.Test(Neko_03.堇子任務開始) &&
                !Neko_03_amask.Test(Neko_03.堇子任務完成) &&
                Neko_03_cmask.Test(Neko_03.找到理路) &&
                !Neko_03_cmask.Test(Neko_03.使用了電晶體) &&
                CountItem(pc, 10023509) >= 1)
            {
                Say(pc, 11001084, 131, "都搞定了…！?$R;" +
                    "$R哇！真厲害呀！$R;" +
                    "$P……不能越過門逃走嗎?$R;");
                return;
            }
            if (Neko_03_amask.Test(Neko_03.堇子任務開始) &&
                !Neko_03_amask.Test(Neko_03.堇子任務完成) &&
                Neko_03_cmask.Test(Neko_03.使用了電晶體) &&
                !Neko_03_cmask.Test(Neko_03.帶理路離開))
            {
                if (pc.Gender == PC_GENDER.FEMALE)
                {
                    Say(pc, 11001084, 131, "姐姐…$R;");
                }
                else
                {
                    Say(pc, 11001084, 131, "哥哥…$R;");
                }
                Say(pc, 0, 131, "快帶理路逃走吧！$R;" +
                    "$R雖然瑪莎和加多會很擔心…$R;");
                Neko_03_cmask.SetValue(Neko_03.帶理路離開, true);
                TakeItem(pc, 10023509, 10);
                Warp(pc, 30102001, 5, 7);
                return;
            }
            if (pc.Gender == PC_GENDER.FEMALE)
            {
                Say(pc, 11001084, 131, "…姐姐…！$R這裡好恐怖…！$R;");
                return;
            }
            Say(pc, 11001084, 131, "…哥哥…！$R這裡好恐怖…！$R;");
        }
    }
}