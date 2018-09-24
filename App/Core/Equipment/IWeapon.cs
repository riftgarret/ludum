using App.Core.Stats;

namespace App.Core.Equipment
{
    public interface IWeapon : IEquipment
    {
        ElementVector DamageMin { get; }
        ElementVector DamageMax { get; }
        AttributeVector AttributeScaling { get; }
    }


}