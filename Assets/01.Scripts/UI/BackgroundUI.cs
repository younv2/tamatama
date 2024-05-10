/*
 * ���ϸ� : BackgroundUI.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/4/11
 * ���� ������ : 2024/5/3
 * ���� ���� : �� ��Ī �˾������� �� ������ Ȱ��ȭ �Ǿ����� �� Ŭ�� �� ��� 
 *             ���� Ȱ��ȭ �Ǿ� �ִ� ���� ������ �����ִ� �˾� ��ũ��Ʈ
 * ���� ���� :
 * 2024/4/11 - ��ũ��Ʈ �ۼ�
 * 2024/5/3 - ��ü���� ��ũ��Ʈ ����(�ڵ� ���� ������Ƽ�� ���� �� region �ۼ�)
 */

using TMPro;

public class BackgroundUI : MonoSingleton<BackgroundUI>
{
    #region Variables
    private TextMeshProUGUI goldText;
    private TextMeshProUGUI cashText;
    private TextMeshProUGUI nameText;

    User user;
    #endregion

    #region Methods
    public void Init()
    {
        user = GameManager.Instance.user; 
        goldText = transform.Find("Gold").GetChild(0).GetComponent<TextMeshProUGUI>();
        cashText = transform.Find("Cash").GetChild(0).GetComponent<TextMeshProUGUI>();
        nameText = transform.Find("Name").GetComponent<TextMeshProUGUI>();

        goldText.text = user.Inventory.Gold.ToString();
        cashText.text = user.Inventory.Cash.ToString();
        nameText.text = user.Nickname.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        goldText.text = user.Inventory.Gold.ToString();
        cashText.text = user.Inventory.Cash.ToString();
        nameText.text = user.Nickname.ToString();
    }
    #endregion
}
