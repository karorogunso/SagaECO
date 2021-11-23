using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaLib;
namespace SagaScript.M12035001
{
    public class S11001912 : Event
    {
        public S11001912()
        {
            this.EventID = 11001912;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Jief> Jief_cmask = pc.CMask["Jief"];
            //int selection;
            if (Jief_cmask.Test(Jief.使用过物品))
            {

                NPCHide(pc, 11001638);//隐藏NPC
            }
            else
            {
                NPCShow(pc, 11001638);//让NPC出现
            }
        }
    }
}