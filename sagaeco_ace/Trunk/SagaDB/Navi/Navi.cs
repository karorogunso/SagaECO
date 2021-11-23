using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;

namespace SagaDB.Navi
{
    /// <summary>
    /// 导航
    /// </summary>
    public class Navi
    {
        Dictionary<uint, Category> categories;
        Dictionary<uint, Step> uniqueSteps;
        public Navi()
        {
             this.categories = new Dictionary<uint,Category>();
        }
        public Navi(Navi navi)
        {
            //this.categories = new Dictionary<uint, Category>(navi.categories);
        }
        public Dictionary<uint, Category> Categories
        {
            get
            {
                return this.categories;
            }
        }
        public Dictionary<uint, Step> UniqueSteps
        {
            get
            {
                return this.uniqueSteps;
            }
        }
    }
    public class Category
    {
        uint id;
        Dictionary<uint, Event> events = new Dictionary<uint,Event>();
        public Category(uint id)
        {
            this.id = id;
        }
        public uint ID
        {
            get
            {
                return this.id;
            }
        }
        public Dictionary<uint, Event> Events
        {
            get
            {
                return this.events;
            }
        }
    }
    public class Event
    {
        uint id;
        byte state = 1;
        //bool show = true;
        //bool finished = false;
        //bool notRewarded = false;
        int displaySteps = 0;
        int finishedSteps = 0;
        Dictionary<uint, Step> steps = new Dictionary<uint,Step>();
        public Event(uint id)
        {
            this.id = id;
        }
        public uint ID
        {
            get
            {
                return this.id;
            }
        }
        /// <summary>
        /// Event状态，最低位为是否显示（1为显示），次低位为是否完成（1为完成），三低位为是否已领取奖励（1为未领取）
        /// </summary>
        public byte State
        {
            get
            {
                return this.state;
            }
            set
            {
                if (value > 7)
                {
                    this.state = 7;
                }
                else
                {
                    this.state = value;
                }
            }
        }
        /// <summary>
        /// 设定和获取Event是否显示（1为显示）
        /// </summary>
        public bool Show
        {
            get
            {
                return toBool(state &= 1);
            }
            set
            {
                if (value)
                {
                    state |= 1;
                }
                else
                {
                    state &= 0xFE;
                }
            }
        }
        /// <summary>
        /// 设定和获取Event是否完成（1为完成）
        /// </summary>
        public bool Finished
        {
            get
            {
                return toBool(state &= 2);
            }
            set
            {
                if (value)
                {
                    state |= 2;
                }
                else
                {
                    state &= 0xFD;
                }
            }
        }
        /// <summary>
        /// 设定和获取Event是否已领取奖励（1为未领取）
        /// </summary>
        public bool NotRewarded
        {
            get
            {
                return toBool(state &= 4);
            }
            set
            {
                if (value)
                {
                    state |= 4;
                }
                else
                {
                    state &= 0xFB;
                }
            }
        }
        /// <summary>
        /// 设定和获取Event的每个步骤是否在导航开始后显示
        /// </summary>
        public int DisplaySteps
        {
            get
            {
                return this.displaySteps;
            }
            set 
            { 
                this.displaySteps = value; 
            }
        }
        /// <summary>
        /// 设定和获取Event的每个步骤是否完成
        /// </summary>
        public int FinishedSteps
        {
            get
            {
                return this.finishedSteps;
            }
            set
            {
                this.finishedSteps = value;
            }
        }
        public Dictionary<uint, Step> Steps
        {
            get
            {
                return this.steps;
            }
        }
        private bool toBool(byte b)
        {
            if (b == 0)
            {
                return false;
            }
            return true;
        }
    }
    public class Step
    {
        uint id;
        uint uniqueId;
        //bool display = false;
        //bool finished = false;
        Event belongEvent;
        public uint ID
        {
            get
            {
                return this.id;
            }
        }
        public uint UniqueId
        {
            get
            {
                return this.uniqueId;
            }
        }
        public Step(uint id, uint uniqueId, Event belongEvent)
        {
            this.id = id;
            this.uniqueId = uniqueId;
            this.belongEvent = belongEvent;
        }
        public bool Display
        {
            get
            {
                return (toBool(belongEvent.DisplaySteps &= (1 << ((int)id - 1))));
            }
            set
            {
                if (value)
                {
                    belongEvent.DisplaySteps |= (1 << ((int)id - 1));
                }
                else
                {
                    belongEvent.DisplaySteps &= ~(1 << ((int)id - 1));
                }
            }
        }
        public bool Finished
        {
            get
            {
                return (toBool(belongEvent.FinishedSteps &= (1 << ((int)id - 1))));
            }
            set
            {
                if (value)
                {
                    belongEvent.FinishedSteps |= (1 << ((int)id - 1));
                }
                else
                {
                    belongEvent.FinishedSteps &= ~(1 << ((int)id - 1));
                }
            }
        }
        public Event BelongEvent
        {
            get
            {
                return this.belongEvent;
            }
            set
            {
                this.belongEvent = value;
            }
        }
        private bool toBool(int i)
        {
            if(i == 0)
            {
                return false;
            }
            return true;
        }
    }
}
