/*
 * 파일명 : AttackComponent.cs
 * 작성자 : 윤주호 
 * 작성일 : 2024/7/2
 * 최종 수정일 : 2024/9/29
 * 파일 설명 : 움직임이 가능한 Entity관련 무브 관리 스크립트
 * 수정 내용 :
 * 2024/7/2 - 패스에 맞춰 움직일 수 있도록 수정
 * 2024/9/29 - 도중에 움직임을 멈출 수 있도록 StopMoving함수 추가
 */
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    private float speed;
    private List<Vector3> path;
    private int currentPathIndex;
    private bool isMoving;
    private Pathfinding pathfinding;
    private SpriteRenderer spriteRenderer;
    private AnimationController animationController;
    public void Initialize(IMovable moveableEntity)
    {
        speed = moveableEntity.MoveSpeed;
        pathfinding = new Pathfinding(BuildManager.Instance.GridLayout, BuildManager.Instance.MainTilemap, BuildManager.tileBases);
        Animator animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (animator != null)
        {
            animationController = new AnimationController(animator);
        }
    }

    private void Update()
    {
        if (isMoving)
        {
            MoveAlongPath();
        }
    }

    public void MoveTo(Vector3 destination)
    {
        path = pathfinding.FindPath(transform.position, destination);
        if (path!= null && path.Count > 0)
        {
            isMoving = true;
            currentPathIndex = 0;
            animationController?.PlayMoveAnimation();
        }
    }
    private void MoveAlongPath()
    {
        if (currentPathIndex < path.Count)
        {
            Vector3 targetPosition = path[currentPathIndex];
            Vector3 direction = (targetPosition - transform.position).normalized;

            // Sprite 방향 조정
            if (spriteRenderer != null)
            {
                spriteRenderer.flipX = direction.x > 0;
            }

            transform.position += direction * speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                currentPathIndex++;
            }
        }
        else
        {
            isMoving = false;
            animationController?.PlayIdleAnimation();
        }
    }
    public void StopMove()
    {
        isMoving = false;
    }
}
