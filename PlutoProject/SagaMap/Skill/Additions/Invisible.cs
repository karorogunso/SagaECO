using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;
using SagaLib;

namespace SagaMap.Skill.Additions.Global
{
    public class Invisible : DefaultBuff
    {
        /// <summary>
        /// 隐身，每2秒会向周围队友发送自己的位置（显示特效）
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="actor"></param>
        /// <param name="lifetime">持续时间</param>
        public Invisible(SagaDB.Skill.Skill skill, Actor actor, int lifetime)
            : base(skill, actor, "Invisible", lifetime, 2000)
        {
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
            this.OnUpdate += this.UpdateEvent;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            /*---------移除仇恨---------*/
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.DISAPPEAR, null, actor, false);

            SkillHandler.Instance.ShowEffectByActor(actor, 4102);
            actor.Buff.Transparent = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.APPEAR, null, actor, false);

            actor.Buff.Transparent = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void UpdateEvent(Actor actor, DefaultBuff skill)
        {
            try
            {
                if (actor.HP > 0 && !actor.Buff.Dead)
                {
                    if(actor.type == ActorType.PC)
                    {
                        ActorPC pc = (ActorPC)actor;
                        if(pc.Party != null)
                        {
                            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
                            List<Actor> target = map.GetActorsArea(pc, 1000, true);
                            foreach (var item in target)
                            {
                                if(item.type == ActorType.PC)
                                {
                                    ActorPC pm = (ActorPC)item;
                                    if(pm.Online && pm.Party == pc.Party)
                                    {
                                        SkillHandler.Instance.ShowEffectByActor(pc, 4238);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }
    }
}
