using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventureGame : MonoBehaviour
{
    [SerializeField] Text textComponent;
    [SerializeField] TextState startingState;
    [SerializeField] Text[] choices;

    Rect bounds1 = new Rect(340, 170, 260, 689);
    Rect bounds2 = new Rect(830, 170, 260, 689);
    Rect bounds3 = new Rect(1490, 170, 260, 689);

    TextState state;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = startingState.GetStateStory();
        state = startingState;
    }

    // Update is called once per frame
    void Update()
    {
        DisplayChoices();
        ManageState();
    }
    

    private void ManageState()
    {
        

        var nextStates = state.GetNextStates();
        if (Input.GetMouseButtonDown(0) && bounds1.Contains(Input.mousePosition))
        {
            state = nextStates[0];
        }
        else if (Input.GetMouseButtonDown(0) && bounds2.Contains(Input.mousePosition))
        {
            Debug.Log(this.gameObject.tag);
            state = nextStates[1];
        }
        else if(Input.GetMouseButtonDown(0) && bounds3.Contains(Input.mousePosition))
        {
            state = nextStates[2];
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
            choices[2].gameObject.SetActive(false);
        }
        else if (state.GetChoices().Length == 2)
        {
            choices[0].gameObject.SetActive(true);
            choices[0].text = state.GetChoices()[0];
            choices[1].gameObject.SetActive(true);
            choices[1].text = state.GetChoices()[1];
            choices[2].gameObject.SetActive(false);
        }
        else if (state.GetChoices().Length == 3)
        {
            choices[0].gameObject.SetActive(true);
            choices[0].text = state.GetChoices()[0];
            choices[1].gameObject.SetActive(true);
            choices[1].text = state.GetChoices()[1];
            choices[2].gameObject.SetActive(true);
            choices[2].text = state.GetChoices()[3];
        }

    }
}
