using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    public Button restartButton;
    public Button runButton;
    public MainLoop mainLoop;
    void Start()
    {
        restartButton.onClick.AddListener(()=>{
            mainLoop.Restart();
            gameObject.SetActive(false);
            
        });

        runButton.onClick.AddListener(()=>{
            Application.Quit();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
