using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaLib;

namespace SagaMap.Skill.SkillDefinations
{
    public partial class S21115 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //获取锁定目标
            dActor = SkillHandler.Instance.GetdActor(sActor, args);
            if (dActor == null) return;

            //计算攻击次数
            byte Count = 1;
            //TODO:移除DEBUFF、统计次数
            if (level == 1 && Count > 2) Count = 2;
            if (level == 2 && Count > 4) Count = 4;

            //造成伤害
            List<Actor> target = new List<Actor>();
            float factor = 1.5f + 0.3f * level;
            for (int i = 0; i < Count; i++)
                target.Add(dActor);
            SkillHandler.Instance.PhysicalAttack(sActor, target, args, SkillHandler.DefType.Def, Elements.Dark, 0, factor, true);

            //延长瘴气兵装时间
            if (sActor.Status.Additions.ContainsKey("P_瘴气兵装"))
            {
                int seconds = Count * 1000;
                ((OtherAddition)sActor.Status.Additions["P_瘴气兵装"]).endTime += new TimeSpan(0, 0, 0, 0, seconds);
            }
        }
    }
}
