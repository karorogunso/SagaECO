using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Cardinal
{
    class CoronaHit : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("CoronaHitCD"))
            {
                Network.Client.MapClient.FromActorPC(pc).SendSystemMessage(string.Format("该技能正在单独冷却中，剩余时间：{0}毫秒", pc.Status.Additions["CoronaHitCD"].RestLifeTime));
                return -99;
            }
            else
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            SkillCD cd = new SkillCD(args.skill, sActor, "CoronaHitCD", 120000);
            SkillHandler.ApplyAddition(sActor, cd);
            DefaultBuff CoronaHit = new DefaultBuff(args.skill, dActor, "CoronaHit", 30);
            CoronaHit.OnAdditionStart += Start;
            CoronaHit.OnAdditionEnd += End;
            SkillHandler.ApplyAddition(dActor, CoronaHit);
            Network.Client.MapClient.FromActorPC((ActorPC)dActor).SendSystemMessage(string.Format("神圣加护状态进入，来源：{0}", sActor.Name));
        }
        void Start(Actor actor, DefaultBuff buff)
        {
            ushort stradd = ((ActorPC)actor).Str;
            ushort vitadd = ((ActorPC)actor).Vit;
            ushort magadd = ((ActorPC)actor).Mag;

            if (buff.Variable.ContainsKey("stradd")) 
            buff.Variable.Remove("stradd");
            buff.Variable.Add("stradd", stradd);
            ((ActorPC)actor).Status.str_skill += (short)stradd;

            if (buff.Variable.ContainsKey("vitadd"))
                buff.Variable.Remove("vitadd");
            buff.Variable.Add("vitadd",vitadd);
            ((ActorPC)actor).Status.vit_skill += (short)vitadd;

            if (buff.Variable.ContainsKey("magadd"))
                buff.Variable.Remove("magadd");
            buff.Variable.Add("magadd", magadd);
            ((ActorPC)actor).Status.mag_skill += (short)magadd;

            
        }
        void End(Actor actor, DefaultBuff buff)
        {
            ((ActorPC)actor).Status.str_skill -= (short)buff.Variable["stradd"];
            ((ActorPC)actor).Status.vit_skill -= (short)buff.Variable["vitadd"];
            ((ActorPC)actor).Status.mag_skill -= (short)buff.Variable["magadd"];

            Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("神圣加护状态消失");
        }
        #endregion
    }
}
