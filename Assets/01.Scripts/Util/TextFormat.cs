/*
 * 파일명 : TextFormat.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/4/28
 * 최종 수정일 : 2024/5/3
 * 파일 설명 : 텍스트 관련 포맷 스크립트
 * 수정 내용 :
 * 2024/5/5 - SetTimeFormat 함수 구현
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
