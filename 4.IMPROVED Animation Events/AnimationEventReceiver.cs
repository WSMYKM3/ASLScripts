using System.Collections.Generic;
using UnityEngine;

public class AnimationEventReceiver : MonoBehaviour
{
    [SerializeField] List<AnimationEvent> animationEvents = new();    // [References AnimationEvent.cs] List of animation events configured in Inspector

    public void OnAnimationEventTriggered(string eventName){    // Method called from animation events in Unity's Animation system
        AnimationEvent matchingEvent = animationEvents.Find(se => se.eventName == eventName);    // Finds the matching event by name
        matchingEvent?.OnAnimationEvent?.Invoke();    // Safely invokes the UnityEvent if found
    }
} 