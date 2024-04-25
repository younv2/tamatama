using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchingManager : MonoSingleton<HatchingManager>
{

    public void ReductionHatchingTime()
    {
        foreach(var egg in GameManager.Instance.user.Eggs)
        {
            if(egg.IsHatched==null)
                StartCoroutine(egg.StartHatching());
        }
    } 
}
