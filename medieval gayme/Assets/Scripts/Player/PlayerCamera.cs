using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera instance;
    public Quaternion nextRotation;

    public float rotationPower = 3f;
    public float rotationLerp = 0.5f;
    public GameObject followTransform;

    [BoxGroup("Settings")]
    public float xSensitivity;
    [BoxGroup("Settings")]
    public float ySensitivity;
    [BoxGroup("Settings")]
    public bool invertedX;
    [BoxGroup("Settings")]
    public bool invertedY;
    [BoxGroup("Settings")]

    void Awake() {
        instance = this;
    }
    void Start() {
        UpdateCamera();

        if(xSensitivity <= 0){
            xSensitivity = 3;
        }
        if(ySensitivity <= 0){
            ySensitivity = 3;
        }
    }

    private void Update()
    {
        if(!GameManager.instance.paused){
            #region Follow Transform Rotation

            //Rotate the Follow Target transform based on the input

            if(!invertedX){
                followTransform.transform.rotation *= Quaternion.AngleAxis(InputManager.instance.cameraLook.x * xSensitivity * Time.deltaTime, Vector3.up);
            }else{
                followTransform.transform.rotation *= Quaternion.AngleAxis(-InputManager.instance.cameraLook.x * xSensitivity * Time.deltaTime, Vector3.up);
            }

            #endregion

            #region Vertical Rotation

            if(!invertedY){
                followTransform.transform.rotation *= Quaternion.AngleAxis(InputManager.instance.cameraLook.y * ySensitivity * Time.deltaTime, Vector3.right);
            }else{
                followTransform.transform.rotation *= Quaternion.AngleAxis(-InputManager.instance.cameraLook.y * ySensitivity * Time.deltaTime, Vector3.right);
            }
            var angles = followTransform.transform.localEulerAngles;

            //Clamp the Up/Down rotation
            Mathf.Clamp(angles.x, -70, 70);

            followTransform.transform.localEulerAngles = angles;
            #endregion

            
            nextRotation = Quaternion.Lerp(followTransform.transform.rotation, nextRotation, 0);
            followTransform.transform.localEulerAngles = new Vector3(angles.x, angles.y, 0);  
        }
    }

    public void UpdateCamera()
    {
        xSensitivity = Settings.instance.saveData.gameplaySaveData.sensitivityX;
        ySensitivity = Settings.instance.saveData.gameplaySaveData.sensitivityY;
        
        invertedX = Settings.instance.saveData.gameplaySaveData.invertedX;
        invertedY = Settings.instance.saveData.gameplaySaveData.invertedY;
    }
}
