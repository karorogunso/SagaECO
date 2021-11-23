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
                Say(pc, 131, "很久很久以前，有一个传说…$R;" +
                    "$R在一个很远很远的地方$R;" +
                    "有一座神创造的塔，$R;" +
                    "是通往天上的塔…$R;" +
                    "$P而这座光之塔$R;" +
                    "就是埃米尔仿照那座塔所建造的…$R;" +
                    "$P结果，如您所见，$R;" +
                    "虽然没有通天…$R;");
            }
            else
            {
                Say(pc, 131, "上面就是天空…$R;" +
                    "临近泰达尼亚世界的地方…$R;" +
                    "$R这附近的魔物$R;" +
                    "本来住在泰达尼亚世界…$R;" +
                    "$P怎么会到这个世界来的呢？$R;");
                if (pc.Race == PC_RACE.TITANIA)//ME.RACE = 1
                {
                    Say(pc, 131, "嗯，其实您也很明白，$R;" +
                        "只是记忆被封印了…$R;" +
                        "$R记忆赶快恢复过来，该多好呀。$R;");
                    return;
                }
            }
        }
    }
}