using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;
using SagaDB.Item;


namespace SagaMap.Skill.SkillDefinations.Royaldealer
{
    /// <summary>
    /// 假币（フォールスマネー）
    /// </summary>
    class FalseMoney : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 1.0f + 0.1f * level;
            int lifetime = 7000;
            ActorPC pc = sActor as ActorPC;

            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 300, false);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                {
                    if (SkillHandler.Instance.CanAdditionApply(sActor, act, SkillHandler.DefaultAdditions.鈍足, 50))
                    {
                        MoveSpeedDown skills = new MoveSpeedDown(args.skill, act, lifetime);
                        SkillHandler.ApplyAddition(act, skills);
                    }
                    if (SkillHandler.Instance.CanAdditionApply(sActor, act, SkillHandler.DefaultAdditions.Confuse, 50))
                    {
                        Confuse skills = new Confuse(args.skill, act, lifetime);
                        SkillHandler.ApplyAddition(act, skills);
                    }
                    if (SkillHandler.Instance.CanAdditionApply(sActor, act, SkillHandler.DefaultAdditions.Silence, 50))
                    {
                        Silence skills = new Silence(args.skill, act, lifetime);
                        SkillHandler.ApplyAddition(act, skills);
                    }
                    if (SagaLib.Global.Random.Next(0, 99) > 50)
                    {
                        DefaultBuff skill = new DefaultBuff(args.skill, dActor, "FalseMoney", 180000);
                        skill.OnAdditionStart += this.StartEventHandler;
                        skill.OnAdditionEnd += this.EndEventHandler;
                        SkillHandler.ApplyAddition(dActor, skill);
                    }
                    realAffected.Add(act);
                }
            }
            SkillHandler.Instance.MagicAttack(sActor, realAffected, args, sActor.WeaponElement, factor);





        }

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            //actor.Buff.DefRateUp = true;
            if (skill.Variable.ContainsKey("Agi_down"))
                skill.Variable.Remove("Agi_down");
            skill.Variable.Add("Agi_down", 25);
            actor.Status.agi_skill -= 25;
            if (skill.Variable.ContainsKey("Dex_down"))
                skill.Variable.Remove("Dex_down");
            skill.Variable.Add("Dex_down", 25);
            actor.Status.dex_skill -= 25;
            actor.Buff.AGIDown = true;
            actor.Buff.DEXDown = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.agi_skill += (short)skill.Variable["Agi_down"];
            actor.Status.dex_skill += (short)skill.Variable["Dex_down"];
            actor.Buff.AGIDown = false;
            actor.Buff.DEXDown = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}
