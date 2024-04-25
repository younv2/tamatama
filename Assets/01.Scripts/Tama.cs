using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tama : MonoBehaviour
{
    [SerializeField] TamaStat stat;
    //TamaOutfitPart outfit; //추후 작업


    public void SetTama(TamaStat stat)
    {
        this.stat = stat;
        Debug.Log("Setted Tama Data");
    }
}
