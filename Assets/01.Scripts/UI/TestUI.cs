/*
 * ���ϸ� : TestUI.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/4/24
 * ���� ������ : 2024/5/3
 * ���� ���� : ������ Ÿ�� ��Ȳ �� ������ �ΰ����� �׽�Ʈ�� ���� UI ��ũ��Ʈ
 * ���� ���� :
 * 2024/4/24 - ��ũ��Ʈ �ۼ�
 * 2024/5/3 - ��ü���� ��ũ��Ʈ ����(�ڵ� ���� ������Ƽ�� ���� �� region �ۼ�)
 */
using UnityEngine;
using UnityEngine.UI;

public class TestUI : MonoBehaviour
{
    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("AddTamaBtn").GetComponent<Button>().onClick.AddListener(
            () =>
            GameManager.Instance.user.AddTama());
        GameObject.Find("EggPopup").GetComponent<Button>().onClick.AddListener(
            () =>
            PopupManager.Instance.eggHatchPopup.Show());
        GameObject.Find("Inventory").GetComponent<Button>().onClick.AddListener(
            () =>
            PopupManager.Instance.inventoryPopup.Show());
    }
    #endregion
}
