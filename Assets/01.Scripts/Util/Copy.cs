/*
 * 파일명 : Copy.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/4/30
 * 최종 수정일 : 2024/5/3
 * 파일 설명 : 클래스의 DeepCopy를 위한 스크립트
 * 수정 내용 :
 * 2024/4/30 - 스크립트 작성
 * 2024/5/3 - 전체적인 스크립트 정리(자동 구현 프로퍼티로 수정 및 region 작성)
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
