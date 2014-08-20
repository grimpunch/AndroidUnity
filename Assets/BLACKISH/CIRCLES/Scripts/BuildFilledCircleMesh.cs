///////////////////////////////////////////////
////     BuildFilledCircleMesh v1.0        ////
////  copyright (c) 2011 by Markus Hofer   ////
////      BLACKISH - GameAssets.net        ////
///////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class BuildFilledCircleMesh : MonoBehaviour {
	
	public int elements = 16; //how many quads should the full circle consist of?
	private int savedElements = 0;
	public float radius = 50f; //Radius of the circle
	private float savedRadius = 0f;
	
	private Vector3[] allVertices = new Vector3[0];
	private Vector2[] allUVs = new Vector2[0];
	private int[] allTriangles = new int[0];
	
	public Vector2 uv1 = new Vector2(0f, 0f);
	public Vector2 uv2 = new Vector2(0f, 1f);
	
	public Mesh mesh;
	
	private bool busy = false;
	
	
	
	void RecalculateMesh () {
		
		if(busy) return;
		
		busy = true;
		savedRadius = radius;
		savedElements = elements;

		if(elements <= 2) {
			Debug.LogWarning("Number of elements can't be < 3", gameObject);
			elements = 3;
		}
		
		allVertices = new Vector3[elements + 1];
		allUVs = new Vector2[elements + 1];
		allTriangles = new int[elements * 6];			

		if(!gameObject.GetComponent("MeshFilter")) gameObject.AddComponent("MeshFilter");
		if(!gameObject.GetComponent("MeshRenderer")) gameObject.AddComponent("MeshRenderer");
       	if(!mesh) mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();
	
		allVertices[0] = Vector3.zero;
		allUVs[0] = uv1;
		
		float degreeStep = 360f / elements;
		float deg = 0f;

		Quaternion quat = Quaternion.identity;
		
		for(int i = 1; i <= elements; i++) {
			quat = Quaternion.AngleAxis(deg, -Vector3.forward);
			
			allVertices[i] = quat * new Vector3(0f, radius, 0f);
			allUVs[i] = uv2;
			
			allTriangles[(i-1) * 3] = 0;
			allTriangles[(i-1) * 3 + 1] = i; 
			if(i+1 <= elements) allTriangles[(i-1) * 3 + 2] = i+1;
			else allTriangles[(i-1) * 3 + 2] = 1; //last triangle connects to first point
			
			deg += degreeStep;
		}
	
 
        mesh.vertices = allVertices;
        mesh.uv = allUVs;
        mesh.triangles = allTriangles;
		mesh.bounds = new Bounds(Vector3.zero, new Vector3(radius*2f, 0.1f, radius*2f));
	
		busy = false;
	
	}
	

	void Update () {
		if(radius != savedRadius || elements != savedElements) {
			RecalculateMesh();
		}
	}
	
	
	void OnDrawGizmosSelected() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, radius);
	}
	
}
