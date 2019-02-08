using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LivesTextScript : MonoBehaviour
{
    TextMeshProUGUI livesText;

    private void Awake()
    {
        livesText = this.GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.updateLives.AddListener(UpdateLivesText);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateLivesText(int lives)
    {
        livesText.SetText("Lives: {0}", lives);
    }
}
