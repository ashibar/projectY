using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform target; // 카메라가 따라다닐 대상의 Transform 컴포넌트
    public Vector2 offset; // 대상과 카메라 간의 오프셋
    public float padding = 0.1f; // 대상과 화면 경계 사이의 여유 공간

    private Camera mainCamera; // 메인 카메라 컴포넌트

    private void Start()
    {
        target = Player.Instance.transform;
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        mainCamera.transform.position = new Vector3(target.position.x, target.position.y, mainCamera.transform.position.z);
        //if (target == null)
        //    return;

        //// 대상의 위치에 오프셋을 적용하여 카메라의 목표 위치 계산
        //Vector3 targetPosition = target.position + new Vector3(offset.x, offset.y, transform.position.z);

        //// 카메라의 뷰포트를 화면 좌표로 변환
        //Vector3 screenPosition = mainCamera.WorldToViewportPoint(targetPosition);

        //// 화면 경계값 계산
        //float minX = 0.5f - padding;
        //float maxX = 0.5f + padding;

        //// 대상이 화면 경계값을 넘어서면 카메라 이동
        //if (screenPosition.x < minX || screenPosition.x > maxX)
        //{
        //    // 대상이 화면 왼쪽 경계를 넘어갔을 때
        //    if (screenPosition.x < minX)
        //        targetPosition.x = mainCamera.ViewportToWorldPoint(new Vector3(minX, screenPosition.y, screenPosition.z)).x;

        //    // 대상이 화면 오른쪽 경계를 넘어갔을 때
        //    if (screenPosition.x > maxX)
        //        targetPosition.x = mainCamera.ViewportToWorldPoint(new Vector3(maxX, screenPosition.y, screenPosition.z)).x;
        //}

        //// 카메라 위치 업데이트
        //transform.position = targetPosition;
    }
}
