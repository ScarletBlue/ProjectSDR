﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSBCamera : MonoBehaviour
{

	public FocusCenterStage FocusLevel;

	public List<GameObject> Players;
	//how quickly zoom in and out
	public float DepthUpdateSpeed = 5f;
	//how quickly it pans up and down having more depth
	public float AngleUpdateSpeed = 7f;
	//how quickly it moves up and down top to bottom
	public float PositionUpdateSpeed = 5f;

	//how far camera can zoom
	public float DepthMax = -10f;
	public float DepthMin = -22f;

	public float AngleMax = 11f;
	public float AngleMin = 3f;

	private float CameraEulerX;
	private Vector3 CameraPosition;
	public bool victoryCondition = false;

	// Use this for initialization
	void Start()
	{
		Players.Add(FocusLevel.gameObject);
	}

	// Update is called once per frame
	private void LateUpdate()
	{
		
		if (!victoryCondition)
		{
			CalculateCameraLocations();
			MoveCamera();
		}
	}

	public void ZoomToWinner(GameObject winner)
	{
		victoryCondition = true;
		Vector3 position = gameObject.transform.position;
		Vector3 desiredPosition = winner.transform.position;
		Vector3 targetPosition = Vector3.zero;
		targetPosition.x = Mathf.MoveTowards(position.x, desiredPosition.x, PositionUpdateSpeed * Time.deltaTime);
		targetPosition.y = Mathf.MoveTowards(position.y, desiredPosition.y, PositionUpdateSpeed * Time.deltaTime);
		targetPosition.z = Mathf.MoveTowards(position.z, desiredPosition.z, DepthUpdateSpeed * Time.deltaTime);


	} 

	private void MoveCamera()
	{
		Vector3 position = gameObject.transform.position;
		if (position != CameraPosition)
		{
			Vector3 targetPosition = Vector3.zero;
			targetPosition.x = Mathf.MoveTowards(position.x, CameraPosition.x, PositionUpdateSpeed * Time.deltaTime);
			targetPosition.y = Mathf.MoveTowards(position.y, CameraPosition.y, PositionUpdateSpeed * Time.deltaTime);
			targetPosition.z = Mathf.MoveTowards(position.z, CameraPosition.z, DepthUpdateSpeed * Time.deltaTime);
			gameObject.transform.position = targetPosition;
		}

		Vector3 localEulerAngles = gameObject.transform.localEulerAngles;
		if (localEulerAngles.x != CameraEulerX)
		{
			Vector3 targetEulerAngles = new Vector3(CameraEulerX, localEulerAngles.y, localEulerAngles.z);
			gameObject.transform.localEulerAngles = Vector3.MoveTowards(localEulerAngles, targetEulerAngles, AngleUpdateSpeed * Time.deltaTime);
		}
	}


	private void CalculateCameraLocations()
	{
		Vector3 averageCenter = Vector3.zero;
		Vector3 totalPositions = Vector3.zero;
		Bounds playerBounds = new Bounds();

		for (int i = 0; i < Players.Count; i++)
		{
			Vector3 playerPosition = Players[i].transform.position;

			if (!FocusLevel.FocusBounds.Contains(playerPosition))
			{
				float playerX = Mathf.Clamp(playerPosition.x, FocusLevel.FocusBounds.min.x, FocusLevel.FocusBounds.max.x);
				float playerY = Mathf.Clamp(playerPosition.y, FocusLevel.FocusBounds.min.y, FocusLevel.FocusBounds.max.y);
				float playerZ = Mathf.Clamp(playerPosition.z, FocusLevel.FocusBounds.min.z, FocusLevel.FocusBounds.max.z);
				playerPosition = new Vector3(playerX, playerY, playerZ);
			}

			totalPositions += playerPosition;
			playerBounds.Encapsulate(playerPosition);
		}

		averageCenter = (totalPositions / Players.Count);

		float extents = (playerBounds.extents.x + playerBounds.extents.y);
		float lerpPercent = Mathf.InverseLerp(0, (FocusLevel.HalfXBounds + FocusLevel.HalfYBounds) / 2, extents);

		float depth = Mathf.Lerp(DepthMax, DepthMin, lerpPercent);
		float angle = Mathf.Lerp(AngleMax, AngleMin, lerpPercent);

		CameraEulerX = angle;
		CameraPosition = new Vector3(averageCenter.x, averageCenter.y, depth);
	}
}
