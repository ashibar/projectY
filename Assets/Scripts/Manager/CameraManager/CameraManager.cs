using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform target; // ī�޶� ����ٴ� ����� Transform ������Ʈ
    public Vector2 offset; // ���� ī�޶� ���� ������
    public float padding = 0.1f; // ���� ȭ�� ��� ������ ���� ����

    private Camera mainCamera; // ���� ī�޶� ������Ʈ

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

        //// ����� ��ġ�� �������� �����Ͽ� ī�޶��� ��ǥ ��ġ ���
        //Vector3 targetPosition = target.position + new Vector3(offset.x, offset.y, transform.position.z);

        //// ī�޶��� ����Ʈ�� ȭ�� ��ǥ�� ��ȯ
        //Vector3 screenPosition = mainCamera.WorldToViewportPoint(targetPosition);

        //// ȭ�� ��谪 ���
        //float minX = 0.5f - padding;
        //float maxX = 0.5f + padding;

        //// ����� ȭ�� ��谪�� �Ѿ�� ī�޶� �̵�
        //if (screenPosition.x < minX || screenPosition.x > maxX)
        //{
        //    // ����� ȭ�� ���� ��踦 �Ѿ�� ��
        //    if (screenPosition.x < minX)
        //        targetPosition.x = mainCamera.ViewportToWorldPoint(new Vector3(minX, screenPosition.y, screenPosition.z)).x;

        //    // ����� ȭ�� ������ ��踦 �Ѿ�� ��
        //    if (screenPosition.x > maxX)
        //        targetPosition.x = mainCamera.ViewportToWorldPoint(new Vector3(maxX, screenPosition.y, screenPosition.z)).x;
        //}

        //// ī�޶� ��ġ ������Ʈ
        //transform.position = targetPosition;
    }
}
