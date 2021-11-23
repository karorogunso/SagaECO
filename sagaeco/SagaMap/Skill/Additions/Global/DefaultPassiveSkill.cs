using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Actor;

using SagaMap.Tasks;

namespace SagaMap.Skill.Additions.Global
{
    public class DefaultPassiveSkill : Addition
    {
        public SagaDB.Skill.Skill skill;
        public delegate void StartEventHandler(Actor actor, DefaultPassiveSkill skill);
        public delegate void EndEventHandler(Actor actor, DefaultPassiveSkill skill);
        public delegate void UpdateEventHandler(Actor actor, DefaultPassiveSkill skill);

        public Dictionary<string, int> Variable = new Dictionary<string, int>();
        public event StartEventHandler OnAdditionStart;
        public event EndEventHandler OnAdditionEnd;
        public event UpdateEventHandler OnUpdate;
        bool activate;
        bool canEnd = false;
        DateTime endTime;

        public int amount;
        int period, lifeTime;

        /// <summary>
        /// Constructor for Addition: Short Sword Mastery
        /// </summary>
        /// <param name="actor">Actor, which this addition get attached to</param>
        public DefaultPassiveSkill(SagaDB.Skill.Skill skill, Actor actor,string name, bool ifActivate)
        {
            this.Name = name;
            this.skill =skill;
            this.AttachedActor = actor;
            this.activate = ifActivate;
        }
        public DefaultPassiveSkill(SagaDB.Skill.Skill skill, Actor actor, string name, bool ifActivate, int amount)
        {
            this.Name = name;
            this.skill = skill;
            this.AttachedActor = actor;
            this.activate = ifActivate;
            this.amount = amount;
        }

        public DefaultPassiveSkill(SagaDB.Skill.Skill skill, Actor actor, string name, bool ifActivate, int period, int lifetime)
        {
            this.Name = name;
            this.skill = skill;
            this.AttachedActor = actor;
            this.activate = ifActivate;
            this.period = period;
            this.lifeTime = lifetime;
            this.amount = amount;
        }
       
        public override bool IfActivate
        {
            get
            {
                return activate;
            }
        }

        public int this[string name]
        {
            get
            {
                if (Variable.ContainsKey(name))
                    return Variable[name];
                else
                    return 0;
            }
            set
            {
                if (this.Variable.ContainsKey(name))
                    this.Variable.Remove(name);
                this.Variable.Add(name, value);
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
        }

        public override void AdditionEnd()
        {
            if (lifeTime != 0)
                TimerEnd();
            if (canEnd && this.AttachedActor.Status != null)
                OnAdditionEnd.Invoke(this.AttachedActor, this);
            if (this.AttachedActor.type == ActorType.PC && this.AttachedActor.Status != null)
            {
                ActorPC pc = (ActorPC)this.AttachedActor;
                //PC.StatusFactory.Instance.CalcStatusOnSkillEffect(pc);
                //Network.Client.MapClient.FromActorPC(pc).SendPlayerInfo();
            }
        }

        public override void AdditionStart()
        {
            canEnd = true;
            if (lifeTime != 0)
            {
                this.endTime = DateTime.Now + new TimeSpan(0, lifeTime / 60000, (lifeTime / 1000) % 60);
                InitTimer(this.period, 0);
                TimerStart();            
            }
            if (this.AttachedActor.Status != null)
                OnAdditionStart.Invoke(this.AttachedActor, this);
            if (this.AttachedActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)this.AttachedActor;
                //PC.StatusFactory.Instance.CalcStatusOnSkillEffect(pc);
                //Network.Client.MapClient.FromActorPC(pc).SendPlayerInfo();
            }
        }

        public override void OnTimerUpdate()
        {
            OnUpdate.Invoke(this.AttachedActor, this);
        }

        public override void OnTimerEnd()
        {
            this.AdditionEnd();
        }
    }
}
