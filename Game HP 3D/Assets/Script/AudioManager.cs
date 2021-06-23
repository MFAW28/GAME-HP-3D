using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public Sound[] sounds;

	// Use this for initialization
	void Awake () {
		foreach (Sound s in sounds) {
			s.source = gameObject.AddComponent<AudioSource> ();
			s.source.clip = s.clip;

			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}
	}
	
	// Update is called once per frame
	public void Play (string name){
		Sound s = Array.Find (sounds, sound => sound.namesound == name);
		s.source.Play ();
	}
	public void StopPlay (string names){
		Sound s = Array.Find (sounds, sound => sound.namesound == names);
		s.source.Stop ();
	}
}
