using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    private AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.Max];
    private Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    private GameObject _soundRoot = null;

    public void Init()
    {
        if (_soundRoot == null)
        {
            _soundRoot = GameObject.Find("@SoundRoot");
            if (_soundRoot == null)
            {
                _soundRoot = new GameObject { name = "@SoundRoot" };
                Object.DontDestroyOnLoad(_soundRoot);

                string[] soundTypeNames = System.Enum.GetNames(typeof(Define.Sound));
                // Define.Sound의 마지막은 Max값이기 때문에 Length - 1
                for (int count = 0; count < soundTypeNames.Length - 1; count++)
                {
                    GameObject go = new GameObject { name = soundTypeNames[count] };
                    _audioSources[count] = go.AddComponent<AudioSource>();
                    go.transform.parent = _soundRoot.transform;
                }

                _audioSources[(int)Define.Sound.Bgm].loop = true;
            }
        }
    }

    public bool Play(Define.Sound type, string path, float volume = 1.0f, float pitch = 1.0f)
    {
        if (string.IsNullOrEmpty(path))
            return false;

        AudioSource audioSource = _audioSources[(int)type];
        if (path.Contains("Sound/") == false)
            path = string.Format("Sound/{0}", path);

        audioSource.volume = volume;
        
        // BGM은 어짜피 하나만 재생되니 바로 Resource.Load
        if (type == Define.Sound.Bgm)
        {
            AudioClip audioClip = Managers.Resource.Load<AudioClip>(path);
            if (audioClip == null)
                return false;
            
            if(audioSource.isPlaying)
                audioSource.Stop();

            audioSource.clip = audioClip;
            audioSource.pitch = pitch;
            audioSource.Play();
            return true;
        }
        
        // 나머지 사운드들은 여러 번 사용할 수도 있으니 Dictionary로 관리
        else if (type == Define.Sound.Effect)
        {
            AudioClip audioClip = GetAudioClip(path);
            if (audioSource == null)
                return false;

            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
            return true;
        }
        else if (type == Define.Sound.Speech)
        {
            AudioClip audioClip = GetAudioClip(path);
            if (audioSource == null)
                return false;

            if (audioSource.isPlaying)
                audioSource.Stop();
            
            if(audioSource.isPlaying)
                audioSource.Stop();

            audioSource.clip = audioClip;
            audioSource.pitch = pitch;
            audioSource.Play();
            return true;
        }

        return false;
    }

    private AudioClip GetAudioClip(string path)
    {
        AudioClip audioClip = null;
        // 이미 등록되어 있는 경우
        if (_audioClips.TryGetValue(path, out audioClip))
            return audioClip;

        // 미등록시 Dictionary에 등록
        audioClip = Managers.Resource.Load<AudioClip>(path);
        _audioClips.Add(path, audioClip);
        return audioClip;

    }
}
