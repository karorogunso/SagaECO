using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaLib;


namespace SagaMap.Skill.SkillDefinations
{
    public class S13106 : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("生命绽放CD"))
                return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            OtherAddition cd = new OtherAddition(null, sActor, "生命绽放CD", 1000);
            SkillHandler.ApplyBuffAutoRenew(sActor, cd);
            float factor = 3f + 1f * level;
            float factorheal = 0.9f + 0.4f * level;
            factorheal += factorheal * (sActor.BeliefLight / 5000f);
            factor += (1.5f + 0.5f * level) * (sActor.BeliefLight / 5000f);
            if (!sActor.Status.Additions.ContainsKey("意志坚定"))
                sActor.EP += 300;
            Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);

            short range = 350;
            if (sActor.EP >= 8000)
                factor *= 1.5f;
            SkillHandler.Instance.MagicAttack(sActor, dActor, args, Elements.Holy, factor);

            if (sActor.type != ActorType.PC) return;

            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(dActor, range, false);
            foreach (var item in actors)
            {
                if(item.type ==  ActorType.PC)
                {
                    ActorPC p = (ActorPC)item;
                    if(p.Mode == ((ActorPC)sActor).Mode && p.HP > 0 && !p.Buff.Dead)
                    {
                        int heal = SkillHandler.Instance.CalcDamage(false, sActor, item, args, SkillHandler.DefType.IgnoreAll, Elements.Holy, 50, factorheal);
                        item.HP += (uint)heal;
                        if (item.HP > item.MaxHP)
                            item.HP = item.MaxHP;
                        SkillHandler.Instance.ShowVessel(item, -heal);
                        SkillHandler.Instance.ShowEffect(map, p, 5126);
                    }
                }
            }
        }
        #endregion
    }
}
