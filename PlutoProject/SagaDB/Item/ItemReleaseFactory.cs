using SagaLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SagaDB.Item
{
    public class ItemReleaseFactory : Factory<ItemReleaseFactory, ItemRelease>
    {
        public ItemReleaseFactory()
        {
            this.loadingTab = "Loading item release database";
            this.loadedTab = " item release loaded.";
            this.databaseName = "ItemRelease";
            this.FactoryType = FactoryType.CSV;
        }
        protected override uint GetKey(ItemRelease item)
        {
            return item.ID;
        }

        protected override void ParseCSV(ItemRelease item, string[] paras)
        {
            item.ID = uint.Parse(paras[0]);
            item.Name = paras[1];
            item.HP.MinValue = short.Parse(paras[2]);
            item.HP.MaxValue = short.Parse(paras[3]);
            item.MP.MinValue = short.Parse(paras[4]);
            item.MP.MaxValue = short.Parse(paras[5]);
            item.SP.MinValue = short.Parse(paras[6]);
            item.SP.MaxValue = short.Parse(paras[7]);
            item.Weight.MinValue = short.Parse(paras[8]);
            item.Weight.MaxValue = short.Parse(paras[9]);
            item.Volume.MinValue = short.Parse(paras[10]);
            item.Volume.MaxValue = short.Parse(paras[11]);
            item.STR.MinValue = short.Parse(paras[12]);
            item.STR.MaxValue = short.Parse(paras[13]);
            item.DEX.MinValue = short.Parse(paras[14]);
            item.DEX.MaxValue = short.Parse(paras[15]);
            item.INT.MinValue = short.Parse(paras[16]);
            item.INT.MaxValue = short.Parse(paras[17]);
            item.VIT.MinValue = short.Parse(paras[18]);
            item.VIT.MaxValue = short.Parse(paras[19]);
            item.AGI.MinValue = short.Parse(paras[20]);
            item.AGI.MaxValue = short.Parse(paras[21]);
            item.MAG.MinValue = short.Parse(paras[22]);
            item.MAG.MaxValue = short.Parse(paras[23]);
            item.ATK1.MinValue = short.Parse(paras[24]);
            item.ATK1.MaxValue = short.Parse(paras[25]);
            item.ATK2.MinValue = short.Parse(paras[26]);
            item.ATK2.MaxValue = short.Parse(paras[27]);
            item.ATK3.MinValue = short.Parse(paras[28]);
            item.ATK3.MaxValue = short.Parse(paras[29]);
            item.MATK.MinValue = short.Parse(paras[30]);
            item.MATK.MaxValue = short.Parse(paras[31]);
            item.DEF_ADD.MinValue = short.Parse(paras[32]);
            item.DEF_ADD.MaxValue = short.Parse(paras[33]);
            item.MDEF_ADD.MinValue = short.Parse(paras[34]);
            item.MDEF_ADD.MaxValue = short.Parse(paras[35]);
            item.SHIT.MinValue = short.Parse(paras[36]);
            item.SHIT.MaxValue = short.Parse(paras[37]);
            item.LHIT.MinValue = short.Parse(paras[38]);
            item.LHIT.MaxValue = short.Parse(paras[39]);
            item.MHIT.MinValue = short.Parse(paras[40]);
            item.MHIT.MaxValue = short.Parse(paras[41]);
            item.SAVOID.MinValue = short.Parse(paras[42]);
            item.SAVOID.MaxValue = short.Parse(paras[43]);
            item.LAVOID.MinValue = short.Parse(paras[44]);
            item.LAVOID.MaxValue = short.Parse(paras[45]);
            item.MAVOID.MinValue = short.Parse(paras[46]);
            item.MAVOID.MaxValue = short.Parse(paras[47]);
            item.CRIT.MinValue = short.Parse(paras[48]);
            item.CRIT.MaxValue = short.Parse(paras[49]);
            item.CAVOID.MinValue = short.Parse(paras[50]);
            item.CAVOID.MaxValue = short.Parse(paras[51]);
            item.Elements.Clear();
            item.Elements.Add(Elements.Neutral, new ItemReleaseAbility() { MinValue = short.Parse(paras[52]), MaxValue = short.Parse(paras[53]) });
            item.Elements.Add(Elements.Holy, new ItemReleaseAbility() { MinValue = short.Parse(paras[54]), MaxValue = short.Parse(paras[55]) });
            item.Elements.Add(Elements.Dark, new ItemReleaseAbility() { MinValue = short.Parse(paras[56]), MaxValue = short.Parse(paras[57]) });
            item.Elements.Add(Elements.Earth, new ItemReleaseAbility() { MinValue = short.Parse(paras[58]), MaxValue = short.Parse(paras[59]) });
            item.Elements.Add(Elements.Water, new ItemReleaseAbility() { MinValue = short.Parse(paras[60]), MaxValue = short.Parse(paras[61]) });
            item.Elements.Add(Elements.Fire, new ItemReleaseAbility() { MinValue = short.Parse(paras[62]), MaxValue = short.Parse(paras[63]) });
            item.Elements.Add(Elements.Wind, new ItemReleaseAbility() { MinValue = short.Parse(paras[64]), MaxValue = short.Parse(paras[65]) });
        }

        protected override void ParseXML(XmlElement root, XmlElement current, ItemRelease item)
        {
            throw new NotImplementedException();
        }

        public void ReleaseItem(SagaDB.Item.Item item)
        {
            var ability = this.items[item.ItemID];

            if (!(ability.HP.MaxValue.Equals(ability.HP.MaxValue).Equals(0)))
                item.HP += (short)Global.Random.Next(ability.HP.MinValue, ability.HP.MaxValue);
            if (!(ability.MP.MaxValue.Equals(ability.MP.MaxValue).Equals(0)))
                item.MP += (short)Global.Random.Next(ability.MP.MinValue, ability.MP.MaxValue);
            if (!(ability.SP.MaxValue.Equals(ability.SP.MaxValue).Equals(0)))
                item.SP += (short)Global.Random.Next(ability.SP.MinValue, ability.SP.MaxValue);
            if (!(ability.Weight.MaxValue.Equals(ability.Weight.MaxValue).Equals(0)))
                item.WeightUp += (short)Global.Random.Next(ability.Weight.MinValue, ability.Weight.MaxValue);
            if (!(ability.Volume.MaxValue.Equals(ability.Volume.MaxValue).Equals(0)))
                item.VolumeUp += (short)Global.Random.Next(ability.Volume.MinValue, ability.Volume.MaxValue);
            if (!(ability.STR.MaxValue.Equals(ability.STR.MaxValue).Equals(0)))
                item.Str += (short)Global.Random.Next(ability.STR.MinValue, ability.STR.MaxValue);
            if (!(ability.DEX.MaxValue.Equals(ability.DEX.MaxValue).Equals(0)))
                item.Dex += (short)Global.Random.Next(ability.DEX.MinValue, ability.DEX.MaxValue);
            if (!(ability.INT.MaxValue.Equals(ability.INT.MaxValue).Equals(0)))
                item.Int += (short)Global.Random.Next(ability.INT.MinValue, ability.INT.MaxValue);
            if (!(ability.VIT.MaxValue.Equals(ability.VIT.MaxValue).Equals(0)))
                item.Vit += (short)Global.Random.Next(ability.VIT.MinValue, ability.VIT.MaxValue);
            if (!(ability.AGI.MaxValue.Equals(ability.AGI.MaxValue).Equals(0)))
                item.Agi += (short)Global.Random.Next(ability.AGI.MinValue, ability.AGI.MaxValue);
            if (!(ability.MAG.MaxValue.Equals(ability.MAG.MaxValue).Equals(0)))
                item.Mag += (short)Global.Random.Next(ability.MAG.MinValue, ability.MAG.MaxValue);
            if (!(ability.ATK1.MaxValue.Equals(ability.ATK1.MaxValue).Equals(0)))
                item.Atk1 += (short)Global.Random.Next(ability.ATK1.MinValue, ability.ATK1.MaxValue);
            if (!(ability.ATK2.MaxValue.Equals(ability.ATK2.MaxValue).Equals(0)))
                item.Atk2 += (short)Global.Random.Next(ability.ATK2.MinValue, ability.ATK2.MaxValue);
            if (!(ability.ATK3.MaxValue.Equals(ability.ATK3.MaxValue).Equals(0)))
                item.Atk3 += (short)Global.Random.Next(ability.ATK3.MinValue, ability.ATK3.MaxValue);
            if (!(ability.MATK.MaxValue.Equals(ability.MATK.MaxValue).Equals(0)))
                item.MAtk += (short)Global.Random.Next(ability.MATK.MinValue, ability.MATK.MaxValue);
            if (!(ability.DEF_ADD.MaxValue.Equals(ability.DEF_ADD.MaxValue).Equals(0)))
                item.Def += (short)Global.Random.Next(ability.DEF_ADD.MinValue, ability.DEF_ADD.MaxValue);
            if (!(ability.MDEF_ADD.MaxValue.Equals(ability.MDEF_ADD.MaxValue).Equals(0)))
                item.MDef += (short)Global.Random.Next(ability.MDEF_ADD.MinValue, ability.MDEF_ADD.MaxValue);

            if (!(ability.SHIT.MaxValue.Equals(ability.SHIT.MaxValue).Equals(0)))
                item.HitMelee += (short)Global.Random.Next(ability.SHIT.MinValue, ability.SHIT.MaxValue);
            if (!(ability.LHIT.MaxValue.Equals(ability.LHIT.MaxValue).Equals(0)))
                item.HitRanged += (short)Global.Random.Next(ability.LHIT.MinValue, ability.LHIT.MaxValue);
            if (!(ability.MHIT.MaxValue.Equals(ability.MHIT.MaxValue).Equals(0)))
                item.HitMagic += (short)Global.Random.Next(ability.MHIT.MinValue, ability.MHIT.MaxValue);

            if (!(ability.SAVOID.MaxValue.Equals(ability.SAVOID.MaxValue).Equals(0)))
                item.AvoidMelee += (short)Global.Random.Next(ability.SAVOID.MinValue, ability.SAVOID.MaxValue);
            if (!(ability.LAVOID.MaxValue.Equals(ability.LAVOID.MaxValue).Equals(0)))
                item.AvoidRanged += (short)Global.Random.Next(ability.LAVOID.MinValue, ability.LAVOID.MaxValue);
            if (!(ability.MAVOID.MaxValue.Equals(ability.MAVOID.MaxValue).Equals(0)))
                item.AvoidMagic += (short)Global.Random.Next(ability.MAVOID.MinValue, ability.MAVOID.MaxValue);

            if (!(ability.CRIT.MaxValue.Equals(ability.CRIT.MaxValue).Equals(0)))
                item.HitCritical += (short)Global.Random.Next(ability.CRIT.MinValue, ability.CRIT.MaxValue);
            if (!(ability.CAVOID.MaxValue.Equals(ability.CAVOID.MaxValue).Equals(0)))
                item.AvoidCritical += (short)Global.Random.Next(ability.CAVOID.MinValue, ability.CAVOID.MaxValue);


            ////不敢解放属性, 这会产生bug的...除非额外增加特性属性. 但这又只对新道具有效.如果改写属性的计算方式, 旧道具的属性就丢了.....
            //if (!(ability.Elements[Elements.Neutral].MinValue.Equals(ability.Elements[Elements.Neutral].MaxValue).Equals(0)))
            //    item.Element[Elements.Neutral] += (short)Global.Random.Next(ability.Elements[Elements.Neutral].MinValue, ability.Elements[Elements.Neutral].MaxValue);
            //if (!(ability.Elements[Elements.Holy].MinValue.Equals(ability.Elements[Elements.Holy].MaxValue).Equals(0)))
            //    item.Element[Elements.Holy] += (short)Global.Random.Next(ability.Elements[Elements.Holy].MinValue, ability.Elements[Elements.Holy].MaxValue);
            //if (!(ability.Elements[Elements.Dark].MinValue.Equals(ability.Elements[Elements.Dark].MaxValue).Equals(0)))
            //    item.Element[Elements.Dark] += (short)Global.Random.Next(ability.Elements[Elements.Dark].MinValue, ability.Elements[Elements.Dark].MaxValue);
            //if (!(ability.Elements[Elements.Fire].MinValue.Equals(ability.Elements[Elements.Fire].MaxValue).Equals(0)))
            //    item.Element[Elements.Fire] += (short)Global.Random.Next(ability.Elements[Elements.Fire].MinValue, ability.Elements[Elements.Fire].MaxValue);
            //if (!(ability.Elements[Elements.Water].MinValue.Equals(ability.Elements[Elements.Water].MaxValue).Equals(0)))
            //    item.Element[Elements.Water] += (short)Global.Random.Next(ability.Elements[Elements.Water].MinValue, ability.Elements[Elements.Water].MaxValue);
            //if (!(ability.Elements[Elements.Earth].MinValue.Equals(ability.Elements[Elements.Earth].MaxValue).Equals(0)))
            //    item.Element[Elements.Earth] += (short)Global.Random.Next(ability.Elements[Elements.Earth].MinValue, ability.Elements[Elements.Earth].MaxValue);
            //if (!(ability.Elements[Elements.Wind].MinValue.Equals(ability.Elements[Elements.Wind].MaxValue).Equals(0)))
            //    item.Element[Elements.Wind] += (short)Global.Random.Next(ability.Elements[Elements.Wind].MinValue, ability.Elements[Elements.Wind].MaxValue);

            item.Release = true;
        }
    }
}
