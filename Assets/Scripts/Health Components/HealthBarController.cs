using UnityEngine;
using System.Collections;

public class HealthBarController : MonoBehaviour {

	[SerializeField]
	private GameObject bar;

	public void setHealthBar(float percentage)
	{
		float checkedPercentage = Mathf.Clamp(percentage, 0.0f, 1.0f);

		bar.transform.localScale = new Vector3(checkedPercentage, bar.transform.localScale.y, bar.transform.localScale.z);
	}
}
