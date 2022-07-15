using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public List<ModeData> datas;
    public ModeData.Mode currentMode;

    public bool isGameStarted;

    public GameObject[] characters;

    public int currentCharacterIndex = 0;
    public int currency = 0;

    public AudioClip[] clips;
    private AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.volume = PlayerPrefs.GetFloat("MusicVolume");
        characters = Resources.LoadAll<GameObject>("Characters").OrderBy(x => x.GetComponent<CharacterManager>().characterIndex).ToArray<GameObject>();
        DontDestroyOnLoad(gameObject);
        if (PlayerPrefs.HasKey("current_selected_character"))
        {
            currentCharacterIndex = PlayerPrefs.GetInt("current_selected_character");
            currency = PlayerPrefs.GetInt("Currency");
        }
        else
        {
            PlayerPrefs.SetString("own_characters", currentCharacterIndex.ToString());
            Save();
        }
    }

    private AudioClip getRandomClip()
    {
        return clips[Random.Range(0, clips.Length)];
    }

    private void Update()
    {
        if(!audioSource.isPlaying)
        {
            audioSource.clip = getRandomClip();
            audioSource.Play();
        }
    }

    /* private void FixedUpdate()
     {
         if (!isGameStarted)
         {
             isGameStarted = true;
             //Camera.main.GetComponent<Animator>().SetTrigger("isGameStart");
             Camera.main.GetComponent<CameraOverrider>().isMoving = true;
         }
     }
         */

    public void Save()
    {
        PlayerPrefs.SetString("current_selected_character", currentCharacterIndex.ToString());
        PlayerPrefs.SetInt("Currency", currency);
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume / 100;
        PlayerPrefs.SetFloat("MusicVolume", volume / 100);
    }
}
