/*
 * 파일명 : EquipmentItem.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/10/16
 * 최종 수정일 : 2024/10/16
 * 파일 설명 : 장비 아이템 스크립트
 * 수정 내용 :
 * 2024/10/16 - 스크립트 작성
 */

public enum EquipmentType {WEAPON, ARMOUR }
public enum WeaponType {NONE,SWORD, BOW, STAFF}
public enum Rarity {NONE, COMMON, UNCOMMON, RARE, UNIQUE, LEGENDARY }
[System.Serializable]
public class EquipmentItem : Item
{
    public EquipmentType EquipmentType { get; }
    public WeaponType WeaponType { get; }
    public EquipmentStat EquipmentStat { get; }

    public Rarity Rarity { get; }
    public EquipmentItem(int id,string name, string desc,string stringPath, EquipmentType equipmentType,WeaponType weaponType, EquipmentStat stat,Rarity rarity) : 
        base(id,name,desc,stringPath)
    {
        EquipmentType = equipmentType;
        WeaponType = weaponType;
        EquipmentStat = stat;
        Rarity = rarity;
    }

}
