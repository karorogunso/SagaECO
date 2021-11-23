
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Necromancer
{
    /// <summary>
    /// 燃燒的靈魂（ソウルバーン）
    /// </summary>
    public class AtkMp : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            float hp = 0.07f * args.skill.Level;
            if (sActor.HP > sActor.MaxHP * hp)
            {
                SkillHandler.Instance.Heal(sActor, -(int)(sActor.MaxHP * hp), 0, 0);
                return 0;
            }
            return -21;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float hp = 0.07f * level;
            float[] add = { 0, 0.04f, 0.06f, 0.09f, 0.13f, 0.18f };
            //sActor.HP -= (uint)(sActor.MaxHP * hp);
            SkillHandler.Instance.Heal(sActor, 0, (int)(sActor.MaxMP * add[level]), (int)(sActor.MaxSP * add[level]));
            Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);

        }
        #endregion
    }
}