using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Item;

namespace SagaDB.Actor
{    
    public class ActorPC : Actor
    {
        uint charID;
        Account account;
        PC_RACE race;
        PC_GENDER gender;
        byte hairStyle;
        byte hairColor;
        byte wig;
        byte face;
        PC_JOB job;
        byte lv;
        byte jlv1;
        byte jlv2x;
        byte jlv2t;
        ushort questRemaining;

        ushort str, dex, intel, vit, agi, mag;
        uint gold;

        byte slot;

        Inventory inventory = new Inventory();

        //EmotionType emotion;

        public ActorPC()
        {
            this.sightRange = 4200;
            this.Speed = 420;
        }

        public uint CharID
        {
            get
            {
                return charID ;
            }
            set
            {
                charID =value;
            }
        }

        public Account Account
        {
            get
            {
                return account;
            }
            set
            {
                account = value;
            }
        }

        public PC_RACE Race
        {
            get
            {
                return race;
            }
            set
            {
                race = value;
            }
        }

        public PC_GENDER Gender
        {
            get
            {
                return gender;
            }
            set
            {
                gender = value;
            }
        }

        public byte HairStyle
        {
            get
            {
                return hairStyle;
            }
            set
            {
                hairStyle = value;
            }
        }

        public byte HairColor
        {
            get
            {
                return hairColor;
            }
            set
            {
                hairColor = value;
            }
        }

        public byte Wig
        {
            get
            {
                return wig;
            }
            set
            {
                wig = value;
            }
        }

        public byte Face
        {
            get
            {
                return face;
            }
            set
            {
                face = value;
            }
        }

        public PC_JOB Job
        {
            get
            {
                return job;
            }
            set
            {
                job = value;
            }
        }      

        public byte Level
        {
            get
            {
                return lv;
            }
            set
            {
                lv = value;
            }
        }

        public byte JobLevel1
        {
            get
            {
                return jlv1;
            }
            set
            {
                jlv1 = value;
            }
        }

        public ushort QuestRemaining
        {
            get
            {
                return questRemaining;
            }
            set
            {
                questRemaining = value;
            }
        }

        public byte JobLevel2X
        {
            get
            {
                return jlv2x;
            }
            set
            {
                jlv2x = value;
            }
        }

        public byte JobLevel2T
        {
            get
            {
                return jlv2t;
            }
            set
            {
                jlv2t = value;
            }
        }

        public byte Slot
        {
            get
            {
                return slot;
            }
            set
            {
                slot = value;
            }
        }

        public ushort Str { get { return this.str; } set { this.str = value; } }
        public ushort Dex { get { return this.dex; } set { this.dex = value; } }
        public ushort Int { get { return this.intel; } set { this.intel = value; } }
        public ushort Vit { get { return this.vit; } set { this.vit = value; } }
        public ushort Agi { get { return this.agi; } set { this.agi = value; } }
        public ushort Mag { get { return this.mag; } set { this.mag = value; } }

        public uint Gold { get { return this.gold; } set { this.gold = value; } }

        public Inventory Inventory
        {
            get
            {
                return this.inventory;
            }
        }

        //public EmotionType Emotion { get { return this.emotion; } set { this.emotion = value; } }
    }
}
