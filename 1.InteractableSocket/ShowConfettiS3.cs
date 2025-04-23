using UnityEngine;
using UnityEngine.Playables;

public class ShowConfettiS3 : MonoBehaviour
{
    public ParticleSystem confetti; // Reference to the confetti particle system
    public Animator tutorAnimator;
    public GameObject loopText;
    private int correctSnapsCount = 0; // Counter to keep track of the number of correct snaps

    public void OnCorrectObjectSnapped()
    {
        correctSnapsCount++;
        CheckCompletion();
    }

    public void OnCorrectObjectRemoved()
    {
        correctSnapsCount = Mathf.Max(0, correctSnapsCount - 1); // Ensure we don't go below 0
    }

    private void CheckCompletion()
    {
        if (correctSnapsCount == 3)
        {
            confetti.Play();
            tutorAnimator.SetBool("Done", true);
            tutorAnimator.SetBool("Play", false);
            loopText.GetComponent<PlayableDirector>().Stop();
        }
        else
        {
            Debug.Log($"Correct snaps: {correctSnapsCount}/3");
        }
    }
}
