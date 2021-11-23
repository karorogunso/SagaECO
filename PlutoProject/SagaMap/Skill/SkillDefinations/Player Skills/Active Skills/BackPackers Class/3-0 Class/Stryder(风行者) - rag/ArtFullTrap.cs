using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaDB.Item;

namespace SagaMap.Skill.SkillDefinations.Stryder
{
    /// <summary>
    /// アートフルトラップ
    /// </summary>
    public class ArtFullTrap : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {


            int lifetime = 300000;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(dActor, 200, false);
            affected.Add(dActor);
            foreach (Actor act in affected)
            {
                if (act.type == ActorType.PC || act.type == ActorType.PARTNER)
                {
                    SkillHandler.RemoveAddition(act, "ArtFullTrap1");
                    SkillHandler.RemoveAddition(act, "ArtFullTrap2");
                    SkillHandler.RemoveAddition(act, "ArtFullTrap3");
                    SkillHandler.RemoveAddition(act, "ArtFullTrap4");
                    SkillHandler.RemoveAddition(act, "ArtFullTrap5");
                    switch (level)
                    {
                        case 1:
                            DefaultBuff skill = new DefaultBuff(args.skill, act, "ArtFullTrap1", lifetime);
                            skill.OnAdditionStart += this.StartEventHandler;
                            skill.OnAdditionEnd += this.EndEventHandler;
                            SkillHandler.ApplyAddition(act, skill);
                            break;
                        case 2:
                            DefaultBuff skill2 = new DefaultBuff(args.skill, act, "ArtFullTrap2", lifetime);
                            skill2.OnAdditionStart += this.StartEventHandler;
                            skill2.OnAdditionEnd += this.EndEventHandler;
                            SkillHandler.ApplyAddition(act, skill2);
                            break;
                        case 3:
                            DefaultBuff skill3 = new DefaultBuff(args.skill, act, "ArtFullTrap3", lifetime);
                            skill3.OnAdditionStart += this.StartEventHandler;
                            skill3.OnAdditionEnd += this.EndEventHandler;
                            SkillHandler.ApplyAddition(act, skill3);
                            break;
                        case 4:
                            DefaultBuff skill4 = new DefaultBuff(args.skill, act, "ArtFullTrap4", lifetime);
                            skill4.OnAdditionStart += this.StartEventHandler;
                            skill4.OnAdditionEnd += this.EndEventHandler;
                            SkillHandler.ApplyAddition(act, skill4);
                            break;
                        case 5:
                            DefaultBuff skill5 = new DefaultBuff(args.skill, act, "ArtFullTrap5", lifetime);
                            skill5.OnAdditionStart += this.StartEventHandler;
                            skill5.OnAdditionEnd += this.EndEventHandler;
                            SkillHandler.ApplyAddition(act, skill5);
                            break;
                    }
                }
            }



        }



        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Buff.アートフルトラップ = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Buff.アートフルトラップ = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        #endregion
    }
}
