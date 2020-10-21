using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camController : MonoBehaviour
{
    public Transform target;

    public float xSpeed = 60f;
    public float ySpeed = 30f;

    public int yMinLimit = -723;
    public int yMaxLimit = 877;

    private float x = 0.0f;
    private float y = 0.0f;

    public void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        Rigidbody rigidbody = GetComponent<Rigidbody>();

        // Make the rigid body not change rotation
        if (rigidbody)
            rigidbody.freezeRotation = true;
    }

    public void LateUpdate()
    {
        if (target)
        {
            x -= Input.GetAxis("Horizontal") * xSpeed * 0.02f;
            y += Input.GetAxis("Vertical") * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            //distance -= Input.GetAxis("Fire1") zoomSpd 0.02f;
            //distance += Input.GetAxis("Fire2") zoomSpd 0.02f;

            Quaternion rotation = Quaternion.Euler(y, x, 0.0f);
            //Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

            transform.rotation = rotation;
            //transform.position = position;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360.0f)
            angle += 360.0f;
        if (angle > 360.0f)
            angle -= 360.0f;
        return Mathf.Clamp(angle, min, max);
    }

}
