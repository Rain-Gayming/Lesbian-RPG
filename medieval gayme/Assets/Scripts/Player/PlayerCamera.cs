using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera instance;
    public Quaternion nextRotation;

    public float rotationPower = 3f;
    public float rotationLerp = 0.5f;
    public GameObject followTransform;

    void Awake() {
        instance = this;
    }

    private void Update()
    {
        if(!GameManager.instance.paused){
            #region Follow Transform Rotation

            //Rotate the Follow Target transform based on the input
            followTransform.transform.rotation *= Quaternion.AngleAxis(InputManager.instance.cameraLook.x * rotationPower * Time.deltaTime, Vector3.up);

            #endregion

            #region Vertical Rotation
            followTransform.transform.rotation *= Quaternion.AngleAxis(InputManager.instance.cameraLook.y * rotationPower * Time.deltaTime, Vector3.right);

            var angles = followTransform.transform.localEulerAngles;

            //Clamp the Up/Down rotation
            Mathf.Clamp(angles.x, -70, 70);

            followTransform.transform.localEulerAngles = angles;
            #endregion

            
            nextRotation = Quaternion.Lerp(followTransform.transform.rotation, nextRotation, 0);
            followTransform.transform.localEulerAngles = new Vector3(angles.x, angles.y, 0);  
        }
    }
}
