
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Command
{
    /// <summary>
    /// 應援要請（応援要請）
    /// </summary>
    public class SumCommand : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 10000;
            List<Actor> sumMob = new List<Actor>();
            List<uint> sumMobID = new List<uint>();
            switch (level)
            {
                case 1:
                    sumMobID.Add(60023400);
                    break;
                case 2:
                    sumMobID.Add(60091550);
                    break;
                case 3:
                    sumMobID.Add(60023400);
                    sumMobID.Add(60091550);
                    break;
            }
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            foreach (uint i in sumMobID)
            {
                sumMob.Add(map.SpawnMob(i, (short)(sActor.X + SagaLib.Global.Random.Next(-100, 100)), (short)(sActor.Y + SagaLib.Global.Random.Next(-100, 100)), 2500, sActor));
            }
            SumCommandBuff skill = new SumCommandBuff(args.skill, sActor, sumMob, lifetime);
            SkillHandler.ApplyAddition(dActor, skill);
        }
        public class SumCommandBuff : DefaultBuff
        {
            List<Actor> sumMob;
            public SumCommandBuff(SagaDB.Skill.Skill skill, Actor actor, List<Actor> sumMob, int lifetime)
                : base(skill, actor, "SumCommand", lifetime)
            {
                this.OnAdditionStart += this.StartEvent;
                this.OnAdditionEnd += this.EndEvent;
                this.sumMob = sumMob;
            }

            void StartEvent(Actor actor, DefaultBuff skill)
            {
            }
            void EndEvent(Actor actor, DefaultBuff skill)
            {
                Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
                foreach (Actor act in sumMob)
                {
                    act.ClearTaskAddition();
                    map.DeleteActor(act);
                }
            }
        }
        #endregion
    }
}
