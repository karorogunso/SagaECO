using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaDB.Partner
{
    public enum PartnerType
    {
        ANIMAL_RIDE_NOPITEM,
HUMAN_NOPITEM,
UNDEAD_RIDE,

        ANIMAL,
        ANIMAL_RIDE,
        ANIMAL_RIDE_BREEDER,
        BIRD,
        BIRD_RIDE,
        ELEMENT,
        ELEMENT_RIDE,
        HUMAN,
        HUMAN_RIDE,
        INSECT,
        INSECT_RIDE,
        MACHINE,
        MACHINE_RIDE,
        MAGIC_CREATURE,
        MAGIC_CREATURE_RIDE,
        PLANT,
        PLANT_RIDE,
        WATER_ANIMAL,
        WATER_ANIMAL_RIDE,
    }

    public enum EnumPartnerEquipSlot
    {
        WEAPON,
        COSTUME,
    }

    public enum PartnerCubeType
    {
        CONDITION,
        ACTION,
        ACTIVESKILL,
        PASSIVESKILL,
    }
}
