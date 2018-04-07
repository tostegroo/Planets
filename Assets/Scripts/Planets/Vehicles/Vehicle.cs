using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Planets.Vehicles{
    public enum VehicleType
    {
        Telescope,
        Probe
    }

    public class Vehicle : MonoBehaviour {
        //Public
        public VehicleType vehicleType = VehicleType.Telescope;
        public string vehicleName;
        public float speed;
        public Rigidbody rb = null;
        public GameObject vehicleCamObject = null;
        public Camera vehicleCamera = null;
        public float turnSpeedCam;// Speed of camera turning when mouse moves in along an axis

        private Vector3 mouseOrigin;// Position of cursor when mouse dragging starts
        private bool isRotatingCam;
        private float zoomTarget = 0.0f;
       
        private Vector3 movement = Vector3.zero;

        private Ray ray;
        private RaycastHit hit;
        private Text velocityHUD;
        private GameObject planetHUD;
        private CanvasRenderer planetCanvas;
        private Text planetName;

        public void Init()
        {
            if(GameObject.Find("VelocityValue"))
                 velocityHUD = GameObject.Find("VelocityValue").GetComponent<Text>();
           
            if(velocityHUD != null)
                velocityHUD.text = "0";

            planetHUD = GameObject.Find("PlanetHUD");
            if(planetHUD){
                planetCanvas = planetHUD.GetComponent<CanvasRenderer>();
                planetCanvas.SetAlpha(0.0f);
            }

            if(GameObject.Find("PlanetName")){
                planetName = GameObject.Find("PlanetName").GetComponent<Text>();
                planetName.text = "";
            }
        }

        public void CreatePhysics()
        {
            rb = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
            rb.useGravity = false;
        }

        public void CreateCamera(VehicleType _vehicleType, string _vehicleName)
        {
            //Object
            vehicleCamObject = new GameObject("Camera");
            vehicleCamObject.tag = "MainCamera";
            vehicleCamera = vehicleCamObject.AddComponent<Camera>();
            vehicleCamera.fieldOfView = 35;
            vehicleCamera.farClipPlane = 10000.0f;
            vehicleCamera.nearClipPlane = 0.005f;
            zoomTarget = vehicleCamera.fieldOfView;

            //vehicleCamera.CopyFrom(Camera.main);
            
            //Camera
            vehicleCamera.name = _vehicleName + "Camera";
            Debug.Log(vehicleCamera.name);

            vehicleCamObject.transform.parent = gameObject.transform;

            switch (_vehicleType)
            {
                case VehicleType.Telescope:
                    vehicleCamera.enabled = true;
                    break;
                case VehicleType.Probe:
                    vehicleCamera.enabled = false;
                    break;
            }
        }

        void Update()
        {
            //Controlling (WASD) Vehicle
            Vector3 moveX_AD_sides = Input.GetAxis("Horizontal") * gameObject.transform.right * speed;
            Vector3 moveZ_WS_frontBehind = Input.GetAxis("Vertical") * gameObject.transform.forward * speed;
            Vector3 moveY_QE_upDown = Input.GetAxis("VerticalY") * gameObject.transform.up * speed;

            //Olhe aqui:: Sempre crie a variavel fora e atualize no update;
            movement = moveX_AD_sides + moveY_QE_upDown + moveZ_WS_frontBehind;

            if(rb){
                rb.AddForce(movement * speed);
            }
            
            if(velocityHUD != null)
                velocityHUD.text = (rb.velocity).ToString();

            if (Input.GetMouseButtonDown(1))
                Zoom(true);
                
            if (Input.GetMouseButtonUp(1))
                Zoom(false);
            
           
            //Controlling (MOUSE) the Camera
            if (!Input.GetMouseButton(0)) isRotatingCam = false;
            if (Input.GetMouseButtonDown(0))
            {
                // Get mouse origin
                mouseOrigin = Input.mousePosition;
                isRotatingCam = true;
                getInfo(true);
            }
            if (Input.GetMouseButtonUp(0))
            {
                getInfo(false);
            }

            if (isRotatingCam)
            {
                Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
                transform.RotateAround(transform.position, transform.right, -pos.y * turnSpeedCam);
                transform.RotateAround(transform.position, Vector3.up, pos.x * turnSpeedCam);
            }

            vehicleCamera.fieldOfView = Mathf.SmoothStep(vehicleCamera.fieldOfView, zoomTarget, Time.deltaTime * 10.0f);
        }

        void getInfo(bool getInfo)
        {
            if(getInfo)
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out hit, 1000.0f))
                {
                    if(planetName !=null)
                        planetName.text = hit.collider.gameObject.name;

                    if(planetCanvas !=null)
                        planetCanvas.SetAlpha(1.0f);
                }
            }
            else
            {
                if(planetCanvas !=null)
                    planetCanvas.SetAlpha(0.0f);

                    if(planetName !=null)
                    planetName.text = "";
            }
        }

        void Zoom(bool zoom)
        {
            if(zoom)
                zoomTarget = 5.0f;
            else
                zoomTarget = 35.0f;
        }
    }
}
