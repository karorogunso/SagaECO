
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.BountyHunter
{
    /// <summary>
    /// 堅固肉體（ソリッドボディ）
    /// </summary>
    public class SolidBody : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }

        bool CheckPossible(Actor sActor)
        {
            return true;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //効果時間(s)	15	30	50	70	90 wiki信息
            //int lifetime = 5000 + 2000 * level;错误时间
            //加上凭依对象,该技能可对宿主使用
            //依然无法实装抗性部分
            args.dActor = 0;
            Actor realdActor = SkillHandler.Instance.GetPossesionedActor((ActorPC)sActor);
            if (CheckPossible(realdActor))
            {
                int[] lifetimes = new int[] { 0, 15000, 30000, 50000, 70000, 90000 };
                DefaultBuff skill = new DefaultBuff(args.skill, realdActor, "SolidBody", lifetimes[level]);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(realdActor, skill);
            }

            //加上暈眩抗性
            //args.autoCast.Add(SkillHandler.Instance.CreateAutoCastInfo(3057, 5, 0));
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int level = skill.skill.Level;
            //左防禦
            //int def_add = (int)(3 * level);
            float def_add = 0.02f + 0.02f * level;
            if (skill.Variable.ContainsKey("SolidBody_def"))
                skill.Variable.Remove("SolidBody_def");
            skill.Variable.Add("SolidBody_def", (int)(actor.Status.def * def_add));
            actor.Status.def_skill += (short)(actor.Status.def * def_add);

            actor.Buff.SolidBody = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //左防禦
            actor.Status.def_skill -= (short)skill.Variable["SolidBody_def"];
            actor.Buff.SolidBody = false;

            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}
