using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.C1skill
{
    public class HotBlade : ISkill
    {
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //创建一个默认被动技能处理对象
            DefaultPassiveSkill skill2 = new DefaultPassiveSkill(args.skill, sActor, "HotBlade", true);
            skill2.OnAdditionStart += this.StartEventHandler;
            skill2.OnAdditionEnd += this.EndEventHandler;
            //对指定Actor附加技能效果
            SkillHandler.ApplyAddition(sActor, skill2);
        }
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        { }
        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
        }
    }
}
