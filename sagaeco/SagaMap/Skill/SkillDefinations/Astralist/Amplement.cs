using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Astralist
{
    public class Amplement : ISkill
    {
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (sActor.Party != null) return 0;
            else return -12;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int[] lifetime = { 0, 60000, 100000, 140000, 180000, 220000 };
            List<Actor> realAffected = new List<Actor>();
            ActorPC sPC = (ActorPC)sActor;
            foreach (ActorPC act in sPC.Party.Members.Values)
            {
                if (act.Online)
                {
                    if (act.Party.ID != 0 && !act.Buff.Dead && act.MapID == sActor.MapID)
                    {
                        DefaultBuff skill = new DefaultBuff(args.skill, act, "Amplement", lifetime[level]);
                        skill.OnAdditionStart += this.StartEventHandler;
                        skill.OnAdditionEnd += this.EndEventHandler;
                        SkillHandler.ApplyAddition(act, skill);
                    }
                }
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int[] el_add = { 0, 12, 14, 17, 21, 26 };
            if (skill.Variable.ContainsKey("Amplement"))
                skill.Variable.Remove("Amplement");
            skill.Variable.Add("Amplement", el_add[skill.skill.Level]);
            actor.Status.elements_skill[SagaLib.Elements.Earth] += (int)el_add[skill.skill.Level];
            actor.Status.elements_skill[SagaLib.Elements.Fire] += (int)el_add[skill.skill.Level];
            actor.Status.elements_skill[SagaLib.Elements.Holy] += (int)el_add[skill.skill.Level];
            actor.Status.elements_skill[SagaLib.Elements.Water] += (int)el_add[skill.skill.Level];
            actor.Status.attackelements_skill[SagaLib.Elements.Earth] += (int)el_add[skill.skill.Level];
            actor.Status.attackelements_skill[SagaLib.Elements.Fire] += (int)el_add[skill.skill.Level];
            actor.Status.attackelements_skill[SagaLib.Elements.Holy] += (int)el_add[skill.skill.Level];
            actor.Status.attackelements_skill[SagaLib.Elements.Water] += (int)el_add[skill.skill.Level];
            actor.Buff.三转四属性赋予アンプリエレメント = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.elements_skill[SagaLib.Elements.Earth] += (int)skill.Variable["Amplement"];
            actor.Status.elements_skill[SagaLib.Elements.Fire] -= (int)skill.Variable["Amplement"];
            actor.Status.elements_skill[SagaLib.Elements.Holy] -= (int)skill.Variable["Amplement"];
            actor.Status.elements_skill[SagaLib.Elements.Water] -= (int)skill.Variable["Amplement"];
            actor.Status.attackelements_skill[SagaLib.Elements.Earth] -= (int)skill.Variable["Amplement"];
            actor.Status.attackelements_skill[SagaLib.Elements.Fire] -= (int)skill.Variable["Amplement"];
            actor.Status.attackelements_skill[SagaLib.Elements.Holy] -= (int)skill.Variable["Amplement"];
            actor.Status.attackelements_skill[SagaLib.Elements.Water] -= (int)skill.Variable["Amplement"];
            actor.Buff.三转四属性赋予アンプリエレメント = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}
