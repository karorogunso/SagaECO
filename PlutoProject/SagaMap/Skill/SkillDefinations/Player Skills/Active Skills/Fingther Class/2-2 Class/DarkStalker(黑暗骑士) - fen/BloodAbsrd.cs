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
            float factor = 2.25f + 0.75f * level;
            List<Actor> actors = Manager.MapManager.Instance.GetMap(dActor.MapID).GetActorsArea(dActor, 100, true);
            List<Actor> affected = new List<Actor>();
            //取得有效Actor（即怪物）
            int dmgheal = 0;
            foreach (Actor i in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                {
                    //affected.Add(i);
                    int dmg = SkillHandler.Instance.CalcDamage(false, sActor, dActor, args, SkillHandler.DefType.Def, SagaLib.Elements.Neutral, 0, factor);
                    //SkillHandler.Instance.FixAttack(sActor, i, args, SagaLib.Elements.Neutral, dmg);
                    SkillHandler.Instance.CauseDamage(sActor, i, dmg);
                    SkillHandler.Instance.ShowVessel(i, dmg);
                    dmgheal -= dmg;
                }
            }
            SkillHandler.Instance.CauseDamage(sActor, sActor, (int)(dmgheal * 0.3f));
            //SkillHandler.Instance.FixAttack(sActor, sActor, args, SagaLib.Elements.Neutral, dmgheal*0.3f);
            SkillHandler.Instance.ShowVessel(sActor, (int)(dmgheal * 0.3f));
            if (sActor.HP > sActor.MaxHP)
                sActor.HP = sActor.MaxHP;
        }
        #endregion
    }
}
