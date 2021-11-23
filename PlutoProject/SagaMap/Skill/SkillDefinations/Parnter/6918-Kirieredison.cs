using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Network.Client;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Global
{
    /// <summary>
    /// 聖印・キリエエレイソン
    /// </summary>
    public class Kirieredison : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float recoverfactor = -5.0f;

            float damagefactor = -6.0f;

            int lifetime = 120000;

            
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(SagaLib.Global.PosX8to16(args.x, map.Width), SagaLib.Global.PosY8to16(args.y, map.Height), 200, null);
            List<Actor> recoveraffected = new List<Actor>();
            List<Actor> damageaffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (act.type == ActorType.PC)
                {
                    ActorPC m = (ActorPC)act;
                    if (m.Buff.Dead != true)
                    {
                        recoveraffected.Add(act);
                    }
                    else if (m.Buff.TurningPurple != true)
                    {
                        m.Buff.TurningPurple = true;
                        MapClient.FromActorPC(m).Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, m, true);
                        m.TInt["Revive"] = level;
                        MapClient.FromActorPC(m).EventActivate(0xF1000000);
                        m.TStr["Revive"] = sActor.Name;
                        MapClient.FromActorPC(m).SendSystemMessage(string.Format("玩家 {0} 正在请求你复活", sActor.Name));
                    }
                    if (!act.Status.Additions.ContainsKey("AllHealing") && !act.Buff.Undead)
                    {
                        DefaultBuff skill = new DefaultBuff(args.skill, act, "AllHealing", lifetime);
                        skill.OnAdditionStart += this.StartEventHandler;
                        skill.OnAdditionEnd += this.EndEventHandler;
                        SkillHandler.ApplyAddition(act, skill);
                    }
                    if (act.Buff.Undead)
                    {
                        damageaffected.Add(act);
                    }
                }
                else if (act.type == ActorType.PARTNER || act.type == ActorType.PET)
                {
                    if (act.Buff.Dead != true)
                    {
                        recoveraffected.Add(act);
                    }
                    if (act.Buff.Undead)
                    {
                        damageaffected.Add(act);
                    }
                }
                else if (act.type == ActorType.MOB)
                {
                    ActorMob m = (ActorMob)act;
                    if (m.BaseData.undead)
                        damageaffected.Add(act);
                }
            }
            SkillHandler.Instance.MagicAttack(sActor, recoveraffected, args, SkillHandler.DefType.IgnoreAll, SagaLib.Elements.Holy, recoverfactor);
            SkillHandler.Instance.MagicAttack(sActor, damageaffected, args, SagaLib.Elements.Holy, damagefactor);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            if (skill.Variable.ContainsKey("AllHealing_MP"))
                skill.Variable.Remove("AllHealing_MP");
            skill.Variable.Add("AllHealing_MP", 15);
            actor.Status.mp_recover_skill += 15;
            if (skill.Variable.ContainsKey("AllHealing_HP"))
                skill.Variable.Remove("AllHealing_HP");
            skill.Variable.Add("AllHealing_HP", 15);
            actor.Status.hp_recover_skill += 15;
            if (skill.Variable.ContainsKey("AllHealing_SP"))
                skill.Variable.Remove("AllHealing_SP");
            skill.Variable.Add("AllHealing_SP", 15);
            actor.Status.sp_recover_skill += 15;
            actor.Buff.HPRegenUp = true;
            actor.Buff.SPRegenUp = true;
            actor.Buff.MPRegenUp = true;
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.mp_recover_skill -= (short)skill.Variable["AllHealing_MP"];
            actor.Status.hp_recover_skill -= (short)skill.Variable["AllHealing_HP"];
            actor.Status.sp_recover_skill -= (short)skill.Variable["AllHealing_SP"];
            actor.Buff.HPRegenUp = false;
            actor.Buff.SPRegenUp = false;
            actor.Buff.MPRegenUp = false;
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}
