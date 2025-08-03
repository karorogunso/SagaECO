
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaDB.Mob;
namespace SagaMap.Skill.SkillDefinations.Druid
{
    /// <summary>
    /// 祭司聖言（ディフィートカース）
    /// </summary>
    public class UndeadMdefDownOne : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0;
            if (dActor.type == ActorType.MOB)
            {
                //List<MobType> types = new List<MobType>();
                //types.Add(SagaDB.Mob.MobType.UNDEAD);
                //types.Add(SagaDB.Mob.MobType.UNDEAD_BOSS);
                //types.Add(SagaDB.Mob.MobType.UNDEAD_BOSS_BOMB_SKILL);
                //types.Add(SagaDB.Mob.MobType.UNDEAD_BOSS_CHAMP_BOMB_SKILL_NOTPTDROPRANGE);
                //types.Add(SagaDB.Mob.MobType.UNDEAD_BOSS_SKILL);
                //types.Add(SagaDB.Mob.MobType.UNDEAD_BOSS_SKILL_CHAMP);
                //types.Add(SagaDB.Mob.MobType.UNDEAD_BOSS_SKILL_NOTPTDROPRANGE);
                //types.Add(SagaDB.Mob.MobType.UNDEAD_NOTOUCH);
                //types.Add(SagaDB.Mob.MobType.UNDEAD_SKILL);

                ActorMob mob = (ActorMob)dActor;
                if (mob.BaseData.undead)
                {
                    factor = 2.15f + 1.15f * level;
                }
                else
                {
                    factor = 1.9f + 0.9f * level;
                }
            }
            else
            {
                factor = 1.9f + 0.9f * level;
            }
            int lifetime = 1000 * level;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "UndeadMdefDownOne", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
            //int rate = 70 + 10 * level;
            //if (SkillHandler.Instance.CanAdditionApply(sActor,dActor,"UndeadMdefDownOne", rate))
            //{

            //}
            SkillHandler.Instance.MagicAttack(sActor, dActor, args, SagaLib.Elements.Holy, factor);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            //int level = skill.skill.Level;
            ////左魔防
            //int mdef_add = -(int)(actor.Status.mdef * 0.5f);
            //if (skill.Variable.ContainsKey("UndeadMdefDownOne_mdef"))
            //    skill.Variable.Remove("UndeadMdefDownOne_mdef");
            //skill.Variable.Add("UndeadMdefDownOne_mdef", mdef_add);
            //actor.Status.mdef_skill += (short)mdef_add;

            ////右魔防
            //int mdef_add_add = -(int)(actor.Status.mdef_add * 0.5f);
            //if (skill.Variable.ContainsKey("UndeadMdefDownOne_mdef_add"))
            //    skill.Variable.Remove("UndeadMdefDownOne_mdef_add");
            //skill.Variable.Add("UndeadMdefDownOne_mdef_add", mdef_add_add);
            //actor.Status.mdef_add_skill += (short)mdef_add_add;

        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            ////左魔防
            //actor.Status.mdef_skill -= (short)skill.Variable["UndeadMdefDownOne_mdef"];

            ////右魔防
            //actor.Status.mdef_add_skill -= (short)skill.Variable["UndeadMdefDownOne_mdef_add"];

        }
        #endregion
    }
}
