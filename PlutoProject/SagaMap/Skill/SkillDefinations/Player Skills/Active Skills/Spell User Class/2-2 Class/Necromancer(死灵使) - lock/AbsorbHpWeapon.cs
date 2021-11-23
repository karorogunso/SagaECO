
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Necromancer
{
    /// <summary>
    /// 噬血（ライフテイク）
    /// </summary>
    public class AbsorbHpWeapon : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(dActor.MapID);
            if (sActor.ActorID == dActor.ActorID)
            {
                EffectArg arg2 = new EffectArg();
                arg2.effectID = 5238;
                arg2.actorID = dActor.ActorID;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg2, dActor, true);
            }
            BloodLeech skill = new BloodLeech(args.skill, dActor, 50000, 0.1f * level);
            if(sActor.Status.Additions.ContainsKey("SpLeech"))
            {
                Additions.Global.SpLeech spadd = (Additions.Global.SpLeech)sActor.Status.Additions["SpLeech"];
                spadd.rate = 0;
            }
            
            SkillHandler.ApplyAddition(dActor, skill);
        }
        #endregion
    }
}