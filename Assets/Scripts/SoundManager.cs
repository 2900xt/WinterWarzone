using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource audioSource;

    void Awake(){
        Instance = this;
    }

    public SoundList[] soundLists;

    public static void PlaySound(SoundType soundType, float volume){
        SoundList list = Instance.soundLists[(int)soundType];
        list.PlayRandomSound(volume);
    }

    [System.Serializable]
    public struct SoundList{
        [HideInInspector] public string name;
        [SerializeField] public AudioClip[] clips;
        public void PlayRandomSound(float volume){
            int randomIndex = Random.Range(0, clips.Length);
            SoundManager.Instance.audioSource.PlayOneShot(clips[randomIndex], volume);
        }
    }
}

public enum SoundType{
    JUMP,
    LAND,
    FOOTSTEP,
    THROW_SNOWBALL,
    HIT_SNOWBALL,
    RELOAD
}
