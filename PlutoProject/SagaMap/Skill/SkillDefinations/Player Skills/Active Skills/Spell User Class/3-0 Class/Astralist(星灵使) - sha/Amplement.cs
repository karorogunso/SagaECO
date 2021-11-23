using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaLib;

namespace SagaMap.Skill.SkillDefinations.Astralist
{
    public class Amplement : ISkill
    {


        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //超广域范围
            List<Actor> affected = map.GetActorsArea(sActor, 1200, true, false);
            List<Actor> realAffected = new List<Actor>();
            ActorPC sPC = (ActorPC)sActor;
            if (sPC.Party != null)
            {
                foreach (Actor act in affected)
                {
                    if (act.type == ActorType.PC)
                    {
                        ActorPC aPC = (ActorPC)act;
                        if (aPC.Party != null && sPC.Party != null)
                        {
                            if ((aPC.Party.ID == sPC.Party.ID) && aPC.Party.ID != 0 && !aPC.Buff.Dead && aPC.PossessionTarget == 0)
                            {
                                if (aPC.Party.ID == sPC.Party.ID)
                                {
                                    realAffected.Add(act);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                realAffected.Add(sActor);
            }
            args.affectedActors = realAffected;
            args.Init();
            int[] lifetimes = { 0, 60000, 100000, 140000, 180000, 220000 };
            int lifetime = lifetimes[level];
            foreach (Actor rAct in realAffected)
            {
                if (rAct.Status.Additions.ContainsKey("Amplement"))
                {
                    rAct.Status.Additions["Amplement"].AdditionEnd();
                    SkillHandler.RemoveAddition(rAct, "Amplement");
                    //
                }
                DefaultBuff skill = new DefaultBuff(args.skill, rAct, "Amplement", lifetime);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(rAct, skill);
            }
        }
        Elements AtkEle, DefEle;
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int[] el_add = { 0, 12, 14, 17, 21, 26 };
            int EarthAttackNow = 0, fireAttackNow = 0, WaterAttackNow = 0, WindAttackNow = 0,
                EarthAttack = 0, FireAttack = 0, WaterAttack = 0, WindAttack = 0,
            EarthNow = 0, FireNow = 0, WaterNow = 0, WindNow = 0,
            Earth = 0, Fire = 0, Water = 0, Wind = 0;
            //地属性攻击判定
            EarthAttackNow = actor.Status.attackElements_item[SagaLib.Elements.Earth]
                + actor.Status.attackElements_skill[SagaLib.Elements.Earth]
                + actor.Status.attackelements_iris[SagaLib.Elements.Earth];
            WaterAttackNow = actor.Status.attackElements_item[SagaLib.Elements.Water]
                + actor.Status.attackElements_skill[SagaLib.Elements.Water]
                + actor.Status.attackelements_iris[SagaLib.Elements.Water];
            //火属性攻击判定
            fireAttackNow = actor.Status.attackElements_item[SagaLib.Elements.Fire]
                + actor.Status.attackElements_skill[SagaLib.Elements.Fire]
                + actor.Status.attackelements_iris[SagaLib.Elements.Fire];
            //风属性攻击判定
            WindAttackNow = actor.Status.attackElements_item[SagaLib.Elements.Wind]
                + actor.Status.attackElements_skill[SagaLib.Elements.Wind]
                + actor.Status.attackelements_iris[SagaLib.Elements.Wind];
            if(EarthAttackNow>0)
            {
                EarthAttack = el_add[skill.skill.Level];
            }
            if (WaterAttackNow > 0)
            {
                WaterAttack = el_add[skill.skill.Level];
            }
            if (fireAttackNow > 0)
            {
                FireAttack = el_add[skill.skill.Level];
            }
            if (WindAttackNow > 0)
            {
                WindAttack = el_add[skill.skill.Level];
            }
            if (skill.Variable.ContainsKey("AmplementAttackEarth"))
                skill.Variable.Remove("AmplementAttackEarth");
            skill.Variable.Add("AmplementAttackEarth", EarthAttack);
            actor.Status.attackElements_skill[Elements.Earth] += EarthAttack;
            if (skill.Variable.ContainsKey("AmplementAttackWater"))
                skill.Variable.Remove("AmplementAttackWater");
            skill.Variable.Add("AmplementAttackWater", WaterAttack);
            actor.Status.attackElements_skill[Elements.Water] += WaterAttack;
            if (skill.Variable.ContainsKey("AmplementAttackFire"))
                skill.Variable.Remove("AmplementAttackFire");
            skill.Variable.Add("AmplementAttackFire", FireAttack);
            actor.Status.attackElements_skill[Elements.Fire] += FireAttack;
            if (skill.Variable.ContainsKey("AmplementAttackWind"))
                skill.Variable.Remove("AmplementAttackWind");
            skill.Variable.Add("AmplementAttackWind", WindAttack);
            actor.Status.attackElements_skill[Elements.Wind] += WindAttack;

            //地属性判定
            EarthNow = actor.Status.elements_item[SagaLib.Elements.Earth]
                + actor.Status.elements_skill[SagaLib.Elements.Earth]
                + actor.Status.elements_iris[SagaLib.Elements.Earth];
            WaterNow = actor.Status.elements_item[SagaLib.Elements.Water]
                + actor.Status.elements_skill[SagaLib.Elements.Water]
                + actor.Status.elements_iris[SagaLib.Elements.Water];
            //火属性判定
            FireNow = actor.Status.elements_item[SagaLib.Elements.Fire]
                + actor.Status.elements_skill[SagaLib.Elements.Fire]
                + actor.Status.elements_iris[SagaLib.Elements.Fire];
            //风属性判定
            WindNow = actor.Status.elements_item[SagaLib.Elements.Wind]
                + actor.Status.elements_skill[SagaLib.Elements.Wind]
                + actor.Status.elements_iris[SagaLib.Elements.Wind];
            if (EarthNow > 0)
            {
                Earth = el_add[skill.skill.Level];
            }
            if (WaterNow > 0)
            {
                Water = el_add[skill.skill.Level];
            }
            if (FireNow > 0)
            {
                Fire = el_add[skill.skill.Level];
            }
            if (WindNow > 0)
            {
                Wind = el_add[skill.skill.Level];
            }

            if (skill.Variable.ContainsKey("AmplementEarth"))
                skill.Variable.Remove("AmplementEarth");
            skill.Variable.Add("AmplementEarth", Earth);
            actor.Status.elements_skill[Elements.Earth] += Earth;
            if (skill.Variable.ContainsKey("AmplementWater"))
                skill.Variable.Remove("AmplementWater");
            skill.Variable.Add("AmplementWater", Water);
            actor.Status.elements_skill[Elements.Water] += Water;
            if (skill.Variable.ContainsKey("AmplementFire"))
                skill.Variable.Remove("AmplementFire");
            skill.Variable.Add("AmplementFire", Fire);
            actor.Status.elements_skill[Elements.Fire] += Fire;
            if (skill.Variable.ContainsKey("AmplementWind"))
                skill.Variable.Remove("AmplementWind");
            skill.Variable.Add("AmplementWind", Wind);
            actor.Status.elements_skill[Elements.Wind] += Wind;


            actor.Buff.三转四属性赋予アンプリエレメント = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            
            actor.Status.attackElements_skill[Elements.Earth] -= (int)skill.Variable["AmplementAttackEarth"];
            actor.Status.attackElements_skill[Elements.Water] -= (int)skill.Variable["AmplementAttackWater"];
            actor.Status.attackElements_skill[Elements.Fire] -= (int)skill.Variable["AmplementAttackFire"];
            actor.Status.attackElements_skill[Elements.Wind] -= (int)skill.Variable["AmplementAttackWind"];
            actor.Status.elements_skill[Elements.Earth] -= (int)skill.Variable["AmplementEarth"];
            actor.Status.elements_skill[Elements.Water] -= (int)skill.Variable["AmplementWater"];
            actor.Status.elements_skill[Elements.Fire] -= (int)skill.Variable["AmplementFire"];
            actor.Status.elements_skill[Elements.Wind] -= (int)skill.Variable["AmplementWind"];
            actor.Buff.三转四属性赋予アンプリエレメント = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}
