using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cinemachine;
using Unity.IO.LowLevel.Unsafe;

public class PlayerCameraManager : MonoBehaviour
{
    public static PlayerCameraManager instance;
    
    [BoxGroup("References")]
    public Transform cam;
    [BoxGroup("References")]
    public CinemachineVirtualCamera virtualCamera;
    [BoxGroup("References")]
    public PlayerCamera playerCamera;

    
    [BoxGroup("Camera Sensitivity")]
    [Range(0.1f, 10)]
    public float ySensitivity;
    [BoxGroup("Camera Sensitivity")]
    [Range(0.1f, 10)]
    public float xSensitivity;

    [BoxGroup("Camera Position")]
    public Vector3 camPoint;
    [BoxGroup("Camera Position")]
    public Vector3 magicCamPoint;
    [BoxGroup("Camera Position")]
    public float lerpTime;
    [BoxGroup("Camera Position")]
    public bool isMagicCam;
    [BoxGroup("Camera Position/Position")]
    [Range(-3, 3)]
    public float camPosX;
    [BoxGroup("Camera Position/Position")]
    [Range(-3, 3)]
    public float camPosY;
    [BoxGroup("Camera Position/Position")]
    [Range(-3, 3)]
    public float camPosZ;

    [BoxGroup("Camera Zoom")]
    public float zoomSpeed;
    [BoxGroup("Camera Zoom")]
    public float zoomAcceleration;
    [BoxGroup("Camera Zoom")]
    public float zoomMin;
    [BoxGroup("Camera Zoom")]
    public float zoomMax;
    [BoxGroup("Camera Zoom")]
    public float currentZoom;


    float xRotation;
    Vector3 prePos;
    Vector3 currentPos;

    public void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        currentPos = new Vector3(camPosX, camPosY, camPosZ);    
    }

    void Update() 
    {
        if(!isMagicCam){
            if(InputManager.instance.zoomValue.y < 0){
                currentZoom -= zoomSpeed * Time.deltaTime;
            }
            if(InputManager.instance.zoomValue.y > 0){
                currentZoom += zoomSpeed * Time.deltaTime;
            }
            if(currentZoom >= zoomMax){
                currentZoom = zoomMax;
            }else if(currentZoom <= zoomMin){
                currentZoom = zoomMin;
            }
            virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance = currentZoom;
        }
        
    }
    public void FixedUpdate()
    {
        if(currentPos.x != camPosX || currentPos.y != camPosY || currentPos.z != camPosZ)
        {
            currentPos = new Vector3(camPosX, camPosY, camPosZ);    
        }
        if(prePos != currentPos){
            virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>().ShoulderOffset = currentPos;
            prePos = currentPos;
        }
        
    }

    public void MagicCamera()
    {
        isMagicCam = true;
        virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance = 2;
        playerCamera.followTransform.transform.localPosition = magicCamPoint;
    }

    public void NormalCamera()
    {
        isMagicCam = false;  
        playerCamera.followTransform.transform.localPosition = Vector3.Slerp(playerCamera.followTransform.transform.position, camPoint, lerpTime);    
    }
}
