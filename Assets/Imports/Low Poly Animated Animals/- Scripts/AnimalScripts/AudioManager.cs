using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LowPolyAnimalPack
{
  public class AudioManager : MonoBehaviour
  {
    private static AudioManager instance;

    [SerializeField]
    private float soundDistance = 7f;

    [SerializeField]
    private bool logSounds = false;

    private void Awake()
    {
      instance = this;
    }

    public static void PlaySound(AudioClip clip, Vector3 pos)
    {
      if (!instance)
      {
        Debug.LogError("No Audio Manager found in the scene.");
        return;
      }

      if (!clip)
      {
        Debug.LogError("Clip is null");
        return;
      }

      if (instance.logSounds)
      {
        Debug.Log("Playing Audio: " + clip.name);
      }

      GameObject soundObject = new GameObject();
      soundObject.transform.position = pos;
      soundObject.name = "Sound";
      AudioSource audioSource = soundObject.AddComponent<AudioSource>();
      audioSource.spatialBlend = 1f;
      audioSource.minDistance = instance.soundDistance;
      audioSource.clip = clip;
      audioSource.Play();
      Destroy(soundObject, clip.length);
    }
  }
}