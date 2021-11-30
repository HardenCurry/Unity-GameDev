using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Rot : MonoBehaviour
{
    public float sens;
    public Transform Player;
    float M_X;
    float M_Y;
    float R_X;
    // Update is called once per frame
    void Update()
    {
        M_X = Input.GetAxis("Mouse X") * sens * Time.deltaTime;
        M_Y = Input.GetAxis("Mouse Y") * sens * Time.deltaTime;

        R_X -= M_Y;
        R_X = Mathf.Clamp(R_X, -90, 70);
        transform.localRotation = Quaternion.Euler(R_X, 0, 0);//카메라가 y축
        Player.Rotate(Vector3.up * M_X);//캐릭터가 X축
    }
}