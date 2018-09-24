namespace App.Core.Equipment
{
    public interface IArmor : IEquipment
    {
        ArmorPosition ArmorPosition { get; }
        ArmorType ArmorType { get; }
    }



}