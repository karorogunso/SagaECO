
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations.Alchemist
{
    /// <summary>
    /// 化工廠（ケミカルプラント）
    /// </summary>
    public class SumChemicalPlant : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map=Manager.MapManager.Instance.GetMap(sActor.MapID);
            ActorMob mob = map.SpawnMob(10580004, (short)(sActor.X + SagaLib.Global.Random.Next(1, 10)),(short)( sActor.Y + SagaLib.Global.Random.Next(1, 10)), 2500, sActor);
            sActor.Slave.Add(mob);
            AutoCastInfo aci = new AutoCastInfo();
            aci.skillID = 3344;//化工廠[接續技能]
            aci.level = level;
            aci.delay = 0;
            args.autoCast.Add(aci);
        }
        #endregion

        //#region Timer
        //private class Activator : MultiRunTask
        //{
        //    Actor sActor;
        //    ActorMob mob;
        //    SkillArg skill;
        //    float factor;
        //    Map map;
        //    public Activator(Actor _sActor, ActorMob mob, SkillArg _args, byte level)
        //    {
        //        sActor = _sActor;
        //        this.mob = mob;
        //        skill = _args.Clone();
        //        factor = 0.5f + 1.5f * level;
        //        this.dueTime = 2500;
        //        this.period = 0;
                
        //    }
        //    public override void CallBack()
        //    {
        //        //同步鎖，表示之後的代碼是執行緒安全的，也就是，不允許被第二個執行緒同時訪問
        //        ClientManager.EnterCriticalArea();
        //        try
        //        {
                    
        //            this.Deactivate();
        //            map.DeleteActor(mob);
        //        }
        //        catch (Exception ex)
        //        {
        //            Logger.ShowError(ex);
        //        }
        //        //解開同步鎖
        //        ClientManager.LeaveCriticalArea();
        //    }
        //}
        //#endregion
    }
}



