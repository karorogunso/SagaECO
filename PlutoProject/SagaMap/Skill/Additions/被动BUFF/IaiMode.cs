using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaDB.Skill;
namespace SagaMap.Skill.Additions.Global
{
    /// <summary>
    /// 居合姿态
    /// </summary>
    public class IaiMode : DefaultBuff
    {
        public IaiMode(SagaDB.Skill.Skill skill, Actor actor, int lifetime)
            : base(skill, actor, "居合姿态启动", lifetime)
        {
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {

            if (actor.type == ActorType.PC)
            {
                SagaDB.Skill.Skill skill2 = SkillFactory.Instance.GetSkill(2178, 1);
                if (!((ActorPC)actor).Skills.ContainsKey(2178))
                    ((ActorPC)actor).Skills.Add(2178,skill2);
            }

            float rate = 0.3f;

            actor.Speed -= 400;

            //最大攻擊
            int max_atk1_add = (int)(actor.Status.max_atk_bs * rate);
            if (skill.Variable.ContainsKey("IaiMode_max_atk1"))
                skill.Variable.Remove("IaiMode_max_atk1");
            skill.Variable.Add("IaiMode_max_atk1", max_atk1_add);
            actor.Status.max_atk1_skill += (short)max_atk1_add;

            //最大攻擊
            int max_atk2_add = (int)(actor.Status.max_atk_bs * rate);
            if (skill.Variable.ContainsKey("IaiMode_max_atk2"))
                skill.Variable.Remove("IaiMode_max_atk2");
            skill.Variable.Add("IaiMode_max_atk2", max_atk2_add);
            actor.Status.max_atk2_skill += (short)max_atk2_add;

            //最大攻擊
            int max_atk3_add = (int)(actor.Status.max_atk_bs * rate);
            if (skill.Variable.ContainsKey("IaiMode_max_atk3"))
                skill.Variable.Remove("IaiMode_max_atk3");
            skill.Variable.Add("IaiMode_max_atk3", max_atk3_add);
            actor.Status.max_atk3_skill += (short)max_atk3_add;

            //最小攻擊
            int min_atk1_add = (int)(actor.Status.min_atk_bs * rate);
            if (skill.Variable.ContainsKey("IaiMode_min_atk1"))
                skill.Variable.Remove("IaiMode_min_atk1");
            skill.Variable.Add("IaiMode_min_atk1", min_atk1_add);
            actor.Status.min_atk1_skill += (short)min_atk1_add;

            //最小攻擊
            int min_atk2_add = (int)(actor.Status.min_atk_bs * rate);
            if (skill.Variable.ContainsKey("IaiMode_min_atk2"))
                skill.Variable.Remove("IaiMode_min_atk2");
            skill.Variable.Add("IaiMode_min_atk2", min_atk2_add);
            actor.Status.min_atk2_skill += (short)min_atk2_add;

            //最小攻擊
            int min_atk3_add = (int)(actor.Status.min_atk_bs * rate);
            if (skill.Variable.ContainsKey("IaiMode_min_atk3"))
                skill.Variable.Remove("IaiMode_min_atk3");
            skill.Variable.Add("IaiMode_min_atk3", min_atk3_add);
            actor.Status.min_atk3_skill += (short)min_atk3_add;



            /*Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.狂戦士 = true;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);*/
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {

            if (actor.type == ActorType.PC)
            {
                SagaMap.Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("居合姿势取消。"); actor.Speed = 410;
                if (((ActorPC)actor).Skills.ContainsKey(2178))
                    ((ActorPC)actor).Skills.Remove(2178);
            }
            //最大攻擊
            actor.Status.max_atk1_skill -= (short)skill.Variable["IaiMode_max_atk1"];

            //最大攻擊
            actor.Status.max_atk2_skill -= (short)skill.Variable["IaiMode_max_atk2"];

            //最大攻擊
            actor.Status.max_atk3_skill -= (short)skill.Variable["IaiMode_max_atk3"];

            //最小攻擊
            actor.Status.min_atk1_skill -= (short)skill.Variable["IaiMode_min_atk1"];

            //最小攻擊
            actor.Status.min_atk2_skill -= (short)skill.Variable["IaiMode_min_atk2"];

            //最小攻擊
            actor.Status.min_atk3_skill -= (short)skill.Variable["IaiMode_min_atk3"];


            /*Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.狂戦士 = false;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);*/
        }
    }
}
