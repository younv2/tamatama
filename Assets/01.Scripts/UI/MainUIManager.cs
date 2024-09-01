/*
 * ���ϸ� : MainUIManager.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/4/11
 * ���� ������ : 2024/5/3
 * ���� ���� : ���� ȭ�鿡���� UI ���� �����ϴ� ��ũ��Ʈ
 * ���� ���� :
 * 2024/4/11 - ��ũ��Ʈ �ۼ�
 * 2024/5/3 - ��ü���� ��ũ��Ʈ ����(�ڵ� ���� ������Ƽ�� ���� �� region �ۼ�)
 */

using UnityEngine;
using UnityEngine.UI;

public class MainUIManager : MonoBehaviour
{
    #region Variables
    private Button settingBtn;
    // Start is called before the first frame update
    #endregion

    #region Methods
    void Start()
    {
        settingBtn = transform.Find("SettingBtn").GetComponent<Button>();
        settingBtn.onClick.AddListener(() => { UIManager.Instance.settingPopup.Show(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion
}
