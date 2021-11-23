using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Manager;

namespace SagaMap.Skill
{
    public partial class SkillHandler
    {
        float elementOld(Actor sActor, Actor dActor, Elements element)
        {
            Map map;
            map = MapManager.Instance.GetMap(dActor.MapID);
            byte x, y;
            float res = 1f;
            x = Global.PosX16to8(dActor.X, map.Width);
            y = Global.PosY16to8(dActor.Y, map.Height);
            switch (element)
            {
                case Elements.Fire:
                    {
                        byte fire = 0, water = 0, wind = 0, holy = 0, dark = 0;
                        if (x < map.Info.width && y < map.Height)
                        {
                            fire = map.Info.fire[x, y];
                            water = map.Info.water[x, y];
                            wind = map.Info.wind[x, y];
                            holy = map.Info.holy[x, y];
                            dark = map.Info.dark[x, y];
                        }
                        fire += (byte)dActor.Elements[Elements.Fire];
                        water += (byte)dActor.Elements[Elements.Water];
                        wind += (byte)dActor.Elements[Elements.Wind];
                        holy += (byte)dActor.Elements[Elements.Holy];
                        dark += (byte)dActor.Elements[Elements.Dark];
                        if (fire > 100) fire = 100;
                        if (water > 100) water = 100;
                        if (wind > 100) wind = 100;
                        if (holy > 100) holy = 100;
                        if (dark > 100) dark = 100;

                        res = 1f + (((water * 0.66f - fire - wind * 0.5f) / 100f) * (1f + (holy * 0.33f - dark * 0.33f) / 100f));
                    }
                    break;
                case Elements.Wind:
                    {
                        byte fire = 0, earth = 0, wind = 0, holy = 0, dark = 0;
                        if (x < map.Info.width && y < map.Height)
                        {
                            fire = map.Info.fire[x, y];
                            earth = map.Info.earth[x, y];
                            wind = map.Info.wind[x, y];
                            holy = map.Info.holy[x, y];
                            dark = map.Info.dark[x, y];
                        }

                        fire += (byte)dActor.Elements[Elements.Fire];
                        wind += (byte)dActor.Elements[Elements.Wind];
                        earth += (byte)dActor.Elements[Elements.Earth];
                        holy += (byte)dActor.Elements[Elements.Holy];
                        dark += (byte)dActor.Elements[Elements.Dark];
                        if (fire > 100) fire = 100;
                        if (wind > 100) wind = 100;
                        if (earth > 100) earth = 100;
                        if (holy > 100) holy = 100;
                        if (dark > 100) dark = 100;

                        res = 1f + (((fire * 0.66f - wind - earth * 0.5f) / 100f) * (1f + (holy * 0.33f - dark * 0.33f) / 100f));
                    }
                    break;
                case Elements.Water:
                    {
                        byte fire = 0, water = 0, earth = 0, holy = 0, dark = 0;
                        if (x < map.Info.width && y < map.Height)
                        {
                            fire = map.Info.fire[x, y];
                            water = map.Info.water[x, y];
                            earth = map.Info.earth[x, y];
                            holy = map.Info.holy[x, y];
                            dark = map.Info.dark[x, y];
                        }

                        fire += (byte)dActor.Elements[Elements.Fire];
                        water += (byte)dActor.Elements[Elements.Water];
                        earth += (byte)dActor.Elements[Elements.Earth];
                        holy += (byte)dActor.Elements[Elements.Holy];
                        dark += (byte)dActor.Elements[Elements.Dark];
                        if (fire > 100) fire = 100;
                        if (water > 100) water = 100;
                        if (earth > 100) earth = 100;
                        if (holy > 100) holy = 100;
                        if (dark > 100) dark = 100;

                        res = 1f + (((earth * 0.66f - water - fire * 0.5f) / 100f) * (1f + (holy * 0.33f - dark * 0.33f) / 100f));
                    }
                    break;
                case Elements.Earth:
                    {
                        byte earth = 0, water = 0, wind = 0, holy = 0, dark = 0;
                        if (x < map.Info.width && y < map.Height)
                        {
                            wind = map.Info.wind[x, y];
                            water = map.Info.water[x, y];
                            earth = map.Info.earth[x, y];
                            holy = map.Info.holy[x, y];
                            dark = map.Info.dark[x, y];
                        }

                        water += (byte)dActor.Elements[Elements.Water];
                        wind += (byte)dActor.Elements[Elements.Wind];
                        earth += (byte)dActor.Elements[Elements.Earth];
                        holy += (byte)dActor.Elements[Elements.Holy];
                        dark += (byte)dActor.Elements[Elements.Dark];
                        if (water > 100) water = 100;
                        if (wind > 100) wind = 100;
                        if (earth > 100) earth = 100;
                        if (holy > 100) holy = 100;
                        if (dark > 100) dark = 100;

                        res = 1f + (((wind * 0.66f - earth - water * 0.5f) / 100f) * (1f + (holy * 0.33f - dark * 0.33f) / 100f));
                    }
                    break;
                case Elements.Holy:
                    {
                        byte fire = 0, water = 0, wind = 0, earth = 0, holy = 0, dark = 0;
                        byte val = 0;
                        if (x < map.Info.width && y < map.Height)
                        {
                            fire = map.Info.fire[x, y];
                            water = map.Info.water[x, y];
                            earth = map.Info.earth[x, y];
                            wind = map.Info.wind[x, y];
                            holy = map.Info.holy[x, y];
                            dark = map.Info.dark[x, y];
                        }
                        fire += (byte)dActor.Elements[Elements.Fire];
                        water += (byte)dActor.Elements[Elements.Water];
                        wind += (byte)dActor.Elements[Elements.Wind];
                        earth += (byte)dActor.Elements[Elements.Earth];
                        holy += (byte)dActor.Elements[Elements.Holy];
                        dark += (byte)dActor.Elements[Elements.Dark];
                        if (fire > 100) fire = 100;
                        if (water > 100) water = 100;
                        if (wind > 100) wind = 100;
                        if (earth > 100) earth = 100;
                        if (holy > 100) holy = 100;
                        if (dark > 100) dark = 100;

                        val = fire;
                        if (water > val) val = water;
                        if (earth > val) val = earth;
                        if (wind > val) val = wind;

                        res = 1f + (((dark * 0.66f - holy) / 100f) - ((val * 0.5f) / 100));
                    }
                    break;
                case Elements.Dark:
                    {
                        byte fire = 0, water = 0, earth = 0, wind = 0, holy = 0, dark = 0;
                        byte val = 0;
                        if (x < map.Info.width && y < map.Height)
                        {
                            fire = map.Info.fire[x, y];
                            water = map.Info.water[x, y];
                            earth = map.Info.earth[x, y];
                            wind = map.Info.wind[x, y];
                            holy = map.Info.holy[x, y];
                            dark = map.Info.dark[x, y];
                        }

                        fire += (byte)dActor.Elements[Elements.Fire];
                        water += (byte)dActor.Elements[Elements.Water];
                        wind += (byte)dActor.Elements[Elements.Wind];
                        earth += (byte)dActor.Elements[Elements.Earth];
                        holy += (byte)dActor.Elements[Elements.Holy];
                        dark += (byte)dActor.Elements[Elements.Dark];
                        if (fire > 100) fire = 100;
                        if (water > 100) water = 100;
                        if (wind > 100) wind = 100;
                        if (earth > 100) earth = 100;
                        if (holy > 100) holy = 100;
                        if (dark > 100) dark = 100;

                        val = fire;
                        if (water > val) val = water;
                        if (earth > val) val = earth;
                        if (wind > val) val = wind;

                        res = 1f + (((-holy * 0.66f - dark) / 100f) + ((val * 0.5f) / 100));
                    }
                    break;
            }
            if (res < 0) res = 0;
            return res;
        }

        Elements elementField(Map map, byte x, byte y, out int value)
        {
            Elements fieldElement = Elements.Neutral;
            value = 0;
            if (map.Info.fire[x, y] != 0)
            {
                fieldElement = Elements.Fire;
                value = map.Info.fire[x, y];
            }
            if (value < map.Info.water[x, y])
            {
                fieldElement = Elements.Water;
                value = map.Info.water[x, y];
            }
            if (value < map.Info.wind[x, y])
            {
                fieldElement = Elements.Wind;
                value = map.Info.wind[x, y];
            }
            if (value < map.Info.earth[x, y])
            {
                fieldElement = Elements.Earth;
                value = map.Info.earth[x, y];
            }
            if (value < map.Info.holy[x, y])
            {
                fieldElement = Elements.Holy;
                value = map.Info.holy[x, y];
            }
            if (value < map.Info.dark[x, y])
            {
                fieldElement = Elements.Dark;
                value = map.Info.dark[x, y];
            }
            return fieldElement;
        }
        
        /*增加A=0 B=1 C=2
             * 减少A=3 B=4, C=5
             */ 
        int[,] elementEffects = new int[,]{
        {6,6,6,6,6,6,6},
        {6,3,0,4,6,1,5},
        {6,4,3,6,0,1,5},
        {6,0,6,3,4,1,5},
        {6,6,4,0,3,1,5},
        {6,4,4,4,4,3,0},
        {6,2,2,2,2,4,3}
        };

        float[][] elementFactor = new float[][]{
            new  float[]{1.2f,1.3f,1.4f,1.5f,1.6f,1.7f,1.8f,1.9f,2.0f,2.1f,2.2f,2.3f,2.4f,2.5f,2.65f,2.8f,2.95f,3.1f,3.3f,3.5f},
            new  float[]{1.05f,1.1f,1.15f,1.2f,1.25f,1.3f,1.35f,1.4f,1.45f,1.5f,1.55f,1.6f,1.65f,1.7f,1.75f,1.8f,1.85f,1.9f,1.95f,2f},
            new  float[]{1.05f,1.1f,1.15f,1.2f,1.25f,1.3f,1.35f,1.4f,1.45f,1.5f,1.55f,1.6f,1.65f,1.7f,1.75f,1.8f,1.85f,1.9f,1.95f,2f},
            new  float[]{0.9f,0.8f,0.7f,0.6f,0.5f,0.4f,0.3f,0.2f,0.1f,0f,0f,0f,0f,0f,0f,0f,0f,0f,0f,0f},
            new  float[]{0.95f,0.9f,0.85f,0.8f,0.75f,0.7f,0.65f,0.6f,0.55f,0.5f,0.45f,0.4f,0.35f,0.3f,0.25f,0.2f,0.15f,0.1f,0.5f,0f},
            new  float[]{0.97f,0.94f,0.91f,0.88f,0.85f,0.82f,0.79f,0.76f,0.73f,0.7f,0.67f,0.64f,0.61f,0.58f,0.55f,0.52f,0.49f,0.46f,0.43f,0.4f},
            new  float[]{1f,1f,1f,1f,1f,1f,1f,1f,1f,1f,1f,1f,1f,1f,1f,1f,1f,1f,1f,1f}
        };

        int elementEffect(Elements src, Elements dst)
        {
            return elementEffects[(int)src, (int)dst];
        }

        int elementLevel(int elementValue)
        {
            if (elementValue > 100)
                elementValue = 100;
            if (elementValue < 10)
                return 0;
            return (elementValue / 5) - 1;
        }

        float elementNew(Actor sActor, Actor dActor, Elements element, int elementValue, bool heal)
        {
            Map map;
            map = MapManager.Instance.GetMap(dActor.MapID);
            byte x, y;
            float res = 1f;
            x = Global.PosX16to8(dActor.X, map.Width);
            y = Global.PosY16to8(dActor.Y, map.Height);

            int fieldValue = 0;
            Elements fieldElement = elementField(map, x, y, out fieldValue);

            #region Attack Element

            Elements attackElement = Elements.Neutral;
            int atkValue = 0;
            foreach (Elements i in sActor.AttackElements.Keys)
            {
                if (atkValue < (sActor.AttackElements[i] + sActor.Status.attackElements_item[i]))
                {
                    attackElement = i;
                    atkValue = sActor.AttackElements[i] + sActor.Status.attackElements_item[i];
                }
            }
            if (element != Elements.Neutral)
            {
                if (element == attackElement)
                    atkValue += elementValue;
                else
                {
                    attackElement = element;
                    atkValue = elementValue;
                }
            }
            if (attackElement != Elements.Neutral)
            {
                if (attackElement == fieldElement)
                    atkValue += fieldValue;
            }
            #endregion

            #region Defence Element
            Elements defElement = Elements.Neutral;
            int defValue = 0;
            foreach (Elements i in dActor.Elements.Keys)
            {
                if (defValue < (dActor.Elements[i] + dActor.Status.elements_item[i]))
                {
                    defElement = i;
                    defValue = dActor.Elements[i] + dActor.Status.elements_item[i];
                }
            }
            if (defElement != Elements.Neutral)
            {
                if (defElement == fieldElement)
                    defValue += fieldValue;
            }
            #endregion

            int effect = 0;
            if (heal)
            {
                effect = elementEffect(Elements.Neutral, Elements.Neutral);
                res = elementFactor[effect][elementLevel(0)];
            }
            else
            {
                effect = elementEffect(attackElement, defElement);
                res = elementFactor[effect][elementLevel(defValue)];
            }

            if (effect <= 1)//「増加A」、「増加B」のとき 
                res = res + ((float)atkValue / 100);
            if (effect == 2)//「増加C」のとき 
                res = res + ((float)atkValue / 400);

            if (heal)
            {
                switch (defElement)
                {
                    case Elements.Holy:
                        res += 0.1f * elementLevel(defValue);
                        break;
                    case Elements.Dark:
                        res -= 0.05f * elementLevel(defValue);
                        break;
                }
            }
            if (res < 0)
                res = 0;
            if (res > 1f && sActor.Status.ElementDamegeUp_rate > 0)
            {
                res += sActor.Status.ElementDamegeUp_rate;
            }
            if (dActor.Status.Additions.ContainsKey("TranceBody"))
            {
                SagaMap.Skill.SkillDefinations.Astralist.TranceBody body = new SkillDefinations.Astralist.TranceBody();
                body.Passive(dActor, sActor, element, elementValue, 0);
            }
            return res;
        }


        float CalcElementBonus(Actor sActor, Actor dActor, Elements element, int elementValue, bool heal)
        {
            if (Configuration.Instance.Version < SagaLib.Version.Saga9_Iris)
                if (heal)
                    return 0f;
                else
                    return elementOld(sActor, dActor, element);
            else
                return elementNew(sActor, dActor, element, elementValue, heal);

        }
    }
}
