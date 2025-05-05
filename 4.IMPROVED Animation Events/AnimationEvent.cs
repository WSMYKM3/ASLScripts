using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]  // Allows this class to be serialized in Unity Inspector
public class AnimationEvent{
    
    public string eventName; // [Referenced in AnimationEventReceiver.cs] Used to identify specific animation events
    // also A string identifier used to match and trigger specific animation events in state machine, the one in AnimationEventStateBehaviour.cs    
    public UnityEvent OnAnimationEvent;   // [Referenced in AnimationEventReceiver.cs] Unity Event that will be triggered when animation event occurs
}
