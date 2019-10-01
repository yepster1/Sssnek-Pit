using UnityEngine;
using System.Collections;
using System.Diagnostics;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {

	public static SoundManager INSTANCE;

    public float Volume;

	public AudioClip[] AudioClips;
	public AudioClip[] AudioClipsForGettingUp;

	public AudioClip collectPoint;
	public AudioClip die;
	public AudioClip spawn;
    public AudioClip jump;
    public AudioClip shoot;

    public AudioClip win;
	AudioSource gameOverAudioSource;

    float lowPitch = 0.8f;
	float highPitch = 1.2f;
	float lowVolume = 0.6f;
	float highVolume = 1f;

	int hitSoundCallCount;

	void Awake(){
		
		DontDestroyOnLoad (gameObject);
		if (INSTANCE == null) {
			INSTANCE = this;
			gameOverAudioSource = GetComponent<AudioSource>();
        } else {
            Destroy (gameObject);
        }
	}

    private void Start()
    {
		gameOverAudioSource = GetComponent<AudioSource>();
	}
    private void Update()
	{
		//to ensure that when alot of things are hit that it dosnt sound terrible.... Improves sfx feal
		if (hitSoundCallCount >0) {
			hitSoundCallCount--;
		}
	}
	public void playCollectPoit(AudioSource s){
		hitSoundCallCount++;
        if (hitSoundCallCount < 2)
        {
            s.PlayOneShot(collectPoint);
        }
	}
	
	public void PlayDie(AudioSource s){
		s.PlayOneShot(die);
	}
	public void PlaySpawn(AudioSource s){
		s.PlayOneShot(spawn);
	}

	public void PlayJump(){
        gameOverAudioSource.PlayOneShot(jump, 0.5f);
	}

	public void PlayShoot()
	{
        gameOverAudioSource.PlayOneShot(shoot, 0.5f);
	}

	void SetVolume(AudioSource source, float value){
		source.volume = value;
	}

    public IEnumerator FadeOut(AudioSource audioSource, float FadeTime) {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0) {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
}
