using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaMap.Network.Client;
using SagaDB.Skill;
namespace SagaMap.Skill.SkillDefinations.Event
{
    public class PressionKiller : ISkill
    {
        bool MobUse = false;
        public PressionKiller()
        {
            MobUse = false;
        }
        public PressionKiller(bool mobuse)
        {
            this.MobUse = mobuse;
        }
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (MobUse)
            {
                //SkillFactory.Instance.GetSkill(9126, 1).Effect = 5368;
                
            }
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            SkillHandler.Instance.ActorSpeak(sActor, "闇に惑いし哀れな影よ,人を伤つけ贬めて.罪に溺れし业の魂,一遍、死んで见る?");
            if (!(dActor is ActorPC))
                return;
            float factor = 2.50f;
            List<Actor> affected = new List<Actor>();

            if ((dActor as ActorPC).PossesionedActors.Count != 0)
            {
                foreach (var item in (dActor as ActorPC).PossesionedActors)
                {
                    affected.Add(item);
                }
            }
            factor += affected.Count * 10;
            SkillHandler.Instance.MagicAttack(sActor, new List<Actor>() { dActor }, args, SkillHandler.DefType.IgnoreAll, SagaLib.Elements.Neutral, 0, factor, 0, false, true);
            int dmg = args.hp[0];
            //args.affectedActors.Clear();
            foreach (var item in affected)
            {
                SkillHandler.Instance.ApplyDamage(sActor, item, (int)(dmg * 1.5f));
                Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, item, true);
            }
            if (affected.Count > 0)
                SkillHandler.Instance.ActorSpeak(sActor, "この怨み、地狱へ流します.");
        }
    }
}
