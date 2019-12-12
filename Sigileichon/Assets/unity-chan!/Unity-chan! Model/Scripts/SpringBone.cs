
using UnityEngine;
using System.Collections;

namespace UnityChan
{
	public class SpringBone : MonoBehaviour
	{
		
		public Transform child;

		
		public Vector3 boneAxis = new Vector3 (-1.0f, 0.0f, 0.0f);
		public float radius = 0.05f;

		
		public bool isUseEachBoneForceSettings = false; 

		
		public float stiffnessForce = 0.01f;

		
		public float dragForce = 0.4f;
		public Vector3 springForce = new Vector3 (0.0f, -0.0001f, 0.0f);
		public SpringCollider[] colliders;
		public bool debug = true;
		
		public float threshold = 0.01f;
		private float springLength;
		private Quaternion localRotation;
		private Transform trs;
		private Vector3 currTipPos;
		private Vector3 prevTipPos;
		
		private Transform org;
		
		

		

		
		

		private void Start ()
		{
			springLength = Vector3.Distance (trs.position, child.position);
			currTipPos = child.position;
			prevTipPos = child.position;
		}

		public void UpdateSpring ()
		{
			
			org = trs;
			
			trs.localRotation = Quaternion.identity * localRotation;

			float sqrDt = Time.deltaTime * Time.deltaTime;

			
			Vector3 force = trs.rotation * (boneAxis * stiffnessForce) / sqrDt;

			
			force += (prevTipPos - currTipPos) * dragForce / sqrDt;

			force += springForce / sqrDt;

			
			Vector3 temp = currTipPos;

			
			currTipPos = (currTipPos - prevTipPos) + currTipPos + (force * sqrDt);

			
			currTipPos = ((currTipPos - trs.position).normalized * springLength) + trs.position;

			
			for (int i = 0; i < colliders.Length; i++) {
				if (Vector3.Distance (currTipPos, colliders [i].transform.position) <= (radius + colliders [i].radius)) {
					Vector3 normal = (currTipPos - colliders [i].transform.position).normalized;
					currTipPos = colliders [i].transform.position + (normal * (radius + colliders [i].radius));
					currTipPos = ((currTipPos - trs.position).normalized * springLength) + trs.position;
				}


			}

			prevTipPos = temp;

		}

		private void OnDrawGizmos ()
		{
			if (debug) {
				Gizmos.color = Color.yellow;
				Gizmos.DrawWireSphere (currTipPos, radius);
			}
		}
	}
}
