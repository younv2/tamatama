/*
 * ���ϸ� : HatchingManager.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/4/24
 * ���� ������ : 2024/5/3
 * ���� ���� : ���� �¾�� ���� �����ϴ� ��ũ��Ʈ
 * ���� ���� :
 * 2024/4/24 - ��ũ��Ʈ �ۼ�
 * 2024/5/3 - ��ü���� ��ũ��Ʈ ����(�ڵ� ���� ������Ƽ�� ���� �� region �ۼ�)
 */
public class HatchingManager : MonoSingleton<HatchingManager>
{
    #region Methods
    public void Start()
    {
        ReductionHatchingTime();
    }
    public void ReductionHatchingTime()
    {
        for (int i = 0; i < GameManager.Instance.user.Eggs.Length; i++)
        {
            Egg egg = GameManager.Instance.user.Eggs[i];
            StartCoroutine(egg.StartHatching());
        }
    }
    #endregion
}
