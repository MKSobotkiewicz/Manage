using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Manage.Units
{
    public class VehicleBehaviour : MonoBehaviour
    {
        public Elements Elements;

        [Range(-180,180)]
        public float Rotation = 0;
        [Range(-5, 50)]
        public float BarrelPitch = 0;
        [Range(-50, 50)]
        public float Speed = 0;
        [Range(-20, 20)]
        public float Turning = 0;

        public GameObject Target;

        private static float maxBarrelAngle = 30;
        private static float minBarrelAngle = 360-5;
        private static float traverseSpeed = 3;
        private static float turretSpeed = 3;

        private NavMeshAgent navMeshAgent;

        private Vector3 lastFacing;

        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            RotateTurretAndBarrel();
            RotateWheels();
            RotateBody();
        }

        void RotateBody()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position+ Vector3.up, Vector3.down, out hit, 5f))
            {
                UnityEngine.Debug.Log("hit.normal "+ hit.normal);
                Elements.Body.transform.rotation = Quaternion.Lerp(Elements.Body.transform.rotation ,
                                                                   Quaternion.LookRotation(Vector3.Cross(transform.right, hit.normal), hit.normal),
                                                                   Time.deltaTime*3);
            }
        }

        void RotateWheels()
        {
            Vector3 currentFacing = transform.forward;
            float currentAngularVelocity;
            if (Vector3.Cross(currentFacing, lastFacing).y<0)
            {
                currentAngularVelocity = Vector3.Angle(currentFacing, lastFacing) / Time.fixedDeltaTime;
            }
            else
            {
                currentAngularVelocity = -Vector3.Angle(currentFacing, lastFacing) / Time.fixedDeltaTime;
            }
            lastFacing = currentFacing;

            Speed = navMeshAgent.velocity.magnitude;
            Turning = currentAngularVelocity;

            if (Turning < -20)
            {
                Turning = -20;
            }

            if (Turning > 20)
            {
                Turning = 20;
            }

            foreach (var wheel in Elements.Wheels)
            {
                wheel.Rotate(new Vector3(Speed * Time.fixedDeltaTime * 40.84f,0 , 0));
            }
            foreach (var axle in Elements.Axles)
            {
                axle.localEulerAngles = new Vector3(0, Turning,0 );
            }
            if (Elements.Tracks != null)
            {
                Elements.Tracks.GetComponent<MeshRenderer>().material.SetFloat("_Speed",Speed/2);
            }
        }

        void RotateTurretAndBarrel()
        {
            if (Target == null)
            {
                return;
            }
            Vector3 turretTargetDirection = new Vector3(Target.transform.position.x - Elements.Turret.transform.position.x,
                                                        0,
                                                        Target.transform.position.z - Elements.Turret.transform.position.z);
            Vector3 barrelTargetDirection = Target.transform.position - Elements.Turret.transform.position;
            Quaternion turretToRotation = Quaternion.LookRotation(turretTargetDirection, Elements.Turret.transform.up);
            Quaternion barrelToRotation = Quaternion.LookRotation(barrelTargetDirection, Elements.Turret.transform.up);
            Elements.Turret.transform.rotation = Quaternion.Lerp(Elements.Turret.transform.rotation, turretToRotation, traverseSpeed * Time.fixedDeltaTime);
            Elements.Turret.localEulerAngles= new Vector3(0,Elements.Turret.transform.localRotation.eulerAngles.y, 0);
            Elements.Barrel.transform.rotation = Quaternion.Lerp(Elements.Barrel.transform.rotation, barrelToRotation, turretSpeed * Time.fixedDeltaTime);
            
            if (Elements.Barrel.localEulerAngles.x < 180)
            {
                Elements.Barrel.localEulerAngles = (new Vector3(Mathf.Min(maxBarrelAngle, Elements.Barrel.localEulerAngles.x), 0, 0));
            }
            else
            {
                Elements.Barrel.localEulerAngles = (new Vector3(Mathf.Max(minBarrelAngle, Elements.Barrel.localEulerAngles.x), 0, 0));
            }
        }

        public void Move(Vector3 position)
        {
            this.GetComponent<NavMeshAgent>().SetDestination(position);
        }

    }

    [System.Serializable]
    public class Elements
    {
        public Transform Body;
        public Transform Turret;
        public Transform Barrel;
        public Transform Tracks;
        public Transform[] Wheels;
        public Transform[] Axles;
    }
}