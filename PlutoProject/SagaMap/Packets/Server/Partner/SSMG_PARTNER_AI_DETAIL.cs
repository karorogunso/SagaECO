using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PARTNER_AI_DETAIL : Packet
    {
        private byte ainum = 10;
        private byte cubes_condition_num, cubes_action_num, cubes_activeskill_num, cubes_passiveskill_num;

        public SSMG_PARTNER_AI_DETAIL(byte cubes_condition_num, byte cubes_action_num, byte cubes_activeskill_num, byte cubes_passiveskill_num)
        {
            this.data = new byte[187 + 2 * cubes_condition_num + 2 * cubes_action_num + 2 * cubes_activeskill_num + 2 * cubes_passiveskill_num];
            this.offset = 2;
            this.cubes_condition_num = cubes_condition_num;
            this.cubes_action_num = cubes_action_num;
            this.cubes_activeskill_num = cubes_activeskill_num;
            this.cubes_passiveskill_num = cubes_passiveskill_num;
            this.ID = 0x2183;
            //seperators
            this.PutByte(10, 7);
            this.PutByte(10, 28);
            this.PutByte(10, 39);
            this.PutByte(10, 50);
            this.PutByte(10, 71);
            this.PutByte(10, 95);
            this.PutByte(10, 116);
            this.PutByte(10, 127);
            this.PutByte(10, 138);
            this.PutByte(10, 159);
        }

        public byte Unknown0
        {
            set
            {
                this.PutByte(0, 2);
            }
        }
        public uint PartnerInventorySlot
        {
            set
            {
                this.PutUInt(value, 3);
            }
        }
        /// <summary>
        /// set cube unique ids
        /// </summary>
        public Dictionary<byte, ushort> Conditions_ID
        {
            set
            {
                Dictionary<byte, ushort> conditions = value;
                for (uint i = 0; i < ainum; i++)
                {
                    if (conditions.ContainsKey((byte)(i)))
                    {
                        this.PutUShort(conditions[(byte)(i)], (ushort)(i * 2 + 8));
                    }
                }
            }
        }
        /// <summary>
        /// set cube unique ids
        /// </summary>
        public Dictionary<byte, ushort> Reactions_ID
        {
            set
            {
                Dictionary<byte, ushort> reactions = value;
                for (uint i = 0; i < ainum; i++)
                {
                    if (reactions.ContainsKey((byte)(i)))
                    {
                        this.PutUShort(reactions[(byte)(i)], (ushort)(i * 2 + 51));
                    }
                }
            }
        }
        /// <summary>
        /// seconds
        /// </summary>
        public Dictionary<byte, ushort> Time_Intervals
        {
            set
            {
                Dictionary<byte, ushort> intervals = value;
                for (uint i = 0; i < ainum; i++)
                {
                    if (intervals.ContainsKey((byte)(i)))
                    {
                        this.PutUShort(intervals[(byte)(i)], (ushort)(i * 2 + 72));
                    }
                }
            }
        }
        /// <summary>
        /// Set On/Off States of AIs
        /// </summary>
        public Dictionary<byte, bool> AI_states
        {
            set
            {
                Dictionary<byte, bool> states = value;
                ushort off_states_sum = 0;
                for (uint i = 9; i > 0; i--)
                {
                    if (states.ContainsKey((byte)(i)))
                    {
                        if (!states[(byte)(i)])
                        {
                            off_states_sum = (ushort)(off_states_sum + Math.Pow(2, i));
                        }
                    }
                }
                this.PutUShort(off_states_sum, 92);
            }
        }
        /// <summary>
        /// AI思考设定
        /// </summary>
        public byte BasicAI
        {
            set
            {
                this.PutByte(value, 94);
            }
        }
        public List<ushort> Cubes_Condition
        {
            set
            {
                List<ushort> cubes = value;
                this.PutByte(this.cubes_condition_num, 183);
                for (int i = 0; i < this.cubes_condition_num; i++)
                {
                    this.PutUShort(cubes[i], (ushort)(184 + i * 2));
                }
            }
        }
        public List<ushort> Cubes_Action
        {
            set
            {
                List<ushort> cubes = value;
                this.PutByte(this.cubes_action_num, 184 + 2 * this.cubes_condition_num);
                for (int i = 0; i < this.cubes_action_num; i++)
                {
                    this.PutUShort(cubes[i], (ushort)(185 + 2 * this.cubes_condition_num + i * 2));
                }
            }
        }
        public List<ushort> Cubes_Activeskill
        {
            set
            {
                List<ushort> cubes = value;
                this.PutByte(this.cubes_activeskill_num, 185 + 2 * this.cubes_condition_num + 2 * this.cubes_action_num);
                for (int i = 0; i < this.cubes_activeskill_num; i++)
                {
                    this.PutUShort(cubes[i], (ushort)(186 + 2 * this.cubes_condition_num + 2 * this.cubes_action_num + i * 2));
                }
            }
        }
        public List<ushort> Cubes_Passiveskill
        {
            set
            {
                List<ushort> cubes = value;
                this.PutByte(this.cubes_passiveskill_num, 186 + 2 * this.cubes_condition_num + 2 * this.cubes_action_num + 2 * this.cubes_activeskill_num);
                for (int i = 0; i < this.cubes_passiveskill_num; i++)
                {
                    this.PutUShort(cubes[i], (ushort)(187 + 2 * this.cubes_condition_num + 2 * this.cubes_action_num + 2 * this.cubes_activeskill_num + i * 2));
                }
            }
        }
    }
}
        
