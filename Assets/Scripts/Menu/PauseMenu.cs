using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Transform Canvas;
    public Transform Player;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Pause();
        }
    }
    public void Pause()
    {
            if (Canvas.gameObject.activeInHierarchy == false)
            {
                Canvas.gameObject.SetActive(true);
                Time.timeScale = 0;

                Player.GetComponent<PlayerMovement>().enabled = false;
            }
            else
            {
                Canvas.gameObject.SetActive(false);
                Time.timeScale = 1;
                Player.GetComponent<PlayerMovement>().enabled = true;
            }
    }

    public void EndGame()
    {     
       Scene currentScence = SceneManager.GetActiveScene();
       SceneManager.LoadScene(currentScence.name);
    }
}
