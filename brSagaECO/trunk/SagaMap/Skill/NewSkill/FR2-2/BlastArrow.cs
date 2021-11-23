using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaMap.Network.Client;
namespace SagaMap.Skill.SkillDefinations.Striker
{
    /// <summary>
    /// 暴風之箭（ブラストアロー）
    /// </summary>
    public class BlastArrow : ISkill 
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //DefaultBuff 射程 = new DefaultBuff(args.skill, sActor, "狙击模式射程", 20000);
            //SagaMap.Skill.SkillHandler.ApplyAddition(sActor, 射程);

            //SnipeMode 狙击 = new SnipeMode(args.skill, sActor, 20000);
            //SagaMap.Skill.SkillHandler.ApplyAddition(sActor, 狙击);

            //if (sActor.type == ActorType.PC)
            //    MapClient.FromActorPC((ActorPC)sActor).SendSystemMessage("狙击模式启动！");
        }
        #endregion
    }
}
