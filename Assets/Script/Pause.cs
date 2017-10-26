using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField]
    private Transform canvas;

    public AudioSource sound1;


    void start()
    {
        sound1 = GetComponent<AudioSource>();
    }
    void Update ()
    {
        pause();
    }
    public void pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (canvas.gameObject.activeInHierarchy == false)
            {
                canvas.gameObject.SetActive(true);
                Time.timeScale = 0;
                sound1.Pause();


            }
            else
            {
                canvas.gameObject.SetActive(false);
                Time.timeScale = 1;
                sound1.UnPause();

            }
        }
    }

    

    public void ContinuePress()
    {
        canvas.gameObject.SetActive(false);
        Time.timeScale = 1;
        sound1.UnPause();
    }

    public void Exit()
    {       
        SceneManager.LoadScene("StartMenu");
    }
      
}
 
   

    

