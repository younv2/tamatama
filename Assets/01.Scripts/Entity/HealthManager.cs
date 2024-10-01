/*
 * 파일명 : HealthManager.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/10/2
 * 최종 수정일 : 2024/10/2
 * 파일 설명 : 엔티티의 체력 관리 스크립트
 * 수정 내용 :
 * 2024/10/2 - 엔티티들의 코드 중복 해결 및 유지보수를 위한 세분화 작업
 */
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int MaxHp { get; private set; }
    public int CurHp { get; private set; }
    public bool IsDead { get; private set; }

    public void Initialize(int maxHp)
    {
        MaxHp = maxHp;
        CurHp = maxHp;
        IsDead = false;
    }

    public void TakeDamage(float damage)
    {
        CurHp -= (int)damage;
        ShowDamageUI(damage);
        if (CurHp <= 0)
        {
            Die();
        }
    }
    private void ShowDamageUI(float damage)
    {
        var go = HUDObjectPoolManager.Instance.GetGo("DamageText");
        go.GetComponent<TextMeshProUGUI>().text = damage.ToString();
        go.transform.position = transform.position + new Vector3(0, 0.1f, 0);
    }
    private void Die()
    {
        IsDead = true;
        gameObject.SetActive(false);
        Debug.Log("Character has died.");
    }
}
