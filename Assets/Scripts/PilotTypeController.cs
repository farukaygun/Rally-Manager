using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

struct pilotSpecs
{
	/*
	 * Zemin s�rt�nme katsay�lar� i�in de�i�kenler.
	 * Extremum noktas� s�rt�nmenin maksimum oldu�u noktad�r.
	 * Asymptote noktas� s�rt�nmenin dengelendi�i noktad�r.
	 * https://docs.unity3d.com/Manual/class-WheelCollider.html
	 */
	public float extremumSlip;
	public float extremumValue;
	public float asymptoteSlip;
	public float asymptoteValue;
	/*
	 * stiffness, yukar�daki de�i�kenlerin etkisinin �arpan�d�r. 
	 * Default olarak 1'dir. 2 olarak de�i�tirilirse yukar�daki de�erlerin etkisi 2 kat�na ��kar. 
	 * Genellikle runtime s�ras�nda de�i�tirilir.
	 */
	public float stiffness;

	// Constructor
	public pilotSpecs(float extremumSlip, float extremumValue, float asymptoteSlip, float asymptoteValue, float stiffness)
	{
		this.extremumSlip = extremumSlip;
		this.extremumValue = extremumValue;
		this.asymptoteSlip = asymptoteSlip;
		this.asymptoteValue = asymptoteValue;
		this.stiffness = stiffness;
	}
}

public class PilotTypeController : MonoBehaviour
{
	public Dropdown pilotTypeDropdown;

	private WheelFrictionCurve _sidewaysFriction;
	private WheelCollider wheelCollider;

	pilotSpecs ps;

	void Awake()
	{
		pilotTypeDropdown = GameObject.Find("DropdownPilotType").GetComponent<Dropdown>();
		wheelCollider = gameObject.GetComponent<WheelCollider>();
		pilotTypeDropdown.value = 0;
	}

	// Pilotun �zelliklerini ara� �zelliklerine �evir
	public void CalculatePilotSpecs()
	{
		PilotType(pilotTypeDropdown.value);

		float newExtremumSlip = ps.extremumSlip * 0.002f; // 0-1 aras�
		float newExtremumValue = ps.extremumValue * 0.002f; // 0-1 aras�
		float newAsymtoteSlip = ps.asymptoteSlip * 0.002f; // 0-1 aras�
		float newAsymtoteValue = ps.asymptoteValue * 0.002f; // 0.75 sabit �imdilik
		float newStiffness = ps.stiffness * 0.002f; // 0-1 aras�

		_sidewaysFriction.extremumSlip = wheelCollider.sidewaysFriction.extremumSlip + newExtremumSlip;
		_sidewaysFriction.extremumValue = wheelCollider.sidewaysFriction.extremumValue + newExtremumValue;
		_sidewaysFriction.asymptoteSlip = wheelCollider.sidewaysFriction.asymptoteSlip + newAsymtoteSlip;
		_sidewaysFriction.asymptoteValue = wheelCollider.sidewaysFriction.asymptoteValue + newAsymtoteValue;
		_sidewaysFriction.stiffness = wheelCollider.sidewaysFriction.stiffness + newStiffness;

		wheelCollider.sidewaysFriction = _sidewaysFriction;
	}

	void PilotType(int value)
	{
		if (value == 0)
		{
			ps = new pilotSpecs(5f, 5f, 5f, 5f, 5f);
		}
		else if (value == 1)
		{
			ps = new pilotSpecs(7f, 7f, 7f, 7f, 7f);
		}
		else if (value == 2)
		{
			ps = new pilotSpecs(10f, 10f, 10f, 10f, 10f);
		}
	}
}
