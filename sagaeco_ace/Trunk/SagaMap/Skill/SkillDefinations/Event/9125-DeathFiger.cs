using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaMap.Network.Client;
using SagaDB.Skill;
namespace SagaMap.Skill.SkillDefinations.Event
{
    public class DeathFiger :ISkill
    {
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            SkillFactory.Instance.GetSkill(9125, 1).Effect = 5368;
            SkillFactory.Instance.GetSkill(9125, 1).EffectRange = 0;
            SkillFactory.Instance.GetSkill(9125, 1).Target = 2;
            SkillFactory.Instance.GetSkill(9125, 1).Target2 = 1;
            return 0;
            
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (!(dActor is ActorPC))
                return;
            List<Actor> actors = Manager.MapManager.Instance.GetMap(sActor.MapID).GetActorsArea(sActor, 1000, false);
            List<Actor> affected = new List<Actor>();
            args.affectedActors.Clear();
            foreach (var item in actors)
            {
                if (item is ActorPC || item is ActorMob)
                    affected.Add(item);
            }
            ChatArg arg = new ChatArg();
            arg.content = "哈哈哈,这招怎么样?!";
            Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHAT, arg, sActor, true);
            SkillHandler.Instance.FixAttack(sActor, affected, args, SagaLib.Elements.Neutral, 100000000f);
        }
    }
}
