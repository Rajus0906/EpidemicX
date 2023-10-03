using UnityEngine;

public class MuteGame : MonoBehaviour
{
    // A flag to keep track of whether the game is muted or not
    private bool isMuted = false;

    // A reference to the audio source that you want to mute/unmute
    public AudioSource audioSource;

    void Update()
    {
        // Check if the "M" key is pressed
        if (Input.GetKeyDown(KeyCode.M))
        {
            // If the game is muted, unmute it
            if (isMuted)
            {
                audioSource.mute = false;
                isMuted = false;
            }
            // Otherwise, mute it
            else
            {
                audioSource.mute = true;
                isMuted = true;
            }
        }
    }
}
