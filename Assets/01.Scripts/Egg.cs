using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Egg 
{
    [SerializeField] double remainTime;
    [SerializeField] double totalTime;
    bool? isHatched;
    public bool? IsHatched { get { return isHatched; } } 
    public IEnumerator StartHatching()
    {
        isHatched = false;
        while (remainTime > 0)
        {
            remainTime -= 1;
            yield return new WaitForSecondsRealtime(1);
        }
        isHatched = true;
        Debug.Log("ÇØÄª ¿Ï·á");
        //GameManager.Instance.user.AddTama(new Tama(new TamaStat()));
    }
    public void SetHatchingTime(double time)
    {
        isHatched = null;   
        this.totalTime = time;
        this.remainTime = time;
    }
}
