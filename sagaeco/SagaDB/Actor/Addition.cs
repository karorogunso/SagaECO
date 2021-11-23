using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaDB.Actor
{
    /// <summary>
    /// A class which contains the information about a players addition bonus (such as Buff)
    /// </summary>
    public abstract class Addition : MultiRunTask
    {
        public enum AdditionType
        {
            PassiveSkill,
            Buff,
            Debuff,
            Control,
            Other,
            Stable,
        }

        
        #region Fields
        private Actor m_myActor;

        /// <summary>
        /// Task instance for this addition
        /// </summary>
        //internal MultiRunTask m_task;

        /// <summary>
        /// A time stamp of when it's get applied to player
        /// </summary>
        private DateTime m_starttime;

        private string m_name;

        private bool m_activated = false;
        
        public AdditionType MyType;

        public bool enabled = true;
        #endregion

        #region Properties

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }
        /// <summary>
        /// Actor that get attached to this addition
        /// </summary>
        /// 
        public Actor AttachedActor
        {
            get
            {
                return m_myActor;
            }
            set
            {
                m_myActor = value;
            }
        }
        /// <summary>
        /// Name of this Addition
        /// </summary>
        public string Name
        {
            get
            {
                return m_name;
            }
            set
            {
                m_name = value;
            }
        }

        /// <summary>
        /// Returns the activation interval
        /// </summary>
        public int Interval
        {
            get
            {
                if (this != null)
                {
                    return this.period;
                }
                else
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// Returns if this addition is activated
        /// </summary>
        public bool Activated
        {
            get
            {
                return m_activated;
            }
            set
            {
                m_activated = value;
            }
        }


        /// <summary>
        /// Time that this addition get started
        /// </summary>
        public DateTime StartTime
        {
            get
            {
                return this.m_starttime;
            }
            set
            {
                this.m_starttime = value;
            }
        }

        /// <summary>
        /// Returns if this addition should be activated
        /// </summary>
        public virtual bool IfActivate
        {
            get
            {
                return true;
            }
        }

        #endregion

        public override string ToString()
        {
            return this.Name;
        }

        #region Virtual methodes
        /// <summary>
        /// Total Life time for this Addition
        /// </summary>
        public virtual int TotalLifeTime
        {
            get { return int.MaxValue; }
            set { }
        }

        /// <summary>
        /// Rest Life time for this Addition
        /// </summary>
        public virtual int RestLifeTime
        {
            get { return int.MaxValue; }
        }

        /// <summary>
        /// Method to be called on Addition start
        /// </summary>
        public abstract void AdditionStart();
        
        /// <summary>
        /// Method to be called on Addition End
        /// </summary>
        public abstract void AdditionEnd();

        /// <summary>
        /// Method that be called once Timer call back function get invoked
        /// </summary>
        public virtual void OnTimerUpdate()
        {
        }

        /// <summary>
        /// Method that be called once Timer get started
        /// </summary>
        public virtual void OnTimerStart()
        {
        }

        /// <summary>
        /// Method that be called once Timer get stoped
        /// </summary>
        public virtual void OnTimerEnd()
        {
        }

        #endregion

        #region Protected Methods
        /// <summary>
        /// Initialize the timer
        /// </summary>
        /// <param name="interval">Interval</param>
        /// <param name="duetime">Due Time</param>
        protected void InitTimer(int interval, int duetime)
        {
            this.dueTime = duetime;
            this.period = interval;//= new MultiRunTask(duetime, interval);
            this.Name = this.Name;
        }

        protected void TimerStart()
        {
            if (this != null)
                this.Activate();
        }

        protected void TimerEnd()
        {
            if (this != null)
                this.Deactivate();
        }
        #endregion

        #region Internal Methods
        public override void CallBack()
        {
            if (this.RestLifeTime > 100)
                this.OnTimerUpdate();
            else
            {
                this.Deactivate(); 
                this.OnTimerEnd();                
            }
        }
        #endregion
    }
}
