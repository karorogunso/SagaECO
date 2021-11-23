using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Wizard
{
    public class MagicShield : ISkill
    {
        bool MobUse;
        public MagicShield()
        {
            this.MobUse = false;
        }
        public MagicShield(bool MobUse)
        {
            this.MobUse = MobUse;
        }
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (dActor.Status.Additions.ContainsKey("DevineBarrier"))
            {
                return -12;
            }
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (MobUse)
            {
                level = 5;
            }
            if(MobUse==true)
            {
                Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
                List<Actor> affected = map.GetActorsArea(sActor, 500, false);
                List<Actor> realAffected = new List<Actor>();
                foreach (Actor act in affected)
                {
                    if (act.type == ActorType.MOB)
                    {
                        realAffected.Add(act);
                    }
                }
                realAffected.Add(sActor);
                foreach (Actor i in realAffected)
                {
                    if (!i.Status.Additions.ContainsKey("MagicShield"))
                    {
                        DefaultBuff skill1 = new DefaultBuff(args.skill, i, "MagicShield", 900000);
                        skill1.OnAdditionStart += this.StartEventHandler;
                        skill1.OnAdditionEnd += this.EndEventHandler;
                        SkillHandler.ApplyAddition(i, skill1);
                    }
                }
            }
            else
            {
                DefaultBuff skill = new DefaultBuff(args.skill, dActor, "MagicShield", 9000000);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(dActor, skill);
            }
            
        }

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            short mdefadd = new short[] { 0, 5, 10, 10, 15, 15 }[skill.skill.Level];
            short mdef = (short)(4 * skill.skill.Level);

            if (skill.Variable.ContainsKey("MagicShieldMDEF"))
                skill.Variable.Remove("MagicShieldMDEF");
            skill.Variable.Add("MagicShieldMDEF", mdef);

            if (skill.Variable.ContainsKey("MagicShieldMDEFADD"))
                skill.Variable.Remove("MagicShieldMDEFADD");
            skill.Variable.Add("MagicShieldMDEFADD", mdefadd);
            actor.Status.mdef_add_skill += mdefadd;
            actor.Status.mdef_skill += mdef;
            actor.Buff.MagicDefUp = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.mdef_skill -= (short)skill.Variable["MagicShieldMDEF"];
            actor.Status.mdef_add_skill -= (short)skill.Variable["MagicShieldMDEFADD"];
            actor.Buff.MagicDefUp = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}