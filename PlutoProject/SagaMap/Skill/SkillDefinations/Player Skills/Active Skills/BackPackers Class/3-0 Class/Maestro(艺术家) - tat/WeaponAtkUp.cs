using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;


namespace SagaMap.Skill.SkillDefinations.Maestro
{
    //ウエポンエンハンス
    class WeaponAtkUp : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            int[] lifetime = { 0, 120, 120, 180, 180, 210 };
            List<Actor> affected = map.GetActorsArea(dActor, 200, true);
            //List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (!SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                {
                    DefaultBuff skill = new DefaultBuff(args.skill, act, "MA_WeaponAtkUp", lifetime[level] * 1000);
                    skill.OnAdditionStart += this.StartEventHandler;
                    skill.OnAdditionEnd += this.EndEventHandler;
                    SkillHandler.ApplyAddition(act, skill);
                    EffectArg arg = new EffectArg();
                    arg.effectID = 4334;
                    arg.actorID = act.ActorID;
                    Manager.MapManager.Instance.GetMap(dActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, act, true);
                }
            }

        }

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int[] atk_up = { 0, 15, 30, 45, 60, 75 };

            if (skill.Variable.ContainsKey("MA_ATK_UP"))
                skill.Variable.Remove("MA_ATK_UP");
            skill.Variable.Add("MA_ATK_UP", atk_up[skill.skill.Level]);

            //最大攻擊
            actor.Status.max_atk1_skill += (short)atk_up[skill.skill.Level];

            //最大攻擊
            actor.Status.max_atk2_skill += (short)atk_up[skill.skill.Level];

            //最大攻擊
            actor.Status.max_atk3_skill += (short)atk_up[skill.skill.Level];

            //最小攻擊
            actor.Status.min_atk1_skill += (short)atk_up[skill.skill.Level];

            //最小攻擊
            actor.Status.min_atk2_skill += (short)atk_up[skill.skill.Level];

            //最小攻擊

            actor.Status.min_atk3_skill += (short)atk_up[skill.skill.Level];

            //暴击
            actor.Status.cri_skill_rate += (short)(atk_up[skill.skill.Level] / 5);

            actor.Buff.三转红锤子ウェポンエンハンス = true;
            actor.Buff.CriticalRateUp = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {

            //最大攻擊
            actor.Status.max_atk1_skill -= (short)skill.Variable["MA_ATK_UP"];

            //最大攻擊
            actor.Status.max_atk2_skill -= (short)skill.Variable["MA_ATK_UP"];

            //最大攻擊
            actor.Status.max_atk3_skill -= (short)skill.Variable["MA_ATK_UP"];

            //最小攻擊
            actor.Status.min_atk1_skill -= (short)skill.Variable["MA_ATK_UP"];

            //最小攻擊
            actor.Status.min_atk2_skill -= (short)skill.Variable["MA_ATK_UP"];

            //最小攻擊
            actor.Status.min_atk3_skill -= (short)skill.Variable["MA_ATK_UP"];

            //右防禦
            actor.Status.cri_skill_rate -= (short)(skill.Variable["MA_ATK_UP"] / 5);
            actor.Buff.三转红锤子ウェポンエンハンス = false;
            actor.Buff.CriticalRateUp = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
        //public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        //{
        //    return 0;
        //}

        //public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        //{
        //    int[] lifetime = {0,120000,120000,180000,180000,210000};
        //    DefaultBuff skill = new DefaultBuff(args.skill, dActor, "WeaponAtkUp", lifetime[level]);
        //    skill.OnAdditionStart += this.StartEventHandler;
        //    skill.OnAdditionEnd += this.EndEventHandler;
        //    SkillHandler.ApplyAddition(dActor, skill);
        //}
        //#endregion
        //void StartEventHandler(Actor actor, DefaultBuff skill)
        //{
        //    ushort up = (ushort)(5 + 10 * skill.skill.Level);
        //    if (skill.Variable.ContainsKey("WeaponAtkUp"))
        //        skill.Variable.Remove("WeaponAtkUp");
        //    skill.Variable.Add("WeaponAtkUp", (int)up);
        //    actor.Status.max_atk1 += up;
        //    actor.Status.max_atk2 += up;
        //    actor.Status.max_atk3 += up;
        //    actor.Status.min_atk1 += up;
        //    actor.Status.min_atk2 += up;
        //    actor.Status.min_atk3 += up;
        //    actor.Buff.三转红锤子ウェポンエンハンス = true;
        //    Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        //}
        //void EndEventHandler(Actor actor, DefaultBuff skill)
        //{
        //    actor.Status.max_atk1 -= (ushort)skill.Variable["WeaponAtkUp"];
        //    actor.Status.max_atk2 -= (ushort)skill.Variable["WeaponAtkUp"];
        //    actor.Status.max_atk3 -= (ushort)skill.Variable["WeaponAtkUp"];
        //    actor.Status.min_atk1 -= (ushort)skill.Variable["WeaponAtkUp"];
        //    actor.Status.min_atk2 -= (ushort)skill.Variable["WeaponAtkUp"];
        //    actor.Status.min_atk3 -= (ushort)skill.Variable["WeaponAtkUp"];
        //    actor.Buff.三转红锤子ウェポンエンハンス = false;
        //    Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        //}
    }
}
//if (i.Status.Additions.ContainsKey("イレイザー") 