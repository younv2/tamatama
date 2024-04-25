using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tama : MonoBehaviour
{
    [SerializeField] TamaStat stat;
    //TamaOutfitPart outfit; //���� �۾�


    public void SetTama(TamaStat stat)
    {
        this.stat = stat;
        Debug.Log("Setted Tama Data");
    }
}
