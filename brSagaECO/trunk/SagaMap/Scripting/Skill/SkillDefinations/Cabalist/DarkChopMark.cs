
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Cabalist
{
    /// <summary>
    /// 黑暗苦痛（ダークペイン）
    /// </summary>
    public class DarkChopMark : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            uint NextSkillID=10000;
            uint ChopMark_SkillID=3166;
         
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            var ActorSkills = from Actor act in map.Actors
                              where act.type == ActorType.SKILL
                              select act;
            foreach (Actor act in ActorSkills)
            {
                ActorSkill actor = (ActorSkill)act;
                if (actor.Skill.ID == ChopMark_SkillID)
                {
                    args.autoCast.Add(SkillHandler.Instance.CreateAutoCastInfo(NextSkillID, level, 0,SagaLib.Global.PosX16to8(actor.X,map.Width),SagaLib.Global.PosY16to8(actor.Y,map.Height)));
                }
            }
        }
        #endregion
    }
}