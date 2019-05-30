using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextState")]
public class TextState : ScriptableObject
{
    [TextArea]
    [SerializeField] public string Script;
    [SerializeField] public string[] choices;
    [SerializeField] public TextState[] nextStates;
    [SerializeField] public AudioClip musicClip;
    public bool isPlayed;

    public string GetStateStory() {
        return Script;
    }

    public TextState[] GetNextStates() {
        return nextStates;
    }

    public string[] GetChoices() {
        return choices;
    }
}
