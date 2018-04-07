using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planets.Vehicles{
    public class Probe : Vehicle {
        public void Create()
        {
            vehicleType = VehicleType.Probe;
            vehicleName = GetComponent<Transform>().name;
            speed = 3.0f;
            turnSpeedCam = 4.0f;

            Init();
            CreatePhysics();
            CreateCamera(vehicleType, vehicleName);
        }
    }
}
