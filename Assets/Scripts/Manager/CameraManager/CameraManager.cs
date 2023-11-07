using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraManager : MonoBehaviour, IEventListener
{
    private static CameraManager instance;
    public static CameraManager Instance
    {
        get
        {
            if (instance == null) // instance가 비어있다
            {
                var obj = FindObjectOfType<CameraManager>();
                if (obj != null)
                {
                    instance = obj;                                             // 전체 찾아봤는데? 있네? 그걸 넣자
                }
                else
                {
                    var newObj = new GameObject().AddComponent<CameraManager>(); // 전체 찾아봤는데? 없네? 새로만들자
                    instance = newObj;
                }
            }
            return instance; // 안비어있네? 그냥 그대로 가져와
        }
    }
    
    public Transform target; // 카메라가 따라다닐 대상의 Transform 컴포넌트
    public Vector2 offset; // 대상과 카메라 간의 오프셋
    public float padding = 0.1f; // 대상과 화면 경계 사이의 여유 공간

    private Camera mainCamera; // 메인 카메라 컴포넌트
    private PostProcessVolume ppVolume;
    private PostProcessProfile ppProfile;

    [SerializeField] private CameraMode camera_mode;
    [SerializeField] private float default_size;

    public CameraMode Camera_mode { get => camera_mode; set => camera_mode = value; }

    public static List<string> event_code = new List<string>
    {
        "Change Camera Mode",
        "Zoom At Vector",
        "Camera Distortion",
        "Camera Move At Vector",
        "Camera Move At Vector Instant",
    };

    private void Awake()
    {
        var objs = FindObjectsOfType<CameraManager>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        SubscribeEvent();
        mainCamera = Camera.main;
        ppVolume = GetComponent<PostProcessVolume>();
        ppProfile = ppVolume.profile;
        default_size = mainCamera.fieldOfView;
    }

    private void Start()
    {
        target = Player.Instance.transform;
        
    }

    private void LateUpdate()
    {
        switch (camera_mode)
        {
            case CameraMode.None:
                Mode_None(); break;
            case CameraMode.Fix:
                Mode_Fix(); break;
            case CameraMode.Flexible:
                Mode_Flexible(); break;
        }
    }

    public void SubscribeEvent()
    {
        foreach (string code in event_code)
            EventManager.Instance.AddListener(code, this);
    }

    public void OnEvent(string event_type, Component sender, Condition condition, params object[] param)
    {
        ExtraParams para = (ExtraParams)param[0];

        switch (event_type)
        {
            case "Change Camera Mode":
                ChangeCameraMode(para); break;
            case "Zoom At Vector": // float(지속 시간), int(줌 속도), vecList[0](줌 위치)
                ZoomAtVector(para); break;
            case "Camera Distortion":
                CameraDistortion(para); break;
            case "Camera Move At Vector":
                MoveAtVector(para); break;
            case "Camera Move At Vector Instant":
                CameraMoveAtVectorInstant(para); break;
        }
    }

    private void ChangeCameraMode(ExtraParams para)
    {
        CameraMode[] enumValues = (CameraMode[])Enum.GetValues(typeof(CameraMode));
        int count = enumValues.Length;

        camera_mode = (CameraMode)Mathf.Clamp(para.Intvalue, 0, count - 1);
    }

    private CancellationTokenSource cts;

    private void ZoomAtVector(ExtraParams para)
    {
        cts = new CancellationTokenSource();

        ZoomAtVector_routine(para, cts.Token);
    }

    private async void ZoomAtVector_routine(ExtraParams para, CancellationToken _cts)
    {
        float end = Time.time + para.Floatvalue;
        float spd = para.Intvalue;
        Vector3 targetVec = new Vector3(para.VecList[0].x, para.VecList[0].y, mainCamera.transform.position.z);
        Vector3 dir = (targetVec - mainCamera.transform.position).normalized;
        Debug.Log("routine_start");
        while (Time.time < end && !_cts.IsCancellationRequested)
        {
            //Debug.Log(string.Format("{0}, {1}", Time.time, end));
            Vector3 camPos = mainCamera.transform.position;
            mainCamera.transform.position = Vector3.MoveTowards(camPos, camPos + dir, spd * Time.deltaTime);
            mainCamera.fieldOfView = Mathf.MoveTowards(mainCamera.fieldOfView, mainCamera.fieldOfView - 1, spd * 3 * Time.deltaTime);
            await Task.Yield();
        }

    }

    private void MoveAtVector(ExtraParams para)
    {
        cts = new CancellationTokenSource();

        MoveAtVector_routine(para, cts.Token);
    }

    private float limit = 20f;

    private async void MoveAtVector_routine(ExtraParams para, CancellationToken _cts)
    {
        float end = Time.time + limit;
        float spd = para.Floatvalue;
        Vector3 targetVec = new Vector3(para.VecList[0].x, para.VecList[0].y, mainCamera.transform.position.z);
        Vector3 dir = (targetVec - mainCamera.transform.position).normalized;
        Debug.Log("routine_start");
        while (Time.time < end && Vector3.Distance(mainCamera.transform.position, targetVec) > 0.1f && !_cts.IsCancellationRequested)
        {
            Debug.Log(string.Format("{0}, {1}", Time.time, end));
            Vector3 camPos = mainCamera.transform.position;
            mainCamera.transform.position = Vector3.MoveTowards(camPos, camPos + dir, spd * Time.deltaTime);
            await Task.Yield();
        }
    }

    private void CameraDistortion(ExtraParams para) {
        LensDistortion lensDistortion;
        if (ppProfile.TryGetSettings(out lensDistortion)){
            lensDistortion.active = para.Boolvalue;
            mainCamera.fieldOfView = para.Boolvalue ? default_size - 20 : default_size;
        }
    }

    private void CameraMoveAtVectorInstant(ExtraParams para)
    {
        mainCamera.transform.position = new Vector3(para.VecList[0].x, para.VecList[0].y, mainCamera.transform.position.z);
    }

    private void Mode_None()
    {

    }

    private void Mode_Fix()
    {
        if (target == null) return;
        mainCamera.transform.position = new Vector3(target.position.x, target.position.y, mainCamera.transform.position.z);
    }

    private void Mode_Flexible()
    {
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

    private void OnDestroy()
    {
        cts?.Cancel();
    }
}

public enum CameraMode
{
    None,
    Fix,
    Flexible,
}
