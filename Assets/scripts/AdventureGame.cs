using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventureGame : MonoBehaviour
{
    [SerializeField] Text textComponent;
    [SerializeField] TextState startingState;
    [SerializeField] Text[] choices;
    TextState state;
    Stack<TextState> previousStates = new Stack<TextState>();
    int rewindCounter = 5;
    Rect bounds1;
    Rect bounds2;
    Rect bounds3;
    int width = 400;
    int height = 70;

    // Start is called before the first frame update
    void Start()
    {
        bounds1 = InitializeBounds(GameObject.Find("Choice1"));
        bounds2 = InitializeBounds(GameObject.Find("Choice2"));
        bounds3 = InitializeBounds(GameObject.Find("Choice3"));

        textComponent.text = startingState.GetStateStory();
        state = startingState;
        GetComponent<AudioSource>().clip = state.musicClip;
        GetComponent<AudioSource>().Play();
        choices[2].text = ("Retries: 5");
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<AudioSource>().isPlaying && state.isPlayed == false)
        {
            GetComponent<AudioSource>().clip = state.musicClip;
            GetComponent<AudioSource>().Play();
            state.isPlayed = true;
        }
        DisplayChoices();
        ManageState();
        choices[2].text = ("Retries:" + rewindCounter);
    }
    

    private void ManageState()
    {
        try
        {
            if (state == startingState)
                rewindCounter = 5;
            var nextStates = state.GetNextStates();
            if (Input.GetMouseButtonDown(0) && bounds1.Contains(Input.mousePosition))
            {
                GetComponent<AudioSource>().Stop();
                state.isPlayed = false;
                previousStates.Push(state);
                state = nextStates[0];
            }
            else if (Input.GetMouseButtonDown(0) && bounds2.Contains(Input.mousePosition) && state.GetChoices().Length == 2)
            {
                GetComponent<AudioSource>().Stop();
                state.isPlayed = false;
                previousStates.Push(state);
                state = nextStates[1];
            }
            else if (Input.GetMouseButtonDown(0) && bounds3.Contains(Input.mousePosition) && rewindCounter > 0 && previousStates.Count > 0)
            {
                GetComponent<AudioSource>().Stop();
                state.isPlayed = false;
                state = previousStates.Pop();
                rewindCounter--;
            }
        }
        catch(System.IndexOutOfRangeException e)
        {
            Debug.Log(e.Message);
        }
        
        textComponent.text = state.GetStateStory();
    }

    private void DisplayChoices()
    {
        if (state.GetChoices().Length == 1)
        {
            choices[0].gameObject.SetActive(true);
            choices[0].text = state.GetChoices()[0];
            choices[1].gameObject.SetActive(false);
        }
        else if (state.GetChoices().Length == 2)
        {
            choices[0].gameObject.SetActive(true);
            choices[0].text = state.GetChoices()[0];
            choices[1].gameObject.SetActive(true);
            choices[1].text = state.GetChoices()[1];
        }

    }

    private Rect InitializeBounds(GameObject choice)
    {
        Rect bounds = new Rect(choice.transform.position.x - width/2, choice.transform.position.y - height/2, width, height);
        return bounds;
    }
}
