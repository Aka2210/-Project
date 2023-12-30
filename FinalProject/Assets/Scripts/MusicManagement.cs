using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManagement : MonoBehaviour
{
    //private static MusicManager instance;
    private AudioSource audioSource;
    private static MusicManagement _instance;
    // �s�W�R�A�ܼ�
    public static float musicVolume { get; private set; } = 1.0f;
    public static float effectsVolume { get; private set; } = 1.0f;

    private void Awake()
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        // �ˬd�O�_�w�g�s�b���
        MusicManagement[] musicManagements = FindObjectsOfType<MusicManagement>();
        if (musicManagements.Length > 1)
        {
            // �w�g�s�b��L��ҡA�P���o�ӷs���
            Destroy(gameObject);
            return;
        }

        // �T�O�b���������ɤ��Q�P��
        DontDestroyOnLoad(gameObject);

        // ����b���������ɭ��s���񭵼�
        audioSource = GetComponent<AudioSource>();
        if (!audioSource.isPlaying)
        {
            audioSource.volume = musicVolume;
            audioSource.Play();
        }

        //SetEffectsVolume();
        //SetMusicVolume();
    }

    private void Start()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        audioSource = GetComponent<AudioSource>();

        // ����b���������ɭ��s���񭵼�
        if (!audioSource.isPlaying)
        {
            audioSource.volume = musicVolume;
            audioSource.Play();
        }
    }
    private void Update() { 
        SetEffectsVolume();
        SetMusicVolume();
    }
    public void SetMusicVolume()
    {
        SceneSwitcher sceneSwitcher = FindObjectOfType<SceneSwitcher>();
        musicVolume = sceneSwitcher.MusicValueChanged();
        Debug.Log("MusicManagement: musicVolume=" + musicVolume);
    }

    public void SetEffectsVolume()
    {
        SceneSwitcher sceneSwitcher = FindObjectOfType<SceneSwitcher>();
        effectsVolume = sceneSwitcher.EffectsValueChanged();
        Debug.Log("MusicManagement: effectsVolume=" + effectsVolume);
    }
    public float GetMusicVolume() {
        return musicVolume;
    }
    public float GetEffectsVolume()
    {
        return effectsVolume;
    }

    // �o�Ӥ�k�b�C���h�X�ɳQ�I�s�A�Ω�O�s���q��
    private void OnApplicationQuit()
    {
        SaveVolumes();
    }
    //�b����Q�P���]�Ҧp���������^�ɳQ�եΡA�Ω�O�s���q��
    private void OnDestroy()
    {
        Debug.Log("OnDestroy called");
        SaveVolumes();
    }
    private void OnSceneUnloaded(Scene scene)
    {
        Debug.Log("OnSceneUnload");
        Debug.Log(musicVolume);
        Debug.Log(effectsVolume);
        SaveVolumes();
    }

    // �o�Ӥ�k�Ω�O�s���q��
    private void SaveVolumes()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("EffectsVolume", effectsVolume);
        Debug.Log("Save " + musicVolume);
        PlayerPrefs.Save();  // �O�s PlayerPrefs
    }
}
