using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaMap.Skill.SkillDefinations.ForceMaster
{
    /// <summary>
    /// ディスペルフィールド
    /// </summary>
    class DispelField : ISkill
    {
        public int TryCast(SagaDB.Actor.ActorPC sActor, SagaDB.Actor.Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 60000 * level;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(sActor, 200, true);
            List<Actor> affected = new List<Actor>();
            if (actors.Count >= 0)
            {
                foreach (var i in actors)
                {
                    if (i is ActorPC)
                        if (!SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                            affected.Add(i);
                }
                foreach (var item in affected)
                {
                    DefaultBuff skill = new DefaultBuff(args.skill, item, "DispelField", lifetime);
                    skill.OnAdditionStart += this.StartEventHandler;
                    skill.OnAdditionEnd += this.EndEventHandler;
                    SkillHandler.ApplyAddition(item, skill);
                }
            }
        }

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int[] timeses = new int[] { 0, 1, 1, 1, 2, 3 };
            int times = timeses[skill.skill.Level];
            skill["DispelField"] = times;
            if (actor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)actor;
                Network.Client.MapClient.FromActorPC(pc).SendSystemMessage(string.Format(Manager.LocalManager.Instance.Strings.SKILL_STATUS_ENTER, skill.skill.Name));
            }
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            if (actor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)actor;
                Network.Client.MapClient.FromActorPC(pc).SendSystemMessage(string.Format(Manager.LocalManager.Instance.Strings.SKILL_STATUS_LEAVE, skill.skill.Name));
            }
        }
    }
}
