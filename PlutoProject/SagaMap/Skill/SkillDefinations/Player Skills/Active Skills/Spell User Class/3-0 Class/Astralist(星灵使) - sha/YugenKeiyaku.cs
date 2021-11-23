using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Astralist
{
    /// <summary>
    /// 幽玄之契约：剑
    /// </summary>
    public class YugenKeiyaku : ISkill
    {
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (sActor.WeaponElement != Elements.Neutral)
            {
                return 0;
            }
            return -12;
        }
        public SkillArg arg = new SkillArg();
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {

            int lifetime = 150000 + 30000 * level;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "YugenKeiyaku", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);

        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            //暂时找不到正确的图标
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {

            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

    }
}
