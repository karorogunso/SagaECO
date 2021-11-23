using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaLib;
using SagaDB.Item;

namespace SagaMap.Skill.SkillDefinations.Striker
{
    /// <summary>
    /// 復活之箭（ポーションアロー）
    /// </summary>
    public class PotionArrow : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            ActorPC pc = (ActorPC)sActor;
            if (SkillHandler.Instance.CheckValidAttackTarget(sActor, dActor))
            {
                return -14;
            }
            else if (SkillHandler.Instance.CountItem(pc, 10026493) < 2)
            {
                return -2;
            }
            else
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int HP_add = 0;
            int MP_add = 0;
            int SP_add = 0;
            int combo = 0;
            //90+2*BASELv./3 180+4*BASELv./3 270+6*BASELv./3 
            switch (level)
            {
                case 1:
                    HP_add = SagaLib.Global.Random.Next((150 + sActor.Level / 3), 300 + 2 * sActor.Level / 3);
                    MP_add = SagaLib.Global.Random.Next(8, 16);
                    SP_add = SagaLib.Global.Random.Next(8, 16);
                    combo = SagaLib.Global.Random.Next(1, 2);
                    break;
                case 2:
                    HP_add = 600 + 4 * sActor.Level / 3;
                    MP_add = SagaLib.Global.Random.Next(24, 32);
                    SP_add = SagaLib.Global.Random.Next(24, 32);
                    combo = SagaLib.Global.Random.Next(3, 4);
                    break;
                case 3:
                    HP_add = 900 + 6 * sActor.Level / 3;
                    MP_add = SagaLib.Global.Random.Next(40, 48);
                    SP_add = SagaLib.Global.Random.Next(40, 48);
                    combo = SagaLib.Global.Random.Next(5, 6);
                    break;
            }
            if(sActor.type==ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                SkillHandler.Instance.TakeItem(pc, 10026493, (ushort)combo);
            }
            //int[] combo = { 0, 2, 4, 6 };
            List<Actor> target = new List<Actor>();
            for (int i = 0; i < combo; i++)
            {
                SkillArg arg2 = new SkillArg();
                arg2.Init();
                arg2.hp.Add(HP_add);
                arg2.mp.Add(MP_add);
                arg2.sp.Add(SP_add);
                arg2.flag.Add(AttackFlag.HP_HEAL | AttackFlag.SP_HEAL | AttackFlag.MP_HEAL | AttackFlag.NO_DAMAGE);
                dActor.HP += (uint)HP_add;
                dActor.MP += (uint)MP_add;
                dActor.SP += (uint)SP_add;
                if (dActor.HP > dActor.MaxHP)
                    dActor.HP = dActor.MaxHP;
                if (dActor.MP > dActor.MaxMP)
                    dActor.MP = dActor.MaxMP;
                if (dActor.SP > dActor.MaxSP)
                    dActor.SP = dActor.MaxSP;
                SkillHandler.Instance.ShowVessel(dActor, -HP_add, -MP_add, -SP_add);
                Manager.MapManager.Instance.GetMap(dActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, arg2, dActor, true);
                
                //target.Add(dActor);
            }
            
            EffectArg arg = new EffectArg();
            arg.effectID = 5263;
            arg.actorID = dActor.ActorID;
            Manager.MapManager.Instance.GetMap(dActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, dActor, true);
            //SkillHandler.Instance.PhysicalAttack(sActor, target, args, SagaLib.Elements.Holy, 0.0f);


        }
        #endregion
    }
}
