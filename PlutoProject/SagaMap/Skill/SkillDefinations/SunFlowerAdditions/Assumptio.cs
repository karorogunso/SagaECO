using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.SunFlowerAdditions
{
    /// <summary>
    /// 圣母之祈福(Ragnarok)
    /// </summary>
    public class Assumptio : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {

            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 500, false);//实测7*7范围内怪物互补情况太差,更改为11*11
            List<Actor> realAffected = new List<Actor>();
            Actor ActorlowHP = sActor;
            //realAffected.Add(sActor);
            //foreach (Actor act in affected)
            //{
            //    if ((float)(act.HP / act.MaxHP) < (float)(ActorlowHP.HP / ActorlowHP.MaxHP) && !SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
            //    {
            //        realAffected.Add(act);
            //    }
            //}
            //int nums = realAffected.Count;
            //ActorlowHP = sActor;// realAffected[SagaLib.Global.Random.Next(1, nums) - 1];
            int lifetime = 10000;
            DefaultBuff skill = new DefaultBuff(args.skill, ActorlowHP, "Assumptio", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(ActorlowHP, skill);
            EffectArg arg = new EffectArg();
            arg.effectID = 5446;
            arg.actorID = ActorlowHP.ActorID;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, sActor, true);
            //SkillHandler.Instance.MagicAttack(sActor, ActorlowHP, args, SkillHandler.DefType.IgnoreAll, SagaLib.Elements.Holy, factor);
            //SkillHandler.Instance.FixAttack(sActor, ActorlowHP, args, SagaLib.Elements.Holy, factor);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Buff.DefRateUp = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Buff.DefRateUp = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}