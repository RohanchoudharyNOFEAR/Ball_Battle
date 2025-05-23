using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//AudioManager is global class that contains audio data and logic and can be triggered by events by other classes
//this class usses observer design pattern which is lightlt (not tight) coupled
//but i prefer global singleton for thsese types of classes , -> i used observer just to show case
//you can see my vfxmanager implementation that uses singletonn

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public struct AudioConfiguration
    {
        public AudioClip clip;
        [Range(0, 1.0f)]
        public float volume;
        public bool is2D;
        public bool loop;
        public float delay;

        public string Name
        {
            get { return clip == null ? "No clip selected" : clip.name; }
        }

        public bool IsValid()
        {
            return clip != null;
        }

        public void AssignToAudioSource(AudioSource audioSource)
        {
            audioSource.volume = volume;
            audioSource.PlayDelayed(delay);
            audioSource.clip = clip;
            audioSource.spatialBlend = is2D ? 0.0f : 1.0f;
            audioSource.loop = loop;
        }
    }

    //SINGLETON
    public static AudioManager Instance { get; private set; }



    [Header("References")]
    public AudioSource AudioSourcePrefab;

    [Header("Configurations")]
    public int MaxAudioSources = 16;
    public Transform AudioSourceDefaultParent;

    [Header("Audios")]
    public AudioConfiguration SoldierSpawnAudio;
    public AudioConfiguration AttackerCaughtAudio;
    public AudioConfiguration PlayerLoseAudio;
    public AudioConfiguration PlayerWinAudio;

    private readonly Stack<AudioSource> _freeAudioSources = new Stack<AudioSource>();
    private List<AudioSource> _audioSourcesInUse = new List<AudioSource>();

    //menu audio manager
    private AudioSource _menuAudioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {


        _menuAudioSource = GetComponent<AudioSource>();

        for (int i = 0; i < MaxAudioSources; i++)
        {
            var audioSource = Instantiate(AudioSourcePrefab, AudioSourceDefaultParent);
            audioSource.transform.localPosition = Vector3.zero;

            _freeAudioSources.Push(audioSource);

        }

        RegisterCallbacks();
    }

    private void OnDisable()
    {
        UnRegisterCallbacks();
    }

    private void Update()
    {
        for (var i = _audioSourcesInUse.Count - 1; i >= 0; i--)
        {
            var source = _audioSourcesInUse[i];
            if (!source.isPlaying)
            {
                _freeAudioSources.Push(source);
                _audioSourcesInUse.RemoveAt(i);
                source.transform.SetParent(AudioSourceDefaultParent);
                source.transform.position = Vector3.zero;
            }
        }

    }

    private void RegisterCallbacks()
    {
        if (GameManager.Instance != null)
        {
            SoldierSpawner.SoldierSpawnEvent += OnSholdierSpawn;
            DefenderChaseState.CaughtAttackerEvent += OnAttackerCaught;

            GameManager.Instance.PlayerLoseEvent += OnPlayerLose;
            GameManager.Instance.PlayerWinEvent += OnPlayerWin;
        }
    }

    private void UnRegisterCallbacks()
    {
        if (GameManager.Instance != null)
        {
            SoldierSpawner.SoldierSpawnEvent -= OnSholdierSpawn;
            DefenderChaseState.CaughtAttackerEvent -= OnAttackerCaught;

            GameManager.Instance.PlayerLoseEvent -= OnPlayerLose;
            GameManager.Instance.PlayerWinEvent -= OnPlayerWin;
        }
    }

    AudioSource GetAvailableAudioSource()
    {
       // Debug.Log(_freeAudioSources.Count + "freeaudiosource");
        if (_freeAudioSources.Count > 0)
        {

            var source = _freeAudioSources.Pop();
            _audioSourcesInUse.Add(source);
            return source;
        }
        else
        {
            var source = _audioSourcesInUse[0];
            _audioSourcesInUse.RemoveAt(0);
            _audioSourcesInUse.Add(source);
            return source;
        }
    }

    void PlayAudioClip(AudioConfiguration audioConfig)
    {
        var source = GetAvailableAudioSource();
        audioConfig.AssignToAudioSource(source);

        source.transform.position = Vector3.zero;
        source.Play();
    }

    private void OnSholdierSpawn(GameObject s)
    {
        PlayAudioClip(SoldierSpawnAudio);
    }

    private void OnAttackerCaught()
    {
        PlayAudioClip(AttackerCaughtAudio);
    }

    private void OnPlayerLose()
    {
        PlayAudioClip(PlayerLoseAudio);
    }
    private void OnPlayerWin()
    {
        PlayAudioClip(PlayerWinAudio);
    }
}
