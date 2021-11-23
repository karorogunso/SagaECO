using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.SunFlowerAdditions
{
    /// <summary>
    /// 高级治愈（Ragnarok）
    /// </summary>
    public class HighHeal : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //float factor = -35.8f;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 500, false);//实测7*7范围内怪物互补情况太差,更改为11*11
            List<Actor> realAffected = new List<Actor>();
            Actor ActorlowHP = sActor;
            foreach (Actor act in affected)
            {
                if (!SkillHandler.Instance.CheckValidAttackTarget(sActor, act) && act.type != ActorType.SKILL)
                {
                    if ((float)(act.HP / act.MaxHP) < (float)(ActorlowHP.HP / ActorlowHP.MaxHP))
                        ActorlowHP = act;
                }
            }
            float rank = 0.05f;
            uint heal = (uint)(ActorlowHP.MaxHP * rank);
            uint realheal = Math.Max(5000, heal);
            if (ActorlowHP.Buff.NoRegen)
                return;
            ActorlowHP.HP += realheal;
            if (ActorlowHP.HP > ActorlowHP.MaxHP)
                ActorlowHP.HP = ActorlowHP.MaxHP;
            args.affectedActors.Add(ActorlowHP);
            args.Init();
            if (args.flag.Count > 0)
            {
                args.flag[0] |= SagaLib.AttackFlag.HP_HEAL | SagaLib.AttackFlag.NO_DAMAGE;
                args.hp[0] = ((int)realheal);
            }
            //map = Manager.MapManager.Instance.GetMap(ActorlowHP.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, ActorlowHP, true);


            //SkillHandler.Instance.FixAttack(sActor, ActorlowHP, args, SagaLib.Elements.Holy, factor);

        }
        #endregion
    }
}
