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
    public CinemachineVirtualCamera normalVirtualCamera;
    [BoxGroup("References")]
    public CinemachineVirtualCamera magicVirtualCamera;
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
    public Vector3 velocity;

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
        if(Input.mouseScrollDelta.y < 0){
            currentZoom -= zoomSpeed * Time.deltaTime;
        }
        if(Input.mouseScrollDelta.y > 0){
            currentZoom += zoomSpeed * Time.deltaTime;
        }
        if(currentZoom >= zoomMax){
            currentZoom = zoomMax;
        }else if(currentZoom <= zoomMin){
            currentZoom = zoomMin;
        }
        normalVirtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance = currentZoom;       
        magicVirtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance = currentZoom;       
        
    }
    public void FixedUpdate()
    {
        if(currentPos.x != camPosX || currentPos.y != camPosY || currentPos.z != camPosZ)
        {
            currentPos = new Vector3(camPosX, camPosY, camPosZ);    
        }
        if(prePos != currentPos){
            normalVirtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>().ShoulderOffset = currentPos;
            prePos = currentPos;
        }
        
    }

    public void MagicCamera()
    {
        isMagicCam = true;
        normalVirtualCamera.gameObject.SetActive(false);
        magicVirtualCamera.gameObject.SetActive(true);
    }

    public void NormalCamera()
    {
        isMagicCam = false;  
        normalVirtualCamera.gameObject.SetActive(true);
        magicVirtualCamera.gameObject.SetActive(false);
    }
}
