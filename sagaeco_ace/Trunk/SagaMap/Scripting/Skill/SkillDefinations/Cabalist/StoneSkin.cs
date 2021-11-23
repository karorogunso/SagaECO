
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Cabalist
{
    /// <summary>
    /// 石化皮膚（メデューサスキン）
    /// </summary>
    public class StoneSkin : ISkill
    {
        bool MobUse;
        public StoneSkin()
        {
            this.MobUse = false;
        }
        public StoneSkin(bool MobUse)
        {
            this.MobUse = MobUse;
        }
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (MobUse)
            {
                level = 5;
            }
            int[] lifetime = {0,30,30,45,45,60};
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "StoneSkin", lifetime[level]*1000);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
        }
        #endregion
    }
}
