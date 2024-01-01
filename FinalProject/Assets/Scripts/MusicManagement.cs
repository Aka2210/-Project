using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MusicManagement : MonoBehaviour
{
    //private static MusicManager instance;
    private AudioSource audioSource;
    private static MusicManagement _instance;

    // �s�W�R�A�ܼ�
    public static float musicVolume { get; private set; } = 1.0f;
    public static float effectsVolume { get; private set; } = 1.0f;
    public AudioClip[] newAudioClip;
    private string currentSceneName;  // �O�s�ثe�������W��
    private bool hasPlayedCurrentAudio;  // �O�_�w�g����F��e����������
    private AudioClip lastPlayedAudioClip;  // �W�@�����񪺭���
    private void Awake()
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
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
        //Debug.Log("Set MusicManagement: musicVolume=" + musicVolume);
    }

    public void SetEffectsVolume()
    {
        SceneSwitcher sceneSwitcher = FindObjectOfType<SceneSwitcher>();
        effectsVolume = sceneSwitcher.EffectsValueChanged();
        //Debug.Log("Set MusicManagement: effectsVolume=" + effectsVolume);
    }
    public float GetMusicVolume() {
        //Debug.Log("Get musicVolume: " + musicVolume);
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
        //Debug.Log("OnDestroy called");
        SaveVolumes();
    }
    private void OnSceneUnloaded(Scene scene)
    {
        //Debug.Log("OnSceneUnload");
        //Debug.Log(musicVolume);
        //Debug.Log(effectsVolume);
        SaveVolumes();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
        //musicVolume = transform.GetComponent<AudioSource>().volume;
        //Debug.Log("OnSceneUnload");
        //Debug.Log(musicVolume);
        //Debug.Log(effectsVolume);
        GameObject.FindObjectOfType<SceneSwitcher>().firstenter = true;
        // ����ثe�������W��
        currentSceneName = scene.name;

        // �P�_�O�_�w�g����F��e���������֡A�p�G�S���A�N��������
        if (!hasPlayedCurrentAudio)
        {
            SwitchAudioForScene(currentSceneName);
        }

        // ���m����лx
        hasPlayedCurrentAudio = false;

    }

    private void SwitchAudioForScene(string sceneName)
    {
        Debug.Log("Switching audio for scene: " + sceneName);
        // �ھڳ����W�٤�������
        switch (sceneName)
        {
            case "level1":
            case "level2":
            case "level3":
                audioSource.Stop();  // ����ثe������
                audioSource.clip = newAudioClip[1];  // ����ĤG������
                audioSource.volume = musicVolume;
                audioSource.Play();  // ����s������
                hasPlayedCurrentAudio = true;
                break;
            default:
                // �p�G�W�@�����񪺭��֩M�ثe�����֬ۦP�A�N�٭쭵�q�A�_�h�����s����
                if (lastPlayedAudioClip == newAudioClip[0])
                {
                    audioSource.volume = musicVolume;
                    //audioSource.Play();
                }
                else
                {
                    audioSource.Stop();
                    audioSource.clip = newAudioClip[0];  // ����Ĥ@������
                    audioSource.volume = musicVolume;
                    audioSource.Play();
                }
                hasPlayedCurrentAudio = true;
                break;
        }
    }

    // �o�Ӥ�k�Ω�O�s���q��
    private void SaveVolumes()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        //PlayerPrefs.SetFloat("EffectsVolume", effectsVolume);
        //Debug.Log("Save " + musicVolume);
        //Debug.Log("Save " + effectsVolume);
        PlayerPrefs.Save();  // �O�s PlayerPrefs
        lastPlayedAudioClip = audioSource.clip;
    }
}
