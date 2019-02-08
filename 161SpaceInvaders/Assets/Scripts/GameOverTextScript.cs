using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverTextScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.EndGame.AddListener(ShowGameOverText);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ShowGameOverText()
    {
        this.transform.GetChild(0).gameObject.SetActive(true);
    }
}
