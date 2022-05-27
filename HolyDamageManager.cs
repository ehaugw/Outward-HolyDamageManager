using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BepInEx;

namespace HolyDamageManager
{
    [BepInPlugin(GUID, NAME, VERSION)]
    public class HolyDamageManager : BaseUnityPlugin
    {

        public const string GUID = "com.ehaugw.holydamagemanager";
        public const string VERSION = "1.0.0";
        public const string NAME = "Holy Damage Manager";

        private static DamageType.Types m_holyDamageType;
        private static Tag m_holyDamageTag;

        internal void Awake() {
            SetHolyType(DamageType.Types.Electric);
            SetHolyTag(Tag.None);
        }

        public static void SetHolyType(DamageType.Types type)
        {
            m_holyDamageType = type;
        }

        public static void SetHolyTag(Tag tag)
        {
            m_holyDamageTag = tag;
        }

        public static float BuffHolyDamageOrHealing(Character character, float value)
        {
            var damageList = new DamageList(GetDamageType(), value);
            character.Stats.GetAmplifiedDamage(new Tag[] { GetDamageTag() }, ref damageList);
            return damageList.TotalDamage;

            //return (character.Stats.GetStatCurrentValue(GetDamageTag())) * value;

        }

        public static Tag GetDamageTag()
        {
            return m_holyDamageTag;
        }

        public static DamageType.Types GetDamageType()
        {
            return m_holyDamageType;
        }

        public static int GetHolyIndex()
        {
            return (int) GetDamageType();
        }

        public static float[] GetHolyDamageBonusArray(float damageBonus)
        {
            float[] damageBonuses = new float[]
            {
                0,  0,  0,
                0,  0,  0,
                0,  0,  0
            };
            damageBonuses[HolyDamageManager.GetHolyIndex()] = damageBonus;

            return damageBonuses;
        }
    }
}
