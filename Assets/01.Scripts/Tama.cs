/*
 * ���ϸ� : Tama.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/4/11
 * ���� ������ : 2024/5/3
 * ���� ���� : Ÿ��(������ ĳ���͵�)
 * ���� ���� :
 * 2024/4/11 - ��ũ��Ʈ �ۼ�
 * 2024/5/3 - ��ü���� ��ũ��Ʈ ����(�ڵ� ���� ������Ƽ�� ���� �� region �ۼ�)
 */
using UnityEngine;

[System.Serializable]
public class Tama : MonoBehaviour
{
    #region Variables
    [SerializeField] private TamaStat stat;
    //TamaOutfitPart outfit; //���� �۾�
    #endregion

    #region Methods
    public void SetTama(TamaStat stat)
    {
        this.stat = stat;
        Debug.Log("Setted Tama Data");
    }
    #endregion
}
