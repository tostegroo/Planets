using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Planets.Vehicles;

namespace Planets.Core{
	public class Universe : MonoBehaviour {

		//Public
		public int minSystems = 5;
		public int maxSystems = 10;
		public List<StarSystem> starSytems = new List<StarSystem>();

        public List<Telescope> telescopeList = new List<Telescope>();//T
        public List<Probe> probetList = new List<Probe>();//T

		private bool created = false; 
		
		public void Awake()
		{
			CreateUniverse();
			CreateVehicles();//T
		}

		public void CreateUniverse()
		{
			if(!created){
				//Debug.Log("here");
				int numSystems = Random.Range(minSystems, maxSystems);
				float offset = 0.0f;
				float space = 100.0f;
				for (int i = 0; i < numSystems; i++)
				{
					//Create gameobject
					GameObject system = new GameObject();
					
					//Add Script to component
					StarSystem starSystem = system.AddComponent<StarSystem>();
					starSystem.CreateSystem();
					starSytems.Add(starSystem);

					float pct = i / ((float) numSystems - 1.0f);
					//pct = Mathf.Pow(pct, 10.0f);
					float step = space * (i + 1);
					
					if(i > 0){
						offset += (starSytems[i - 1].bounds) + (starSytems[i].bounds);
						offset += step;
					}

					//Change gameobject
					system.name = "System_" + i;
					system.transform.position = new Vector3(offset, 0.0f, 0.0f);
					system.transform.parent = gameObject.transform;
				}
				created = true;
			}
		}

		public void CreateVehicles()//T
        {
            int numTelescopes = 1;

            for (int i = 0; i < numTelescopes; i++)
            {
                //Create gameobject
                GameObject telescopeObject = GameObject.CreatePrimitive(PrimitiveType.Cube);

                //Add Script to Component
                Telescope telescope = telescopeObject.AddComponent<Telescope>();
                telescopeObject.name = "Telescope" + i;
                telescope.Create();
                telescopeList.Add(telescope);

                //!!!!!!!!!!!!!
                telescopeObject.transform.position = new Vector3(1000, 1000, 1000);

                //Add Child
                telescopeObject.transform.parent = gameObject.transform;

            }

            int numProbes = 1;

            for (int i = 0; i < numProbes; i++)
            {
                //Create gameobject
                GameObject probeObject = GameObject.CreatePrimitive(PrimitiveType.Cube);

                //Add Script to Component
                Probe probe = probeObject.AddComponent<Probe>();
                probeObject.name = "Probe" + i;
                probe.Create();
                probetList.Add(probe);

                //!!!!!!!!!!!!!
                probeObject.transform.position = Vector3.zero;

                //Add Child
                probeObject.transform.parent = gameObject.transform;

                //!!!!!!!!!!!!!
                probeObject.transform.position = new Vector3(300, 300, 300);
            }

			//probetList[0].vehicleCamera.enabled = true;
        }

		// Update is called once per frame
		void Update () {
			
		}
	}
}
