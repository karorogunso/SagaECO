using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Manager;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill
{
    public partial class SkillHandler
    {

        public int fieldelements(Map map, byte x, byte y, Elements eletype)
        {
            int fieldele = 0;
            if (eletype == Elements.Neutral)
            {
                fieldele = map.Info.neutral[x, y];
            }
            if (eletype == Elements.Fire)
            {
                fieldele = map.Info.fire[x, y];
            }
            if (eletype == Elements.Water)
            {
                fieldele = map.Info.water[x, y];
            }
            if (eletype == Elements.Wind)
            {
                fieldele = map.Info.wind[x, y];
            }
            if (eletype == Elements.Earth)
            {
                fieldele = map.Info.earth[x, y];
            }
            if (eletype == Elements.Holy)
            {
                fieldele = map.Info.holy[x, y];
            }
            if (eletype == Elements.Dark)
            {
                fieldele = map.Info.dark[x, y];
            }
            return fieldele;
        }

        // 0 = 无变化
        // 1 = 增加A, 2 = 增加B, 3 = 增加C, 4 = 增加D,
        // 5 = 减少A, 6 = 减少B, 7 = 减少C, 8 = 减少D
        //     无 火 水 风 土 光 暗
        // 无   0  0  0  0  0  0  0
        // 火   0  5  2  6  0  3  7
        // 水   0  6  5  0  2  3  7
        // 风   0  2  0  5  5  3  7
        // 土   0  0  6  2  5  3  7
        // 光   0  6  6  6  6  5  1
        // 暗   0  4  4  4  4  6  5

        int[,] EFtype = new int[,]{
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 5, 2, 6, 0, 3, 7 },
            { 0, 6, 5, 0, 2, 3, 7 },
            { 0, 2, 0, 5, 5, 3, 7 },
            { 0, 0, 6, 2, 5, 3, 7 },
            { 0, 6, 6, 6, 6, 5, 1 },
            { 0, 4, 4, 4, 4, 6, 5 }
        };

        //横排防御等级
        //竖列变化等级
        float[,] DEFAULTBONUS = new float[,]
        {
            { 0.00f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f },
            { 0.00f, 1.20f, 1.30f, 1.40f, 1.50f, 1.60f, 1.70f, 1.80f, 1.90f, 2.00f, 2.15f, 2.30f, 2.45f, 2.60f, 2.75f, 2.90f, 3.05f, 3.20f, 3.35f, 3.50f, 3.80f },
            { 0.00f, 1.20f, 1.30f, 1.40f, 1.50f, 1.60f, 1.70f, 1.80f, 1.90f, 2.00f, 2.10f, 2.20f, 2.30f, 2.40f, 2.50f, 2.65f, 2.80f, 2.95f, 3.10f, 3.30f, 3.50f },
            { 0.00f, 1.05f, 1.10f, 1.15f, 1.20f, 1.25f, 1.30f, 1.35f, 1.40f, 1.45f, 1.50f, 1.55f, 1.60f, 1.65f, 1.70f, 1.75f, 1.80f, 1.85f, 1.90f, 1.95f, 2.00f },
            { 0.00f, 1.05f, 1.10f, 1.15f, 1.20f, 1.25f, 1.30f, 1.35f, 1.40f, 1.45f, 1.50f, 1.55f, 1.60f, 1.65f, 1.70f, 1.75f, 1.80f, 1.85f, 1.90f, 1.95f, 2.00f },
            { 0.00f, 0.90f, 0.80f, 0.70f, 0.60f, 0.50f, 0.40f, 0.30f, 0.20f, 0.10f, 0.00f, 0.00f, 0.00f, 0.00f, 0.00f, 0.00f, 0.00f, 0.00f, 0.00f, 0.00f, 0.00f },
            { 0.00f, 0.95f, 0.90f, 0.85f, 0.80f, 0.75f, 0.70f, 0.65f, 0.60f, 0.55f, 0.50f, 0.45f, 0.40f, 0.35f, 0.30f, 0.25f, 0.20f, 0.15f, 0.10f, 0.05f, 0.00f },
            { 0.00f, 0.97f, 0.94f, 0.91f, 0.88f, 0.85f, 0.82f, 0.79f, 0.76f, 0.73f, 0.70f, 0.67f, 0.64f, 0.61f, 0.58f, 0.55f, 0.52f, 0.49f, 0.46f, 0.43f, 0.40f },
            { 0.00f, 0.99f, 0.96f, 0.93f, 0.90f, 0.87f, 0.84f, 0.81f, 0.79f, 0.76f, 0.73f, 0.70f, 0.67f, 0.64f, 0.61f, 0.59f, 0.56f, 0.53f, 0.50f, 0.47f, 0.44f }
        };

        int bonustype(Elements src, Elements dst)
        {
            return EFtype[(int)src, (int)dst];
        }

        float defaultbonus(int defincelevel, int attacktype)
        {
            return DEFAULTBONUS[attacktype, defincelevel];
        }

        /// <summary>
        /// 计算实际的属性倍率
        /// </summary>
        /// <param name="sActor">攻击者</param>
        /// <param name="dActor">防御者</param>
        /// <param name="skillelement">技能属性</param>
        /// <param name="skilltype">技能类型, 物理:0, 魔法:1</param>
        /// <param name="heal">是否为治疗技能</param>
        /// <returns>倍率</returns>
        float efcal(Actor sActor, Actor dActor, Elements skillelement, int skilltype, bool heal)
        {
            Map map;
            byte dx, dy, sx, sy;
            float res = 1f;

            #region Calc Attacker and Defincer Coordinate

            // Attacker and Defincer Must be in the same map
            map = MapManager.Instance.GetMap(dActor.MapID);

            //Attacker Coordinate
            sx = Global.PosX16to8(sActor.X, map.Width);
            sy = Global.PosY16to8(sActor.Y, map.Height);

            //Defincer Coordinate
            dx = Global.PosX16to8(dActor.X, map.Width);
            dy = Global.PosY16to8(dActor.Y, map.Height);

            #endregion

            #region Calc Attack and Defince Element

            Elements attackElement = Elements.Neutral;
            Elements defineElement = Elements.Neutral;
            int atkValue = 0;
            int defValue = 0;

            attackElement = GetAttackElement(sActor, ref atkValue, map, sx, sy);
            defineElement = GetDefElement(dActor, ref defValue, map, dx, dy);

            if (sActor.type == ActorType.MOB)
            {
                attackElement = Elements.Neutral;
                atkValue = 100;
            }
            if (skillelement != Elements.Neutral)
            {
                attackElement = skillelement;
                atkValue = 100 + sActor.AttackElements[skillelement] + fieldelements(map, sx, sy, skillelement);
            }
            if (sActor.Status.Additions.ContainsKey("Astralist"))
            {
                atkValue += (sActor.Status.Additions["Astralist"] as DefaultBuff).Variable["Astralist"];
            }

            #endregion

            #region CalcElementFactor

            float Factor = 1.000f;

            if (heal)
            {
                if ((dActor.Elements[Elements.Dark] + dActor.Status.elements_item[Elements.Dark] + fieldelements(map, dx, dy, Elements.Dark)) <= 100)
                {
                    res = (1f + (float)Math.Sqrt((dActor.Elements[Elements.Holy] + dActor.Status.elements_item[Elements.Holy] + fieldelements(map, dx, dy, Elements.Holy)) / 100.0)) * ((float)Math.Sqrt(1.0 - (dActor.Elements[Elements.Dark] + dActor.Status.elements_item[Elements.Dark] + fieldelements(map, dx, dy, Elements.Dark)) / 100.0));
                }
                else
                {
                    res = -(float)Math.Sqrt((dActor.Elements[Elements.Dark] + dActor.Status.elements_item[Elements.Dark] + fieldelements(map, dx, dy, Elements.Dark)) / 100.0 - 1.0);
                }
            }
            else
            {
                int elementbonustype = bonustype(attackElement, defineElement);


                GetElementFactor(atkValue, defValue, elementbonustype, ref Factor);

                res = res * Factor;
            }

            if (sActor.Status.Additions.ContainsKey("EvilSoul")|| sActor.Status.Additions.ContainsKey("SoulTaker"))
            {
                float rate = 0f;
                if (sActor.Status.Additions.ContainsKey("EvilSoul"))
                {
                    rate += ((float)((sActor.Status.Additions["EvilSoul"] as DefaultBuff).Variable["EvilSoul"]) / 100.0f);
                }
                if (sActor.Status.Additions.ContainsKey("SoulTaker"))
                {
                    rate += ((float)((sActor.Status.Additions["SoulTaker"] as DefaultBuff).Variable["SoulTaker"]) / 100.0f);
                }
                if (attackElement == Elements.Dark)
                    res *= rate;
            }

            #endregion

            return res;
        }

        private void GetElementFactor(int atkValue, int defValue, int type, ref float Factor)
        {
            int deflevel = GetDefElementLevel(defValue);
            Factor = defaultbonus(deflevel, type);
            if (type > 0 && type < 5)
            {
                if (type == 4)
                    Factor += atkValue / 400;
                else
                    Factor += atkValue / 100;
            }
        }


        private short GetDefElementLevel(int DefinceValue)
        {
            if (DefinceValue < 10)
                return 1;
            else if (DefinceValue >= 10 && DefinceValue <= 14)
                return 2;
            else if (DefinceValue >= 15 && DefinceValue <= 19)
                return 3;
            else if (DefinceValue >= 20 && DefinceValue <= 24)
                return 4;
            else if (DefinceValue >= 25 && DefinceValue <= 29)
                return 5;
            else if (DefinceValue >= 30 && DefinceValue <= 34)
                return 6;
            else if (DefinceValue >= 35 && DefinceValue <= 39)
                return 7;
            else if (DefinceValue >= 40 && DefinceValue <= 44)
                return 8;
            else if (DefinceValue >= 45 && DefinceValue <= 49)
                return 9;
            else if (DefinceValue >= 50 && DefinceValue <= 54)
                return 10;
            else if (DefinceValue >= 55 && DefinceValue <= 59)
                return 11;
            else if (DefinceValue >= 60 && DefinceValue <= 64)
                return 12;
            else if (DefinceValue >= 65 && DefinceValue <= 69)
                return 13;
            else if (DefinceValue >= 70 && DefinceValue <= 74)
                return 14;
            else if (DefinceValue >= 75 && DefinceValue <= 79)
                return 15;
            else if (DefinceValue >= 80 && DefinceValue <= 84)
                return 16;
            else if (DefinceValue >= 85 && DefinceValue <= 89)
                return 17;
            else if (DefinceValue >= 90 && DefinceValue <= 94)
                return 18;
            else if (DefinceValue >= 95 && DefinceValue <= 99)
                return 19;
            else
                return 20;
        }

        private Elements GetAttackElement(Actor sActor, ref int atkvalue, Map map, byte x, byte y)
        {
            Elements ele = Elements.Neutral;

            foreach (var item in sActor.AttackElements)
            {
                if (atkvalue < item.Value)
                {
                    ele = item.Key;
                    atkvalue = item.Value;
                }
            }
            atkvalue += fieldelements(map, x, y, ele);
            return ele;
        }

        private Elements GetDefElement(Actor dActor, ref int defvalue, Map map, byte x, byte y)
        {
            Elements ele = Elements.Neutral;

            foreach (var item in dActor.Elements)
            {
                if (defvalue < item.Value)
                {
                    defvalue = item.Value;
                    ele = item.Key;
                }
            }
            defvalue += fieldelements(map, x, y, ele);

            if (dActor.Status.Additions.ContainsKey("WaterFrosenElement"))
            {
                ele = Elements.Water;
                defvalue = 100;
            }
            if (dActor.Status.Additions.ContainsKey("StoneFrosenElement"))
            {
                ele = Elements.Earth;
                defvalue = 100;
            }
            return ele;
        }

        /// <summary>
        /// 计算实际的属性倍率
        /// </summary>
        /// <param name="sActor">攻击者</param>
        /// <param name="dActor">防御者</param>
        /// <param name="skillelement">技能属性</param>
        /// <param name="skilltype">技能类型, 物理:0, 魔法:1</param>
        /// <param name="heal">是否为治疗技能</param>
        /// <returns>倍率</returns>
        float CalcElementBonus(Actor sActor, Actor dActor, Elements element, int skilltype, bool heal)
        {
            return efcal(sActor, dActor, element, skilltype, heal);
        }
    }
}
