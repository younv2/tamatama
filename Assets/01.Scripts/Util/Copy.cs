/*
 * ���ϸ� : Copy.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/4/30
 * ���� ������ : 2024/5/3
 * ���� ���� : Ŭ������ DeepCopy�� ���� ��ũ��Ʈ
 * ���� ���� :
 * 2024/4/30 - ��ũ��Ʈ �ۼ�
 * 2024/5/3 - ��ü���� ��ũ��Ʈ ����(�ڵ� ���� ������Ƽ�� ���� �� region �ۼ�)
 */
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class Copy
{
    #region Methods
    public static T DeepCopy<T>(T obj)
    {
        using (var ms = new MemoryStream())
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(ms, obj);
            ms.Position = 0;
            return (T)formatter.Deserialize(ms);
        }
    }
    #endregion
}
