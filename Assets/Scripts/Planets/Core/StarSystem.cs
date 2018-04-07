using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planets.Core{
	public class StarSystem : MonoBehaviour {

		//Public
		public Star sun = null;
		public List<Planet> planetList = new List<Planet>();
		public float bounds = 100.0f;
		public int minPlanets = 2;
		public int maxPlanets = 20;

		private float alpha = 0.0f;
		
		public void CreateSystem(){
			GameObject sunObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);

			Color sunColor = new Color(1.0f, 1.0f, 0.9f);
			Material sunMaterial = Resources.Load("Materials/Mat_Star", typeof(Material)) as Material;

			if(sunMaterial != null)
				sunObject.GetComponent<Renderer>().material = sunMaterial;
			
			Light sunLight = sunObject.AddComponent<Light>();
			sunLight.type = LightType.Point;
			sunLight.color = sunColor;
			sun = sunObject.AddComponent<Star>();
			sun.Create();

			Material planetMaterial = Resources.Load("Materials/Mat_Planet", typeof(Material)) as Material;

			int numPlanets = Random.Range(minPlanets, maxPlanets);
			float offset = sun.diameter + 30.0f;
			float space = 30.0f;
			for (int i = 0; i < numPlanets; i++)
			{
				//Create gameobject
				GameObject planetObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);

				if(planetMaterial!=null){
					planetObject.GetComponent<Renderer>().material = planetMaterial;
					planetObject.GetComponent<Renderer>().material.SetColor("_Color", new Color(Random.Range(0.5f, 1.0f), Random.Range(0.5f, 1.0f), Random.Range(0.5f, 1.0f)));
				}
				
				//Add Script to component
				Planet planet = planetObject.AddComponent<Planet>();
				planet.Create();
				planetList.Add(planet);

				float pct = i / ((float) numPlanets - 1.0f);
				pct = Mathf.Pow(pct, 10.0f);
				float step = space * (i + 1);
				
				if(i > 0){
					offset += (planetList[i - 1].diameter / 2.0f) + (planetList[i].diameter / 2.0f);
					offset += step;
				}

				//Change gameobject
				planetObject.name = "Planet" + i;
				planet.distance = offset;
				planet.translationSpeed = Random.Range(2.0f, 5.0f) / planet.distance; 
				planetObject.transform.position = new Vector3(0.0f, 0.0f, offset);

				//Add child :: inverso aqui vc adiciona esse object aao pai
				planetObject.transform.parent = gameObject.transform;
			}
			bounds = sun.diameter + offset;
			sunLight.range = bounds;
			sunObject.name = "Sun";

			//Add child
			sunObject.transform.parent = gameObject.transform;
		}

		void Update() {

			alpha += 30.0f * Time.deltaTime;
			int totalItens = planetList.Count;
			for (int i = 0; i < totalItens; i++)
			{
				float distance = planetList[i].distance;
				float speed = planetList[i].translationSpeed;
				float x = sun.gameObject.transform.position.x + (distance * Mathf.Cos(alpha * speed));
     			float z = sun.gameObject.transform.position.z + (distance * 0.8f * Mathf.Sin(alpha * speed));

				planetList[i].gameObject.transform.position = new Vector3(x, 0.0f, z);
			}
		}
	}
}
