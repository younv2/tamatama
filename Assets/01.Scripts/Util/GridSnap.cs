using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSnap : MonoBehaviour
{
    private Grid grid;
    // Start is called before the first frame update
    void Start()
    {
        grid = FindAnyObjectByType<Grid>();
       
    }

    // Update is called once per frame
    void Update()
    {
        Vector3Int cp = grid.LocalToCell(transform.localPosition);
        transform.localPosition = grid.GetCellCenterLocal(cp);
    }
}
