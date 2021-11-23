using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations.Merchant
{
    /// <summary>
    /// 死角地帶（ライオットハンマー）
    /// </summary>
    public class SunSofbley : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            ActorPC sActorPC=(ActorPC)sActor;
            if(sActorPC.Party==null)
            {
                return;
            }
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //算敵人
            List<Actor> affected = map.GetActorsArea(sActor, 150, false);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                {
                    realAffected.Add(act);
                }
            }
            //算隊員
            int bonus=0;
            List<Actor> acts = map.GetActorsArea(sActor, 350, false);
            foreach (Actor m in acts)
            {
                if (m.type == ActorType.PC)
                {
                    ActorPC mPC = (ActorPC)m;
                    if (mPC.Party != null)
                    {
                        if (mPC.Party.ID == sActorPC.Party.ID)
                        {
                            //floor[Hp/15]+floor[Mp/5]+floor[Sp/5]
                            bonus += (int)(Math.Floor(mPC.HP / 15f) + Math.Floor(mPC.MP / 5f) + Math.Floor(mPC.SP / 5f));
                        }
                    }
                }
            }
            float[] factor={0f,0.166f,0.2f,0.25f,0.33f,0.5f};
            int Damge = (int)(sActor.HP * factor[level]+bonus );
            //需有一個傷害直接指定數字給予的攻擊函數
            SkillHandler.Instance.MagicAttack(sActor, realAffected, args, SkillHandler.DefType.Def, Elements.Neutral, Damge, 0, true);
        }
        #endregion
    }
}