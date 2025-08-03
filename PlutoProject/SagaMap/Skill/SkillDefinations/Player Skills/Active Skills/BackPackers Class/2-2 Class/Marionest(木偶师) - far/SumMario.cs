using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations.Marionest
{
    /// <summary>
    /// 召喚活動木偶皇帝
    /// </summary>
    public class SumMario : ISkill
    {
        private uint MobID;
        private uint NextSkillID;
        public SumMario(uint MobID, uint NextSkillID)
        {
            this.MobID = MobID;
            this.NextSkillID = NextSkillID;
        }
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //招換
            ActorMob mob = map.SpawnMob(MobID, (short)(sActor.X + SagaLib.Global.Random.Next(1, 10))
                                             , (short)(sActor.Y + SagaLib.Global.Random.Next(1, 10))
                                             , 50, sActor);
            sActor.Slave.Add(mob);
            args.autoCast.Add(SkillHandler.Instance.CreateAutoCastInfo(NextSkillID, level, 500));

        }
        #endregion
    }
}



