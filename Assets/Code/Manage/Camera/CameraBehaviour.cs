using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manage.Camera
{
    public class CameraBehaviour : MonoBehaviour
    {

        public float Smooth = 10.0F;
        public float Speed = 0.01F;

        public float XAxisValue = 0.0F;
        public float YAxisValue = 0.0F;

        public float XRotationValue = -30.0F;
        public float YRotationValue = -30.0F;

        public float CameraZoomTarget = 40F;
        public float CameraZoomSpeed = 50.0F;
        public float RotationSpeed = 10.0F;

        public float MaxCameraZoomout = 50.0F;

        public UnityEngine.Camera Camera;
        public Transform anchor;

        void Start()
        {
            XAxisValue = anchor.position.x;
            YAxisValue = anchor.position.z;
        }

        void Update()
        {
            XAxisValue += (Input.GetAxis("Horizontal")* Mathf.Cos(anchor.localEulerAngles.y* 0.0174532925f) + Input.GetAxis("Vertical") * Mathf.Sin(anchor.localEulerAngles.y* 0.0174532925f)) * Speed * (CameraZoomTarget+10);
            YAxisValue += (Input.GetAxis("Vertical") * Mathf.Cos(anchor.localEulerAngles.y* 0.0174532925f) + Input.GetAxis("Horizontal") * Mathf.Sin(-anchor.localEulerAngles.y* 0.0174532925f)) * Speed * (CameraZoomTarget + 10);

            CameraZoomTarget = CameraZoomTarget - Input.GetAxis("Mouse ScrollWheel") * CameraZoomSpeed;
            if (CameraZoomTarget < 1)
            {
                CameraZoomTarget = 1f;
            }
            if (CameraZoomTarget > MaxCameraZoomout)
            {
                CameraZoomTarget = MaxCameraZoomout;
            }

            float cameraYPosition = 1.5f;
            RaycastHit hit;
            if (Physics.Raycast(transform.position + new Vector3(0,100,0), Vector3.down, out hit, 500f))
            {
                cameraYPosition = 1.5f+hit.point.y;
            }
            anchor.localPosition = Vector3.Slerp(anchor.localPosition,
                                            new Vector3(XAxisValue,
                                                        cameraYPosition,
                                                        YAxisValue),
                                            Time.deltaTime * Smooth);
            transform.localPosition = Vector3.Slerp(transform.localPosition,
                                                    new Vector3(transform.localPosition.x,
                                                                CameraZoomTarget,
                                                                transform.localPosition.z),
                                                   Time.deltaTime * Smooth);
            if (Input.GetAxis("Middle Mouse Button")>0)
            {
                YRotationValue += Input.GetAxis("Mouse X")* RotationSpeed;
                XRotationValue += Input.GetAxis("Mouse Y")* RotationSpeed;
                YRotationValue %= 360;
                XRotationValue %= 360;
                if (XRotationValue < -90)
                {
                    XRotationValue =-90;
                }
                else if (XRotationValue > 0)
                {
                    XRotationValue = 0;
                }
            }
            anchor.localEulerAngles = new Vector3(XRotationValue,
                                             YRotationValue,
                                             anchor.localEulerAngles.z);
        }

        public void SetTarget(Vector3 position)
        {
            XAxisValue = position.x;
            YAxisValue = position.z;
        }
    }
}