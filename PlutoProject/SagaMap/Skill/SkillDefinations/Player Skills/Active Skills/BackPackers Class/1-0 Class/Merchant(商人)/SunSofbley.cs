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
            int bonus = 0;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> acts = map.GetActorsArea(sActor, 300, false);
            foreach (Actor m in acts)
            {
                if (m.type == ActorType.PC)
                {
                    ActorPC mPC = (ActorPC)m;
                    if (mPC.Party != null)
                    {
                        if (mPC.Party.ID == sActor.Party.ID)
                        {
                            return 0;
                        }
                    }
                }
            }
            return -12;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            ActorPC sActorPC = (ActorPC)sActor;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);

            //算隊員
            int bonus = 0, hp = 0, mp = 0, sp = 0;
            List<Actor> acts = map.GetActorsArea(sActor, 300, false);
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
                            //bonus += (int)(Math.Floor(mPC.HP / 15f) + Math.Floor(mPC.MP / 5f) + Math.Floor(mPC.SP / 5f));
                            hp += (int)mPC.HP;
                            mp += (int)mPC.MP;
                            sp += (int)mPC.SP;
                        }
                    }
                }
            }
            bonus = (int)(Math.Floor(hp / 15.0f) + Math.Floor(mp / 5.0f) + Math.Floor(sp / 5.0f));
            float[] factor = { 0f, 0.166f, 0.2f, 0.25f, 0.33f, 0.5f };
            int Damge = (int)(sActor.HP * factor[level] + bonus);
            //算敵人
            List<Actor> affected = map.GetActorsArea(dActor, 150, false);
            List<Actor> realAffected = new List<Actor>();
            realAffected.Add(dActor);
            //SkillHandler.Instance.FixAttack(sActor, dActor, args, SagaLib.Elements.Neutral, Damge);
            foreach (Actor act in affected)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                {
                    realAffected.Add(act);
                    //SkillHandler.Instance.FixAttack(sActor, act, args, SagaLib.Elements.Neutral, Damge);
                }
            }
            //需有一個傷害直接指定數字給予的攻擊函數
            SkillHandler.Instance.MagicAttack(sActor, realAffected, args, SkillHandler.DefType.Def, Elements.Neutral, Damge, 0, true);
        }
        #endregion
    }
}