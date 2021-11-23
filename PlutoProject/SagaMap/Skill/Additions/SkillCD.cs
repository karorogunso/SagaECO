using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Actor;

using SagaMap.Tasks;
using SagaLib;

namespace SagaMap.Skill.Additions.Global
{
    public class SkillCD : Addition
    {
        DateTime endTime;
        int lifeTime;
        int period;
        public SagaDB.Skill.Skill skill;
        public delegate void StartEventHandler(Actor actor, SkillCD skill);
        public delegate void EndEventHandler(Actor actor, SkillCD skill);
        public delegate void UpdateEventHandler(Actor actor, SkillCD skill);
        public delegate void ValidCheckEventHandler(ActorPC sActor, Actor dActor, out int result);

        public Dictionary<string, int> Variable = new Dictionary<string, int>();
        public event StartEventHandler OnAdditionStart;
        public event EndEventHandler OnAdditionEnd;
        public event UpdateEventHandler OnUpdate;
        public ValidCheckEventHandler OnCheckValid;


        public SkillCD(SagaDB.Skill.Skill skill, Actor actor, string name, int lifetime)
            : this(skill, actor, name, lifetime, lifetime)
        {

        }

        public SkillCD(SagaDB.Skill.Skill skill, Actor actor, string name, int lifetime, int period)
        {
            this.Name = name;
            this.skill = skill;
            this.AttachedActor = actor;
            this.lifeTime = lifetime;
            this.period = period;
        }

        public int this[string name]
        {
            get
            {
                bool blocked = ClientManager.Blocked;
                if (!blocked)
                    ClientManager.EnterCriticalArea();
                int value;
                if (Variable.ContainsKey(name))
                    value = Variable[name];
                else
                    value = 0;
                if (!blocked)
                    ClientManager.LeaveCriticalArea();
                return value;
            }
            set
            {
                bool blocked = ClientManager.Blocked;
                if (!blocked)
                    ClientManager.EnterCriticalArea();

                if (this.Variable.ContainsKey(name))
                    this.Variable.Remove(name);
                this.Variable.Add(name, value);

                if (!blocked)
                    ClientManager.LeaveCriticalArea();
            }
        }

        public override int RestLifeTime
        {
            get
            {
                return (int)(this.endTime - DateTime.Now).TotalMilliseconds;
            }
        }

        public override int TotalLifeTime
        {
            get
            {
                return lifeTime;
            }
            set
            {
                int delta = value - lifeTime;
                lifeTime = value;
                TimeSpan span = new TimeSpan(0, 0, 0, 0, delta);
                this.endTime += span;
            }
        }

        public override void AdditionEnd()
        {
            SkillHandler.RemoveAddition(this.AttachedActor, this, true);
            TimerEnd();
            if (OnAdditionEnd != null && this.AttachedActor.Status != null)
                OnAdditionEnd.Invoke(this.AttachedActor, this);

            if (this.AttachedActor.type == ActorType.PC && this.AttachedActor.Status != null)
            {
                ActorPC pc = (ActorPC)this.AttachedActor;
                PC.StatusFactory.Instance.CalcStatus(pc);
                Network.Client.MapClient.FromActorPC(pc).SendPlayerInfo();
            }
        }

        public override void AdditionStart()
        {
            if (period != int.MaxValue)
            {
                this.endTime = DateTime.Now + new TimeSpan(0, lifeTime / 60000, (lifeTime / 1000) % 60);
                InitTimer(period, 0);
                TimerStart();
            }
            if (OnAdditionStart != null && this.AttachedActor.Status != null)
                OnAdditionStart.Invoke(this.AttachedActor, this);
            else
            {

            }
            if (this.AttachedActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)this.AttachedActor;
                PC.StatusFactory.Instance.CalcStatus(pc);
                Network.Client.MapClient.FromActorPC(pc).SendPlayerInfo();
            }
        }

        public override void OnTimerUpdate()
        {
            if (OnUpdate != null)
                OnUpdate.Invoke(this.AttachedActor, this);
        }

        public override void OnTimerEnd()
        {
            this.AdditionEnd();
        }
    }
}
