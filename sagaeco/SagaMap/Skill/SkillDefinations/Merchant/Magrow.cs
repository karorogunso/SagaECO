
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Merchant
{
    /// <summary>
    /// 高聲放歌（ファシーボイス）
    /// </summary>
    public class Magrow : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 1.0f + 0.4f * level;
            List<Actor> affected = new List<Actor>();
            affected.Add(dActor);
            SkillHandler.Instance.PhysicalAttack(sActor, affected, args, SkillHandler.DefType.IgnoreDefRight, SagaLib.Elements.Neutral, 0, factor, false);

            SagaMap.Skill.Additions.Global.DefaultBuff buff = new Additions.Global.DefaultBuff(args.skill, sActor, "破靈擊", 15000);
            buff.OnAdditionStart += this.StartEventHandler;
            buff.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, buff);
        }
        void StartEventHandler(Actor actor, SagaMap.Skill.Additions.Global.DefaultBuff skill)
        {
            int[] values = { 0, 70, 103, 142, 189, 241 };
            if (skill.Variable.ContainsKey("破靈擊maxmatkdown"))
                skill.Variable.Remove("破靈擊maxmatkdown");
            skill.Variable.Add("破靈擊maxmatkdown", values[skill.skill.Level]);

            actor.Status.max_matk_skill -= (short)values[skill.skill.Level];
            actor.Status.min_matk_skill -= (short)values[skill.skill.Level];

            actor.Buff.最大魔法攻撃力減少 = true;
            actor.Buff.最小魔法攻撃力減少 = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, SagaMap.Skill.Additions.Global.DefaultBuff skill)
        {
            actor.Status.max_matk_skill += (short)skill.Variable["破靈擊maxmatkdown"];
            actor.Status.min_matk_skill += (short)skill.Variable["破靈擊maxmatkdown"];

            actor.Buff.最大魔法攻撃力減少 = false;
            actor.Buff.最小魔法攻撃力減少 = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}
