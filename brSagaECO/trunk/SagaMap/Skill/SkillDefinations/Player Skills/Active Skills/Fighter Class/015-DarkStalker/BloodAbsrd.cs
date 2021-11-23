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
            float[] absfactor = new float[] { 1.0f, 0.14f, 0.17f, 0.21f, 0.25f, 0.30f };
            hp_recovery = (int)Math.Floor(hp_recovery * absfactor[level]);

            //sActor.HP += (uint)hp_recovery;
            //if (sActor.HP > sActor.MaxHP)
            //    sActor.HP = sActor.MaxHP;
            //args.affectedActors.Add(sActor);
            //args.Init();
            //if (args.flag.Count > 0)
            //{
            //    args.flag[0] |= SagaLib.AttackFlag.HP_HEAL | SagaLib.AttackFlag.NO_DAMAGE;
            //    args.hp[0] = hp_recovery;
            //}
            //Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);

            SkillHandler.Instance.FixAttack(sActor, sActor, args, SagaLib.Elements.Holy, -hp_recovery);
        }
        #endregion
    }
}
