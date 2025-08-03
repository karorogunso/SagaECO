using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Sorcerer
{
    /// <summary>
    /// マジックバリア
    /// </summary>
    public class MagicBarrier : ISkill
    {
        bool MobUse;
        public MagicBarrier()
        {
            this.MobUse = false;
        }
        public MagicBarrier(bool MobUse)
        {
            this.MobUse = MobUse;
        }
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (MobUse)
                level = 5;

            int life = 60000 + 120000 * level;

            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 250, true);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (!SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                {
                    SkillHandler.RemoveAddition(act, "DevineBarrier");
                    DefaultBuff skill = new DefaultBuff(args.skill, act, "MagicBarrier", life);
                    skill.OnAdditionStart += this.StartEventHandler;
                    skill.OnAdditionEnd += this.EndEventHandler;
                    SkillHandler.ApplyAddition(act, skill);
                    EffectArg arg2 = new EffectArg();
                    arg2.effectID = 5169;
                    arg2.actorID = act.ActorID;


                    Manager.MapManager.Instance.GetMap(act.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg2, act, true);
                }
            }

        }

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int atk1 = 0, atk2 = 0;
            int level3114 = 0;
            if (actor is ActorPC)
            {
                ActorPC pc = actor as ActorPC;

                //不管是主职还是副职, 只要习得剑圣技能, 都会导致combo成立, 这里一步就行了
                if (pc.Skills.ContainsKey(3114) || pc.DualJobSkill.Exists(x => x.ID == 3114))
                {
                    var duallv = 0;
                    if (pc.DualJobSkill.Exists(x => x.ID == 3114))
                        duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 3114).Level;

                    //这里取主职的剑圣等级
                    var mainlv = 0;
                    if (pc.Skills.ContainsKey(3114))
                        mainlv = pc.Skills[3114].Level;

                    //这里取等级最高的剑圣等级用来做居合的倍率加成
                    level3114 += Math.Max(duallv, mainlv);
                }
                else
                {
                    level3114 = 1;
                }
            }
            switch (level3114)
            {
                case 1:
                    atk1 = 4;
                    atk2 = 5;
                    break;
                case 2:
                    atk1 = 8;
                    atk2 = 10;
                    break;
                case 3:
                    atk1 = 12;
                    atk2 = 10;
                    break;
                case 4:
                    atk1 = 16;
                    atk2 = 15;
                    break;
                case 5:
                    atk1 = 20;
                    atk2 = 15;
                    break;
            }

            if (skill.Variable.ContainsKey("MDef"))
                skill.Variable.Remove("MDef");
            skill.Variable.Add("MDef", atk1);
            actor.Status.mdef_skill += (short)atk1;
            if (skill.Variable.ContainsKey("MDefAdd"))
                skill.Variable.Remove("MDefAdd");
            skill.Variable.Add("MDefAdd", atk2);
            actor.Status.mdef_add_skill += (short)atk2;

            actor.Buff.MagicDefUp = true;
            actor.Buff.MagicDefRateUp = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            int value = skill.Variable["MDef"];
            actor.Status.mdef_skill -= (short)value;
            value = skill.Variable["MDefAdd"];
            actor.Status.mdef_add_skill -= (short)value;

            actor.Buff.MagicDefUp = false;
            actor.Buff.MagicDefRateUp = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        public void RemoveAddition(Actor actor, String additionName)
        {
            if (actor.Status.Additions.ContainsKey(additionName))
            {
                Addition addition = actor.Status.Additions[additionName];
                actor.Status.Additions.Remove(additionName);
                if (addition.Activated)
                {
                    addition.AdditionEnd();
                }
                addition.Activated = false;
            }
        }
        #endregion
    }
}