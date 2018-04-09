using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planets.Vehicles
{
    public class Telescope : Vehicle
    {
        public void Create()
        {
            vehicleType = VehicleType.Telescope;
            vehicleName = GetComponent<Transform>().name;
            speed = 0f;
            turnSpeedCam = 6.0f;

            Init();
            CreatePhysics();
            CreateCamera(vehicleType, vehicleName);
        }
    }
}
