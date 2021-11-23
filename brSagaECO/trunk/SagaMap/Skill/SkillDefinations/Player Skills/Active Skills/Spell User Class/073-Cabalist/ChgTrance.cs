
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaDB.Skill;
namespace SagaMap.Skill.SkillDefinations.Cabalist
{
    /// <summary>
    /// 蝙蝠變身（トランスフォーム）
    /// </summary>
    public class ChgTrance : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 300000;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "ChgTrance", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            ActorPC pc=(ActorPC)actor;
            switch (skill.skill.Level)
            {
                case 1:
                    AddSkill(pc, 952, 1);//吸血
                    AddSkill(pc, 3282, 1);//隱身
                    //變身成蝙蝠
                    break;
                case 2:
                    AddSkill(pc, 6309, 1);//野獸震怒
                    AddSkill(pc, 6303, 1);//咆哮
                    SkillHandler.Instance.TranceMob(pc, 10136901);//魔法狼
                    break;
                case 3:
                    AddSkill(pc, 3279, 1);//元靈護盾
                    AddSkill(pc, 3280, 1);//元素護盾
                    AddSkill(pc, 7738, 1);//魔法衝擊波
                    SkillHandler.Instance.TranceMob(pc, 10410000);//詛咒者
                    break;
            }
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            ActorPC pc = (ActorPC)actor;
            switch (skill.skill.Level)
            {
                case 1:
                    DelSkill(pc, 952);//吸血
                    DelSkill(pc, 3282);//隱身
                    break;
                case 2:
                    DelSkill(pc, 6309);//野獸震怒
                    DelSkill(pc, 6303);//咆哮
                    break;
                case 3:
                    DelSkill(pc, 3279);//元靈護盾
                    DelSkill(pc, 3280);//元素護盾
                    DelSkill(pc, 7738);//魔法衝擊波
                    break;
            }
            SkillHandler.Instance.TranceMob(pc, 0);
        }

        void AddSkill(ActorPC actor,uint SkillID,byte lv)
        {
            var s = SkillFactory.Instance.GetSkill(SkillID, lv);
            s.NoSave = true;
            actor.Skills.Add(s.ID, s);
        }
        void DelSkill(ActorPC actor, uint SkillID)
        {
            var s = (from SagaDB.Skill.Skill x in actor.Skills
                     where x.ID == SkillID
                     select x).First();
            actor.Skills.Remove(s.ID );
        }
        #endregion
    }
}
