/*
 * ���ϸ� : CameraController.cs
 * �ۼ��� : ����ȣ 
 * �ۼ��� : 2024/5/26
 * ���� ������ : 2024/5/26
 * ���� ���� : ī�޶� �����̰� �ϱ� ���� ��ũ��Ʈ 
 * ���� ���� :
 * 2024/5/26 - ī�޶� �����̱� ���� �۾� 
 */
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static Define;

public class CameraController : MonoBehaviour
{
    /*[SerializeField]
    float zoomSpeed = 300f;
    [SerializeField]
    float zoomMax = 200f;
    [SerializeField]
    float zoomMin = 1000f;*/

    //[SerializeField]
    //float rotateSpeed = -1f;
    [SerializeField]
    float dragSpeed = 0.2f;
    //[SerializeField]
    //float inputSpeed = 20;

    bool isUIClicked;


    private void LateUpdate()
    {
        if (BuildManager.Instance.GetMapState() == MapState.NONE)
        {
            CameraDrag();
        }
    }

    void CameraDrag()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            isUIClicked = false;
        }
        else if(Input.GetMouseButtonDown(0))
        {
            isUIClicked = true;
        }

        if (!isUIClicked && Input.GetMouseButton(0))
        {
            float posX = Input.GetAxis("Mouse X");
            float posZ = Input.GetAxis("Mouse Y");

            Quaternion v3Rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
            transform.position += v3Rotation * new Vector3(posX * -dragSpeed, posZ * -dragSpeed, 0); // �÷��̾��� ��ġ���� ī�޶� �ٶ󺸴� ���⿡ ���Ͱ��� ������ ��� ��ǥ�� �����մϴ�.

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minMapX, maxMapX), Mathf.Clamp(transform.position.y, minMapY, maxMapY), transform.position.z);

        }
    }
    private void OnMouseDown()
    {
        Debug.Log("Ŭ�� ");
        if (EventSystem.current.IsPointerOverGameObject()) isUIClicked = true;
        else isUIClicked = false;
    }
}
