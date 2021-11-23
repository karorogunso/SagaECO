
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Knight
{
    /// <summary>
    /// 防禦援手（ディフェンスアシスト）
    /// </summary>
    public class DJoint : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            //使用條件：憑依時
            if (sActor.PossessionTarget != 0)
            {
                if (!dActor.Status.Additions.ContainsKey("A_T_DJoint"))
                {
                    return 0;
                }
                else
                {
                    return -24;
                }
            }
            else
            {
                return -23;
            }
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 60000 - 10000 * level;
            float f = 0.1f + 0.1f * level;
            DJointBuff skill = new DJointBuff(args.skill, sActor, dActor, lifetime, f);
            SkillHandler.ApplyAddition(dActor, skill);
        }
        #endregion

        #region DJoint_Addition
        public class DJointBuff : DefaultBuff
        {
            private float rate;
            private Actor sActor;
            public DJointBuff(SagaDB.Skill.Skill skill, Actor sActor, Actor actor, int lifetime, float rate)
                : base(skill, actor, "DJoint", lifetime)
            {
                this.rate = rate;
                this.sActor = sActor;
                this["Target"] = (int)sActor.ActorID;
                this.OnAdditionStart += this.StartEvent;
                this.OnAdditionEnd += this.EndEvent;
            }

            void StartEvent(Actor actor, DefaultBuff skill)
            {
                skill["Rate"] = 10 + 10 * skill.skill.Level;
                //Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
                //actor.Buff.Sleep = true;
                //map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            }

            void EndEvent(Actor actor, DefaultBuff skill)
            {
                skill["Rate"] = 0;
                //Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
                //actor.Buff.Sleep = false;
                //map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            }
        }
        #endregion
    }
}
