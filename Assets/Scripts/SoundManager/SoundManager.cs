using System;
using System.Collections;
using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource audioSource;

    void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public SoundList[] soundLists;

    [Rpc(SendTo.Everyone)]
    public static void PlaySoundRpc(SoundType soundType, float volume, Action onComplete = null)
    {
        SoundList list = Instance.soundLists[(int)soundType];
        list.PlayRandomSound(volume, onComplete);
    }

    [System.Serializable]
    public struct SoundList
    {
        [HideInInspector] public string name;
        [SerializeField] public AudioClip[] clips;

        public void PlayRandomSound(float volume, Action onComplete = null)
        {
            if (clips.Length == 0)
            {
                Debug.Log("NO SOUND FOR " + name);
                onComplete?.Invoke();
                return;
            }

            int randomIndex = UnityEngine.Random.Range(0, clips.Length);
            AudioClip clip = clips[randomIndex];
            Instance.audioSource.PlayOneShot(clip, volume);

            // Schedule the callback for when the sound finishes
            if (onComplete != null)
            {
                Instance.StartCoroutine(InvokeAfterDelay(clip.length, onComplete));
            }
        }

        private static IEnumerator InvokeAfterDelay(float delay, Action callback)
        {
            yield return new WaitForSeconds(delay);
            callback?.Invoke();
        }
    }

#if UNITY_EDITOR
    void OnEnable()
    {
        string[] namesList = Enum.GetNames(typeof(SoundType));
        Array.Resize(ref soundLists, namesList.Length);
        for (int i = 0; i < namesList.Length; i++)
        {
            soundLists[i].name = namesList[i];
        }
    }
#endif
}

public enum SoundType{
    BACKGROUND_MUSIC,
    JUMP,
    LAND,
    FOOTSTEP,
    THROW_SNOWBALL,
    HIT_SNOWBALL,
    RELOAD,
    MENU_MUSIC,
    DASH
}