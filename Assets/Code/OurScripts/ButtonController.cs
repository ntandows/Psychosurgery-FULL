using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour, IPointerClickHandler
{
    private Button endButton;
    private Text buttonText;

    private SceneDriver scenes;

    // Start is called before the first frame update
    void Start()
    {
        endButton = gameObject.GetComponent<Button>();
        buttonText = endButton.GetComponentInChildren<Text>();
        buttonText.text = "Next Level";

        scenes = gameObject.GetComponent<SceneDriver>();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Right ||
            pointerEventData.button == PointerEventData.InputButton.Left)
        {
            scenes.GoToNextScene();
        }
    }
}
