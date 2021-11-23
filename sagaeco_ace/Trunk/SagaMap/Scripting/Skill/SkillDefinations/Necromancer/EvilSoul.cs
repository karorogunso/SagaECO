
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Necromancer
{
    /// <summary>
    /// 邪惡靈魂（イビルソウル）
    /// </summary>
    public class EvilSoul : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 40000 + 10000 * level;
            DefaultBuff skill = new DefaultBuff(args.skill, sActor, "EvilSoul", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.darkZenList.Add(3083);//闇靈之力
            actor.Status.darkZenList.Add(3134);//黑暗毒血
            actor.Status.darkZenList.Add(3085);//死亡天幕
            actor.Status.darkZenList.Add(3093);//黑暗子彈
            actor.Status.darkZenList.Add(2229);//黑暗刀刃
            actor.Status.darkZenList.Add(3167);//黑暗苦痛
            actor.Status.darkZenList.Add(3327);//虛弱幻界
            actor.Status.darkZenList.Add(3272);//闇術師刻印
            actor.Status.darkZenList.Add(3310);//黑暗火焰
            actor.Status.darkZenList.Add(3290);//魔王之火
            actor.Status.darkZenList.Add(3332);//混沌之門

            actor.Status.darkZenList.Add(3323);//死亡進行曲
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.darkZenList.Clear();
        }
        #endregion
    }
}
