/*
 * 파일명 : CameraController.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/5/26
 * 최종 수정일 : 2024/5/26
 * 파일 설명 : 카메라를 움직이게 하기 위한 스크립트 
 * 수정 내용 :
 * 2024/5/26 - 카메라를 움직이기 위한 작업 
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
            transform.position += v3Rotation * new Vector3(posX * -dragSpeed, posZ * -dragSpeed, 0); // 플레이어의 위치에서 카메라가 바라보는 방향에 벡터값을 적용한 상대 좌표를 차감합니다.

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minMapX, maxMapX), Mathf.Clamp(transform.position.y, minMapY, maxMapY), transform.position.z);

        }
    }
    private void OnMouseDown()
    {
        Debug.Log("클릭 ");
        if (EventSystem.current.IsPointerOverGameObject()) isUIClicked = true;
        else isUIClicked = false;
    }
}
