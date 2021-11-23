using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Sorcerer
{
    /// <summary>
    /// 瞬間移動（テレポート）
    /// </summary>
    public class Teleport : ISkill 
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("Teleport"))
            {
                return -30;
            }
            else
            {
                return 0;
            }
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            map.TeleportActor(sActor,SagaLib.Global.PosX8to16(args.x, map.Width),SagaLib.Global.PosY8to16(args.y, map.Height));
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "Teleport", 5000);
            skill.OnAdditionStart += this.StartEvent;
            skill.OnAdditionEnd += this.EndEvent;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEvent(Actor actor, DefaultBuff skill)
        {
        }
        void EndEvent(Actor actor, DefaultBuff skill)
        {
        }
        #endregion
    }
}
