using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_INFO : Packet
    {
        public SSMG_PLAYER_INFO()
        {
            this.data = new byte[209];
            this.offset = 2;
            this.ID = 0x01FF; 
        }

        public ActorPC Player
        {
            set
            {
                this.PutUInt(value.ActorID, 2);
                this.PutUInt(value.CharID, 6);
                string name = value.Name;
                name = name.Replace("\0", "");
                byte[] buf = Global.Unicode.GetBytes(name);
                byte[] buff = new byte[210 + buf.Length];
                this.data.CopyTo(buff, 0);
                this.data = buff;                
                ushort offset = (ushort)(12 + buf.Length);
                this.PutByte((byte)(buf.Length + 1), 10);
                this.PutBytes(buf, 11);
                this.PutByte((byte)value.Race, offset);
                this.PutByte((byte)value.Gender, (ushort)(offset + 1));
                this.PutByte(value.HairStyle, (ushort)(offset + 2));
                this.PutByte(value.HairColor, (ushort)(offset + 3));
                this.PutByte(value.Wig, (ushort)(offset + 4));
                this.PutByte(0xff, (ushort)(offset + 5));
                this.PutByte(value.Face, (ushort)(offset + 6));                
                this.PutUInt(value.MapID, (ushort)(offset + 10));
                this.PutByte(Global.PosX16to8(value.X), (ushort)(offset + 14));
                this.PutByte(Global.PosY16to8(value.Y), (ushort)(offset + 15));
                this.PutByte((byte)(value.Dir / 45), (ushort)(offset + 16));
                this.PutUInt(value.HP, (ushort)(offset + 17));
                this.PutUInt(value.MaxHP, (ushort)(offset + 21));
                this.PutUInt(value.MP, (ushort)(offset + 25));
                this.PutUInt(value.MaxMP, (ushort)(offset + 29));
                this.PutUInt(value.SP, (ushort)(offset + 33));
                this.PutUInt(value.MaxSP, (ushort)(offset + 37));
                this.PutByte(8, (ushort)(offset + 41));
                this.PutUShort(value.Str, (ushort)(offset + 42));
                this.PutUShort(value.Dex, (ushort)(offset + 44));
                this.PutUShort(value.Int, (ushort)(offset + 46));
                this.PutUShort(value.Vit, (ushort)(offset + 48));
                this.PutUShort(value.Agi, (ushort)(offset + 50));
                this.PutUShort(value.Mag, (ushort)(offset + 52));
                this.PutUShort(13, (ushort)(offset + 54));//luk
                this.PutUShort(0, (ushort)(offset + 56));//cha
                this.PutByte(0x14, (ushort)(offset + 58));//unknown
                this.PutUInt(0xFFFFFFFF, (ushort)(offset + 101));//possession target
                this.PutByte(0xFF, (ushort)(offset + 105));//possession place
                this.PutUInt(value.Gold, (ushort)(offset + 106));
                this.PutByte(13, (ushort)(offset + 111));
                for (int j = 0; j < 13; j++)
                {
                    if (value.Inventory.Equipments.ContainsKey((EnumEquipSlot)j))
                    {
                        Item item = value.Inventory.Equipments[(EnumEquipSlot)j];
                        this.PutUInt(item.ItemID, (ushort)(offset + 112 + j * 4));
                    }
                }
            }
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public uint CharID
        {
            set
            {
                this.PutUInt(value, 6);
            }
        }

        public string Name
        {
            set
            {
                byte[] buf = Global.Unicode.GetBytes(value + "\0");
                byte[] buff = new byte[209 + buf.Length];
                this.data.CopyTo(buff, 0);
                this.data = buff;
                this.PutByte((byte)buf.Length, 10);
                this.PutBytes(buf, 11);

                this.Unknown = 0xff;
                this.Unknown2 = 0x14;
                //this.Unknown3();
            }
        }

        private ushort GetDataOffset()
        {
            byte offset = this.GetByte(10);
            return (ushort)(11 + offset);
        }

        public PC_RACE Race
        {
            set
            {
                this.PutByte((byte)value, GetDataOffset());
            }
        }

        public PC_GENDER Gender
        {
            set
            {
                this.PutByte((byte)(value), (ushort)(GetDataOffset() + 1));
            }
        }

        public byte HairStyle
        {
            set
            {
                this.PutByte(value, (ushort)(GetDataOffset() + 2));
            }
        }

        public byte HairColor
        {
            set
            {
                this.PutByte(value, (ushort)(GetDataOffset() + 3));
            }
        }

        public byte Wig
        {
            set
            {
                this.PutByte(value, (ushort)(GetDataOffset() + 4));
            }
        }

        public byte Unknown
        {
            set
            {
                this.PutByte(value, (ushort)(GetDataOffset() + 5));
            }
        }

        public byte Face
        {
            set
            {
                this.PutByte(value, (ushort)(GetDataOffset() + 6));
            }
        }

        public uint MapID
        {
            set
            {
                this.PutUInt(value, (ushort)(GetDataOffset() + 10));
            }
        }

        public byte X
        {
            set
            {
                this.PutByte(value, (ushort)(GetDataOffset() + 14));
            }
        }

        public byte Y
        {
            set
            {
                this.PutByte(value, (ushort)(GetDataOffset() + 15));
            }
        }

        public byte Dir
        {
            set
            {
                this.PutByte(value, (ushort)(GetDataOffset() + 16));
            }
        }

        public uint HP
        {
            set
            {
                this.PutUInt(value, (ushort)(GetDataOffset() + 17));
            }
        }

        public uint MAXHP
        {
            set
            {
                this.PutUInt(value, (ushort)(GetDataOffset() + 21));
            }
        }

        public uint MP
        {
            set
            {
                this.PutUInt(value, (ushort)(GetDataOffset() + 25));
            }
        }

        public uint MAXMP
        {
            set
            {
                this.PutUInt(value, (ushort)(GetDataOffset() + 29));
            }
        }

        public uint SP
        {
            set
            {
                this.PutUInt(value, (ushort)(GetDataOffset() + 33));
            }
        }

        public uint MAXSP
        {
            set
            {
                this.PutUInt(value, (ushort)(GetDataOffset() + 37));
            }
        }

        public ushort Str
        {
            set
            {
                this.PutByte(8, (ushort)(GetDataOffset() + 41));
                this.PutUShort(value, (ushort)(GetDataOffset() + 42));
            }
        }

        public ushort Dex
        {
            set
            {
                this.PutUShort(value, (ushort)(GetDataOffset() + 44));
            }
        }

        public ushort Int
        {
            set
            {
                this.PutUShort(value, (ushort)(GetDataOffset() + 46));
            }
        }

        public ushort Vit
        {
            set
            {
                this.PutUShort(value, (ushort)(GetDataOffset() + 48));
            }
        }

        public ushort Agi
        {
            set
            {
                this.PutUShort(value, (ushort)(GetDataOffset() + 50));
            }
        }

        public ushort Mag
        {
            set
            {
                this.PutUShort(value, (ushort)(GetDataOffset() + 52));
            }
        }

        public ushort Luk
        {
            set
            {
                this.PutUShort(value, (ushort)(GetDataOffset() + 54));
            }
        }

        public ushort Cha
        {
            set
            {
                this.PutUShort(value, (ushort)(GetDataOffset() + 56));
            }
        }

        public byte Unknown2
        {
            set
            {
                this.PutByte(value, (ushort)(GetDataOffset() + 58));
            }
        }

        public uint PossessionTarget
        {
            set
            {
                this.PutUInt(value, (ushort)(GetDataOffset() + 101));
            }
        }

        public PossessionPosition PossessionPlace
        {
            set
            {
                this.PutByte((byte)value, (ushort)(GetDataOffset() + 105));
            }
        }

        public uint Gold
        {
            set
            {
                this.PutUInt(value, (ushort)(GetDataOffset() + 106));
            }
        }

        public Dictionary<EnumEquipSlot, uint> Equip
        {
            set
            {
                this.PutByte(13, (ushort)(GetDataOffset() + 111));
                for (int i = 0; i < 13; i++)
                {
                    if (value.ContainsKey((EnumEquipSlot)i))
                        this.PutUInt(value[(EnumEquipSlot)i], (ushort)(GetDataOffset() + 112 + i * 4));
                }
            }
        }

        public void Unknown3()
        {
            this.PutByte(3, (ushort)(GetDataOffset() + 164));
            this.PutUShort(0x30b, (ushort)(GetDataOffset() + 168));
            this.PutUShort(0x903, (ushort)(GetDataOffset() + 171));
            this.PutByte(1, (ushort)(GetDataOffset() + 184));
            this.PutByte(2, (ushort)(GetDataOffset() + 192));
            
        }

        /*         
        ABYTE onehand_motion;// 左手モーションタイプ size=3 { 片手, 両手, 攻撃}
        ABYTE twohand_motion;// 右手モーションタイプ size=3 chr_act_tbl.csvを参照する
        ABYTE  riding_motion;// 乗り物モーションタイプ size=3
        int  ridepet_id; //(itemid
        byte ridepet_color;//ロボ用
        int range     //武器の射程
        int unknown   //0?
        int mode1   //2 r0fa7参照
        int mode2   //0 r0fa7参照
        byte  unknown //0?
        byte  guest //ゲストIDかどうか (07/11/22より)
        */
    }
}
        
