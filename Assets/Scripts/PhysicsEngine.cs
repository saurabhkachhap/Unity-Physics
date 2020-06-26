using System.Collections.Generic;
using UnityEngine;

public class PhysicsEngine : MonoBehaviour
{
	public float mass;				 // [kg]
	public Vector3 velocityVector;   // [m/s]
    public Vector3 netForceVector;	 // N [kg.m/s^2]
    private List<Vector3> forceVectorList = new List<Vector3>();

    private void Start()
    {
		SetupThrustTrails();
	}

    private void FixedUpdate()
    {
		RenderThrust();		
		UpdatePosition();
    }

    private void UpdatePosition()
    {
		netForceVector = Vector3.zero;
		for (int i = 0; i < forceVectorList.Count; i++)
		{
			netForceVector += forceVectorList[i];
		}
		forceVectorList.Clear();
		
		var accelerationVector = netForceVector / mass;
        velocityVector += accelerationVector * Time.deltaTime;
        transform.position += velocityVector * Time.deltaTime;
    }

    public void AddForce(Vector3 forceVector)
    {
        forceVectorList.Add(forceVector);
    }

	public bool showTrails = true;
	private LineRenderer lineRenderer;
	private int numberOfForces;

	void SetupThrustTrails()
	{
		forceVectorList = GetComponent<PhysicsEngine>().forceVectorList;

		lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
		lineRenderer.SetColors(Color.yellow, Color.yellow);
		lineRenderer.SetWidth(0.2F, 0.2F);
		lineRenderer.useWorldSpace = false;
	}

	// Update is called once per frame
	void RenderThrust()
	{
		if (showTrails)
		{
			lineRenderer.enabled = true;
			numberOfForces = forceVectorList.Count;
			lineRenderer.SetVertexCount(numberOfForces * 2);
			int i = 0;
			foreach (Vector3 forceVector in forceVectorList)
			{
				lineRenderer.SetPosition(i, Vector3.zero);
				lineRenderer.SetPosition(i + 1, -forceVector);
				i = i + 2;
			}
		}
		else
		{
			lineRenderer.enabled = false;
		}
	}
}
