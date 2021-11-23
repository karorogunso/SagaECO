using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;


namespace SagaMap.Skill.SkillDefinations
{
    class S20015 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("神佑：破邪CD"))
                return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lefttime = 20000;
            int CD = 120000;

            //技能CD
            OtherAddition cd = new OtherAddition(null, sActor, "神佑：破邪CD", CD);
            cd.OnAdditionEnd += (s, e) =>
            {
                SkillHandler.SendSystemMessage(dActor, "『神佑：破邪』可以再次使用了。");
            };
            SkillHandler.ApplyBuffAutoRenew(sActor, cd);

            //附加BUFF
            OtherAddition skill = new OtherAddition(null, dActor, "神佑：破邪免疫异常", lefttime);
            skill.OnAdditionStart += (s, e) =>
            {
                SkillHandler.SendSystemMessage(dActor, "受到了『神佑：破邪』效果，你将对部分异常状态免疫");
            };
            skill.OnAdditionEnd += (s, e) =>
            {
                SkillHandler.SendSystemMessage(dActor, "『神佑：破邪』效果消失了");
            };
            SkillHandler.ApplyBuffAutoRenew(dActor, skill);

            //驱散状态
            List<string> ss = new List<string>();
            if (dActor.Status.Additions.ContainsKey("Confuse")) ss.Add("Confuse");
            if (dActor.Status.Additions.ContainsKey("Frosen")) ss.Add("Frosen");
            if (dActor.Status.Additions.ContainsKey("Paralyse")) ss.Add("Paralyse");
            if (dActor.Status.Additions.ContainsKey("Silence")) ss.Add("Silence");
            if (dActor.Status.Additions.ContainsKey("Sleep")) ss.Add("Sleep");
            if (dActor.Status.Additions.ContainsKey("Stone")) ss.Add("Stone");
            if (dActor.Status.Additions.ContainsKey("Stun")) ss.Add("Stun");
            if (dActor.Status.Additions.ContainsKey("鈍足")) ss.Add("鈍足");
            if (dActor.Status.Additions.ContainsKey("冰棍的冻结")) ss.Add("冰棍的冻结");
            for (int i = 0; i < ss.Count; i++)
                SkillHandler.RemoveAddition(dActor, ss[i]);
        }
    }
}