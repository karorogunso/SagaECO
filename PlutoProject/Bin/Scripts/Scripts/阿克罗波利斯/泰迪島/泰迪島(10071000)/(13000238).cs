using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10071000
{
    public class S13000238 : Event
    {
        public S13000238()
        {
            this.EventID = 13000238;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<hanabi1> hanabi1_mask = new BitMask<hanabi1>(pc.CMask["hanabi1"]);
            if (hanabi1_mask.Test(hanabi1.第一次对话后))
            {
                Say(pc, 0, 0, "壁に妙な違和感を感じる…$R;" +
                "すり抜けられそうだ$R;", "");
                if (Select(pc, "中に入る？", "", "入る", "入らない") == 1)
                {
                    pc.CInt["Beginner_Map"] = CreateMapInstance(50064000, 10071000, 184, 180);

                    //x = (byte)Global.Random.Next(21, 23);
                    //y = (byte)Global.Random.Next(34, 36);

                    Warp(pc, (uint)pc.CInt["Beginner_Map"], (byte)Global.Random.Next(21, 23), (byte)Global.Random.Next(34, 36));
                }
            }
        }
    }
}