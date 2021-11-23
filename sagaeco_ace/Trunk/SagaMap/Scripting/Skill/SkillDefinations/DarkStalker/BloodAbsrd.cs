using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.DarkStalker
{
    /// <summary>
    /// 血液吸收（ブラッドアブソーブ）
    /// </summary>
    public class BloodAbsrd : ISkill 
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0;
            args.type = ATTACK_TYPE.BLOW;
            factor = 1.0f + 0.2f * level;
            List<Actor> actors = Manager.MapManager.Instance.GetMap(dActor.MapID).GetActorsArea(dActor, 100, true);
            List<Actor> affected = new List<Actor>();
            //取得有效Actor（即怪物）
            foreach (Actor i in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                {
                    affected.Add(i);
                }
            }
            SkillHandler.Instance.MagicAttack(sActor, affected, args, SagaLib.Elements.Neutral, factor);
            int hp_recovery = 0;
            foreach (int hp in args.hp)
            {
                hp_recovery += hp;
            }
            hp_recovery = (int)Math.Floor(hp_recovery * 0.8f);
            SkillHandler.Instance.FixAttack(sActor, sActor, args, SagaLib.Elements.Holy, -hp_recovery);
        }
        #endregion
    }
}
