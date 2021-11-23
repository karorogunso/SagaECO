using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_PARTNER_AI_DETAIL_SETUP : Packet
    {
        private byte ainum = 10;

        public CSMG_PARTNER_AI_DETAIL_SETUP()
        {
            this.offset = 2;
        }

        public uint PartnerInventorySlot
        {
            get
            {
                return this.GetUInt(2);
            }
            set
            {
                this.PutUInt(2);
            }
        }
        /// <summary>
        /// return cube unique ids
        /// </summary>
        public Dictionary<byte, ushort> Conditions_ID
        {
            get
            {
                Dictionary<byte, ushort> conditions = new Dictionary<byte, ushort>();
                for (uint i = 0; i < ainum; i++)
                {
                    conditions.Add((byte)(i), this.GetUShort((ushort)(i * 2 + 8)));
                }
                return conditions;
            }
        }
        public Dictionary<byte, byte> HPs
        {
            get
            {
                Dictionary<byte, byte> hps = new Dictionary<byte, byte>();
                for (uint i = 0; i < ainum; i++)
                {
                    hps.Add((byte)(i), this.GetByte((ushort)(i * 2 + 29)));
                }
                return hps;
            }
        }
        /// <summary>
        /// owner=1,enemy=3...
        /// </summary>
        public Dictionary<byte, byte> Targets
        {
            get
            {
                Dictionary<byte, byte> targets = new Dictionary<byte, byte>();
                for (uint i = 0; i < ainum; i++)
                {
                    targets.Add((byte)(i), this.GetByte((ushort)(i * 2 + 40)));
                }
                return targets;
            }
        }
        /// <summary>
        /// return cube unique ids
        /// </summary>
        public Dictionary<byte, ushort> Reactions_ID
        {
            get
            {
                Dictionary<byte, ushort> reactions = new Dictionary<byte, ushort>();
                for (uint i = 0; i < ainum; i++)
                {
                    reactions.Add((byte)(i), this.GetUShort((ushort)(i * 2 + 51)));
                }
                return reactions;
            }
        }
        /// <summary>
        /// seconds
        /// </summary>
        public Dictionary<byte, ushort> Time_Intervals
        {
            get
            {
                Dictionary<byte, ushort> intervals = new Dictionary<byte, ushort>();
                for (uint i = 0; i < ainum; i++)
                {
                    intervals.Add((byte)(i), this.GetUShort((ushort)(i * 2 + 72)));
                }
                return intervals;
            }
        }
        /// <summary>
        /// Return On/Off States of AIs
        /// </summary>
        public Dictionary<byte, bool> AI_states
        {
            get
            {
                Dictionary<byte, bool> states = new Dictionary<byte, bool>();
                ushort off_states_sum = this.GetUShort(92);
                for (uint i = 9; i > 0; i--)
                {
                    if (off_states_sum >= Math.Pow(2, i))//i is off
                    {
                        off_states_sum = (ushort)(off_states_sum - Math.Pow(2, i));
                        states.Add((byte)(i), false);
                    }
                    else
                    {
                        states.Add((byte)(i), true);
                    }
                }
                return states;
            }
        }
        /// <summary>
        /// AI思考设定
        /// </summary>
        public byte BasicAI
        {
            get
            {
                return this.GetByte(94);
            }
        }
        
        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_PARTNER_AI_DETAIL_SETUP();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnPartnerAISetup(this);
        }

    }
}