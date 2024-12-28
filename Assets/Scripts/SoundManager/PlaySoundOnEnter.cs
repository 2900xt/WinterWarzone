using System.Collections;
using UnityEngine;

public class PlaySoundOnEnter : StateMachineBehaviour
{
    public SoundType soundType;
    [Range(0f, 1f)] public float volume = 1f;

    //private bool soundFinished;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //soundFinished = false;

        // Start the coroutine to wait for the sound to finish
        SoundManager.PlaySound(soundType, volume, () =>
        {
            //soundFinished = true;
            animator.SetBool("SoundFinished", true);
        });

        // Reset the flag initially to block transitions
        animator.SetBool("SoundFinished", false);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Clean up the flag to ensure proper state on re-entry
        animator.SetBool("SoundFinished", false);
    }
}
