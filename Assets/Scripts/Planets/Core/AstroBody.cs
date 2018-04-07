using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planets.Core{

	public enum AstroType{
		Star,
		Planet
	}

	public class AstroBody : MonoBehaviour {

		//Public
		public AstroType astroType = AstroType.Star;
		public float distance;
		public float rotationSpeed;
		public float translationSpeed;
		public float diameter;
		public int parentSystem;
		public float minRadius;
		public float maxRadius;

		public void SetScale(){
			diameter = Random.Range(minRadius, maxRadius);
			gameObject.transform.localScale = new Vector3(diameter, diameter, diameter);
		}

		void Update() {
			gameObject.transform.Rotate(0.0f, 50.0f * Time.deltaTime, 0.0f);
		}
	}
}

