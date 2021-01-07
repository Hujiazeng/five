using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    public Button StartButton;
    public Button ExitButton;
    public GameObject board;
    // Start is called before the first frame update
    void Start()
    {
        StartButton.onClick.AddListener(()=>{
        gameObject.SetActive(false);
        board.SetActive(true);
        });

        ExitButton.onClick.AddListener(()=>{
            Application.Quit();
        });
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
