using UnityEngine;
using System.Collections;

public class pulsingLights : MonoBehaviour 
{
	void Start () 
	{
		if(ufoMaterial)
		{	
			ufoMaterial.EnableKeyword("_EMISSION");
			saveOriginalColor=ufoMaterial.GetColor("_EmissionColor");
		}
	}
	

	void Update () 
	{
	 	if(ufoMaterial)
		{
			ufoLightIntensity=Mathf.PingPong(Time.time*0.5f, 0.8f);
			ufoLightIntensity+=0.2f;
			ufoMaterial.SetColor("_EmissionColor", new Color(ufoLightIntensity, ufoLightIntensity, ufoLightIntensity, 1.0f));
		}
	}

	void OnApplicationQuit()
	{
		if(ufoMaterial) ufoMaterial.SetColor("_EmissionColor", saveOriginalColor);
	}	

	public Material ufoMaterial;
	private float ufoLightIntensity;
	private Color saveOriginalColor;
}

