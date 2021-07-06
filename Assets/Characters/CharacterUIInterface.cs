using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterUIInterface : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI FeedbackText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetFeedbackText(string newText)
    {
        FeedbackText.text = newText;
    }
}
