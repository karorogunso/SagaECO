using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Skill.Additions.Global;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31097 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, Elements.Dark, 4f);
            Stun stun = new Stun(null, dActor, 3000);
            SkillHandler.ApplyAddition(dActor, stun);
            SkillHandler.Instance.ActorSpeak(sActor, "我哪里有骗你哦？你看我不是飞过来了嘛。");
        }
    }
}
