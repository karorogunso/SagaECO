using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20163000
{
    public class S11001257 : Event
    {
        public S11001257()
        {
            this.EventID = 11001257;
        }

        public override void OnEvent(ActorPC pc)
        {
            int a = Global.Random.Next(1, 2);
            if (a == 1)
            {
                Say(pc, 131, "很久很久以前，有一個傳說…$R;" +
                    "$R在一個很遠很遠的地方$R;" +
                    "有一座神創造的塔唷，$R;" +
                    "是通往天上的塔…$R;" +
                    "$P而這座光之塔$R;" +
                    "就是埃米爾仿照那座塔所建造的…$R;" +
                    "$P結果，如您所見，$R;" +
                    "雖然沒有通天…$R;");
            }
            else
            {
                Say(pc, 131, "這裡是天…$R;" +
                    "臨近塔妮亞世界的地方…$R;" +
                    "$R這附近的魔物$R;" +
                    "本來住在塔妮亞世界…$R;" +
                    "$P怎麼會到這個世界來的呢？$R;");
                if (pc.Race == PC_RACE.TITANIA)//ME.RACE = 1
                {
                    Say(pc, 131, "嗯，其實您也很明白，$R;" +
                        "只是記憶被封印了…$R;" +
                        "$R記憶趕快恢復過來，該多好呀。$R;");
                    return;
                }
            }
        }
    }
}