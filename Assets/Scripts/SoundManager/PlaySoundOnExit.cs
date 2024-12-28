using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnExit : StateMachineBehaviour
{
    public SoundType soundType;
    [Range(0f, 1f)] public float volume = 1f;
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SoundManager.PlaySound(soundType, volume);
    }
}
