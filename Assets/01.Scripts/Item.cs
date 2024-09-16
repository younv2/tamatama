/*
 * 파일명 : Item.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/4/11
 * 최종 수정일 : 2024/5/11
 * 파일 설명 : 아이템 관련 스크립트(최상위)
 * 수정 내용 :
 * 2024/4/11 - 스크립트 작성
 * 2024/4/30 - 아이템 클래스 세분화
 * 2024/5/3 - 전체적인 스크립트 정리(자동 구현 프로퍼티로 수정 및 region 작성)
 * 2024/5/11 - 저장이 제대로 되지않아 Newtonsoft.Json으로 수정 및 해당 라이브러리에 맞게 수정
 */

using Newtonsoft.Json;
using System.Text.RegularExpressions;

[System.Serializable]
public class Item
{
    #region Variables
    #endregion

    #region Properties
    public int Id { get; set; }
    [JsonIgnore] public string Name { get; }
    [JsonIgnore] public string Desc { get; private set; }
    [JsonIgnore] public string SpritePath { get; }
    #endregion

    #region Constructor
    [JsonConstructor]
    public Item(int id)
    {
        Id = id;
        Item temp = DataManager.Instance.ItemLst.Find(x => x.Id == id); 
        Name = temp.Name;
        Desc = temp.Desc;
        SpritePath = temp.SpritePath;
    }
    public Item(int id, string name, string desc,string spritePath)
    {
        Id = id;
        Name = name;
        Desc = desc;
        SpritePath = spritePath;
    }
    #endregion
}

//사용할 수 있는 아이템 인터페이스
public interface IUseable
{
    bool Use(int amount);
}