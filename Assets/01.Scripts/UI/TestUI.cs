using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("AddTamaBtn").GetComponent<Button>().onClick.AddListener(
            () =>
            GameManager.Instance.user.AddTama());
        GameObject.Find("EggPopup").GetComponent<Button>().onClick.AddListener(
            () =>
            PopupManager.Instance.eggBornPopup.Show());
        GameObject.Find("Inventory").GetComponent<Button>().onClick.AddListener(
            () =>
            PopupManager.Instance.inventoryPopup.Show());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
