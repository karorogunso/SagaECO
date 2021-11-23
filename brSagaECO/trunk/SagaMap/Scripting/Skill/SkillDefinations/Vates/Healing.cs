using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Vates
{
    public class Healing:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            //if (pc.Status.Additions.ContainsKey("HealingCD"))
            //{
                //Network.Client.MapClient.FromActorPC(pc).SendSystemMessage(string.Format("该技能正在单独冷却中，剩余时间：{0}毫秒", pc.Status.Additions["HealingCD"].RestLifeTime));
                //return -99;
            //}
            if(pc.Status.Additions.ContainsKey("Spell"))
            {
                return -7;
            }
            if (dActor.type == ActorType.MOB)
            {
                ActorEventHandlers.MobEventHandler eh = (ActorEventHandlers.MobEventHandler)dActor.e;
                if (eh.AI.Mode.Symbol)
                    return -14;
            }
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //DefaultBuff skill = new DefaultBuff(args.skill, sActor, "HealingCD", 3000);
            //SkillHandler.ApplyAddition(sActor, skill);

            uint hpadd = (uint)(dActor.MaxHP * (float)(0.07f + 0.01f * level));
            dActor.HP += hpadd;
            if (dActor.HP > dActor.MaxHP)
            {
                dActor.HP = dActor.MaxHP;
            }
            Map map = Manager.MapManager.Instance.GetMap(dActor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, dActor, true);
            List<Actor> actors = new List<Actor>();
            actors.Add(dActor);
            List<SagaLib.AttackFlag> aty = new List<SagaLib.AttackFlag>();
            aty.Add(SagaLib.AttackFlag.HP_HEAL);
            List<int> hpp = new List<int>();
            hpp.Add((int)-hpadd);
            List<int> mpp = new List<int>();
            mpp.Add(0);
            Packets.Server.SSMG_ITEM_ACTIVE_SELF p3 = new SagaMap.Packets.Server.SSMG_ITEM_ACTIVE_SELF(1);
            p3.ActorID = sActor.ActorID;
            p3.AffectedID = actors;
            p3.AttackFlag(aty);
            p3.ItemID = 10000000;
            p3.SetHP(hpp);
            p3.SetMP(mpp);
            p3.SetSP(mpp);
            if(sActor.type == ActorType.PC)
            SagaMap.Network.Client.MapClient.FromActorPC((ActorPC)sActor).netIO.SendPacket(p3);
            //map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, arg, dActor, true);

            float factor = 0.5f + 0.1f * level;
            SkillHandler.Instance.MagicAttack(sActor, dActor, args, SkillHandler.DefType.IgnoreAll, SagaLib.Elements.Holy, -factor);

        }
        #endregion
    }
}
