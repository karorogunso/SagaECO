using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;

namespace SagaMap.Skill.Additions.Global
{
    public class ElementContract : OtherAddition
    {
        /// <summary>
        /// 6属性契约通用addition
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="actor"></param>
        /// <param name="lifetime">持续时间</param>
        /// <param name="ele">属性</param>
        public ElementContract(SagaDB.Skill.Skill skill, Actor actor, int lifetime, SagaLib.Elements ele)
            : base(skill, actor, "属性契约", lifetime)
        {
            if (this.Variable.ContainsKey("属性契约"))
                this.Variable.Remove("属性契约");
            this.Variable.Add("属性契约", (int)ele);
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
        }

        void StartEvent(Actor actor, OtherAddition skill)
        {
            if (actor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)(actor);
                if (pc.Online)
                {
                    string s = "";
                    switch(skill.Variable["属性契约"])
                    {
                        case 0:
                            s = "无";
                            break;
                        case 1:
                            s = "火";
                            break;
                        case 2:
                            s = "水";
                            break;
                        case 3:
                            s = "风";
                            break;
                        case 4:
                            s = "地";
                            break;
                        case 5:
                            s = "光";
                            break;
                        case 6:
                            s = "暗";
                            break;
                    }
                    Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("已获得『"+s+"属性』契约");
                }
            }
        }

        void EndEvent(Actor actor, OtherAddition skill)
        {
            if (skill.Variable.ContainsKey("属性契约"))
                skill.Variable["属性契约"] = 0;
            if (actor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)(actor);
                if (pc.Online)
                {
                    Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("『精灵契约』效果消失辣~！");
                }
            }
        }
    }
}
