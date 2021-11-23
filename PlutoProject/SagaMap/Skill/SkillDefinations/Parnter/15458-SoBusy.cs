using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Global
{
    /// <summary>
    /// ビビってなんか、いないんだから！(警戒模板)
    /// </summary>
    public class SoBusy : ISkill
    {
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (sActor.Status.Additions.ContainsKey("Warn"))
                return -1;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 60000;
            args.dActor = 0;
            Actor realdActor = sActor;
            int a = SagaLib.Global.Random.Next(1, 2);
            ActorPartner pet = (ActorPartner)sActor;
            switch (a)
            {
                case 1:
                    realdActor = pet;
                    break;
                case 2:
                    realdActor = pet.Owner;
                    break;
            }
            DefaultBuff skill = new DefaultBuff(args.skill, realdActor, "Warn", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(realdActor, skill);

        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            if (skill.Variable.ContainsKey("ST_LEFT_DEF"))
                skill.Variable.Remove("ST_LEFT_DEF");
            skill.Variable.Add("ST_LEFT_DEF", 9);
            actor.Status.def_skill += (short)(9);
            actor.Buff.Warning = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.def_skill -= (short)skill.Variable["ST_LEFT_DEF"];

            actor.Buff.Warning = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}