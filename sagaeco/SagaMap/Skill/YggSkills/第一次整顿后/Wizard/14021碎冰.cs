using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    /// <summary>
    /// 寒冰湍流：7×7水属性设置多段魔法攻击，附带颤栗
    /// </summary>
    public class S14021 : ISkill
    {

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                pc.TInt["水属性魔法释放"] = 1;
                if (pc.TInt["火属性魔法释放"] == 1)
                    pc.TInt["水属性魔法释放"] = 2;
            }

            float factor = 2f + 4f * level;
            byte maxCount = 2;
            if (level >= 3) maxCount = 3;

            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(dActor, 250, false);
            List<Actor> targets = new List<Actor>() { dActor };
            foreach (var item in actors)
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item) && targets.Count < maxCount)
                    targets.Add(item);
                    
            if(dActor.Status.Additions.ContainsKey("Frosen"))
            {
                SkillHandler.RemoveAddition(dActor, "Frosen");
                factor *= 2;
            }
            if (dActor.Status.Additions.ContainsKey("空间震") && sActor.type == ActorType.PC)
            {
                ActorPC Me = sActor as ActorPC;
                if (Me.Skills.ContainsKey(14007))
                {
                    byte lv = Me.Skills[14007].Level;
                    float fup = 1.25f + lv * 0.05f;
                    factor *= fup;
                    SkillHandler.RemoveAddition(dActor, "空间震");
                    SkillHandler.Instance.ShowEffectOnActor(dActor, 5266, sActor);
                }
            }
            SkillHandler.Instance.MagicAttack(sActor, targets, args, Elements.Water, factor);
        }
    }
}
