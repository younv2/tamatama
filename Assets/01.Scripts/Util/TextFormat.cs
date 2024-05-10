/*
 * ���ϸ� : TextFormat.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/4/28
 * ���� ������ : 2024/5/3
 * ���� ���� : �ؽ�Ʈ ���� ���� ��ũ��Ʈ
 * ���� ���� :
 * 2024/5/5 - SetTimeFormat �Լ� ����
 */
public static class TextFormat
{
    
    public static string SetTimeFormat(int sec)
    {
        string temp;
        int hour = sec / 3600;
        int minute = sec / 60;
        int second = sec % 60;
        temp = string.Format("{0:D2}", hour) + ":" + string.Format("{0:D2}", minute) + ":" + string.Format("{0:D2}", second);

        return temp;
    }
}
