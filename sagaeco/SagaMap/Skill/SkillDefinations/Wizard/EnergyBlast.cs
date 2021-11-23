using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Wizard
{
    public class EnergyBlast:ISkill
    {
        bool MobUse;
        public EnergyBlast()
        {
            this.MobUse = false;
        }
        public EnergyBlast(bool MobUse)
        {
            this.MobUse = MobUse;
        }
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (MobUse)
            {
                level = 6;
            }
            float factor = 0;
            switch (level)
            {
                case 1:
                    factor = 1.8f;
                    break;
                case 2:
                    factor = 2.4f;
                    break;
                case 3:
                    factor = 3.0f;
                    break;
                case 4:
                    factor = 3.6f;
                    break;
                case 5:
                    factor = 4.2f;
                    break;
                case 6 :
                    factor = 12f;
                    break;
            }
            List<Actor> actors = Manager.MapManager.Instance.GetMap(dActor.MapID).GetActorsArea(dActor, 100, true);
            List<Actor> affected = new List<Actor>();
            //取得有效Actor（即怪物）
            foreach (Actor i in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                    affected.Add(i);
            }
            //factor *= (1f / affected.Count);
            SkillHandler.Instance.MagicAttack(sActor, affected, args, SagaLib.Elements.Neutral, factor);
            if (level == 6)
            {
                SkillHandler.Instance.ShowEffectOnActor(dActor, 5294);
                //SkillHandler.Instance.ShowEffect((ActorPC)sActor, dActor, 5294);
                foreach (Actor i in actors)
                {
                    Skill.Additions.Global.硬直 yz = new 硬直(args.skill, i, 3000);
                    SkillHandler.ApplyAddition(i, yz);
                }
            }
        }

        #endregion
    }
}
