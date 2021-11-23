using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Vates
{
    /// <summary>
    /// 各種抗性
    /// </summary>
    public class StatusRegi : ISkill
    {
        private string StatusName;
        public StatusRegi(string StatusName)
        {
            this.StatusName = StatusName;
        }
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 1200000 + 120000 * level;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, StatusName + "Regi", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            ChangeBuffIcon(actor,true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            ChangeBuffIcon(actor,false);
        }
        void ChangeBuffIcon(Actor actor, bool OnOff)
        {
            switch (StatusName)
            {
                case "Sleep":
                    actor.Buff.SleepResist = OnOff;
                    break;
                case "Poison":
                    actor.Buff.PoisonResist = OnOff;
                    break;
                case "Stun":
                    actor.Buff.FaintResist = OnOff;
                    break;
                case "Silence":
                    actor.Buff.SilenceResist = OnOff;
                    break;
                case "Stone":
                    actor.Buff.StoneResist = OnOff;
                    break;
                case "Confuse":
                    actor.Buff.ConfuseResist = OnOff;
                    break;
                case "鈍足":
                    actor.Buff.SpeedDownResist = OnOff;
                    break;
                case "Frosen":
                    actor.Buff.FrosenResist = OnOff;
                    break;
            }
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}
