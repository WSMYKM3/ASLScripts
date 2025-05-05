using UnityEngine;
using UnityEngine.Events;

// AnimationEventStateBehaviour is attached to animation states in the Animator and monitors animation progress
// When the specified triggerTime is reached, it looks for an AnimationEventReceiver component
// The AnimationEventReceiver maintains a list of AnimationEvent objects
// When triggered, it finds the matching event by name and invokes its associated Unity Event
// The eventName string serves as the key that connects all three components together
public class AnimationEventStateBehaviour : StateMachineBehaviour{
    public string eventName;//eventName is used across all three scripts to match events
    //[Referenced in AnimationEvent.cs] Matches with eventName in AnimationEvent class

    [Range(0f,1f)] public float triggerTime;//the exact time of trigger the OnAnimationEventTriggered method in AnimationEventReceiver.cs

    bool hasTriggered;// Internal flag to ensure event only triggers once per state
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
        hasTriggered = false;// Reset trigger flag when entering animation state
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
        float currentTime = stateInfo.normalizedTime % 1f;// Get current animation progress (0-1)

        // // Check if it's time to trigger and hasn't triggered yet
        if(!hasTriggered && currentTime >= triggerTime){
            NotifyReceiver(animator);
            hasTriggered = true;
        }
    }

    void NotifyReceiver(Animator animator){
        //// [References AnimationEventReceiver.cs] Gets the receiver component
        AnimationEventReceiver receiver = animator.GetComponent<AnimationEventReceiver>();

        if(receiver != null){
            receiver.OnAnimationEventTriggered(eventName);// [Calls method in AnimationEventReceiver.cs] Triggers the event
        }
    }
}
