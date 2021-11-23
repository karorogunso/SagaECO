
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.BladeMaster
{
    /// <summary>
    /// 流水功勢（流水の構え）
    /// </summary>
    public class SwordEaseSp : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 20000;
            DefaultBuff skill = new DefaultBuff(args.skill, sActor , "SwordEaseSp", lifetime);
            SkillHandler.ApplyAddition(sActor, skill);

            EPdown ep = new EPdown(args, sActor,"流水", 1000);
            SkillHandler.ApplyAddition(sActor, ep);
        }

        class EPdown : DefaultBuff
        {
           public EPdown(SkillArg arg, Actor actor, string name,int lifetime)
                : base(arg.skill, actor, name, lifetime, 1000)
            {
                this.OnUpdate += this.TimerUpdate;
            }
            void TimerUpdate(Actor sActor, DefaultBuff skill)
            {
                if (sActor.EP > 2)
                    sActor.EP -= 2;
                else
                    sActor.EP = 0;
                if(sActor.type == ActorType.PC)
                {
                    SagaMap.Network.Client.MapClient.FromActorPC((ActorPC)sActor).SendActorHPMPSP(sActor);
                }
            }
        }
        
        #endregion
    }
}
