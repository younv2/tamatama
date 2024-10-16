/*
 * 파일명 : EquipmentStat.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/10/16
 * 최종 수정일 : 2024/10/16
 * 파일 설명 :  장비 스테이터스 스크립트
 * 수정 내용 :
 * 2024/10/16 - 스크립트 작성
 */
[System.Serializable]
public class EquipmentStat
{
    public int AttackPower { get; set; }
    public float AttackSpeed { get; set; }
    public float AttackRange { get; set; }

    public int Defense { get; set; }
    public float MoveSpeed { get; set; }
    public float CriticalChance { get; set; }
    public float CriticalDamage { get; set; }

    public EquipmentStat(int attackPower, float attackSpeed, float attackRange, int defense,float moveSpeed, float critChance, float critDamage)
    { 
        AttackPower = attackPower;
        AttackSpeed = attackSpeed;
        AttackRange = attackRange;
        Defense = defense;
        MoveSpeed = moveSpeed;
        CriticalChance = critChance;
        CriticalDamage = critDamage;
    }
}
