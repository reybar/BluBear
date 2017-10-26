using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{


    private AudioSource source;

    public AudioClip clipone;

    

    Animator animator;
    
    [SerializeField]
    private Canvas quitMenu;
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private Button exitButton;

    public GameObject logo;

    

	// Use this for initialization
	void Start () 
    {

        source = GetComponent<AudioSource>();
        

        logo = GameObject.FindWithTag("logo");
        
        animator = logo.GetComponent<Animator>();
        quitMenu = quitMenu.GetComponent<Canvas>();
        startButton = startButton.GetComponent<Button>();
        exitButton = exitButton.GetComponent<Button>(); 
        quitMenu.enabled = false;
        
        

	}

    public void ExitPress()
    {
        quitMenu.enabled = true;
        startButton.enabled = false;
        exitButton.enabled = false;
    }

    public void NoPress()
    {
        quitMenu.enabled = false;
        startButton.enabled = true;
        exitButton.enabled = true;
    }

    public void StartLevel()
    {
        source.Stop();
        source.clip = clipone;
        source.Play();
        animator.SetTrigger("start");
        StartCoroutine("Wait");
        
    }

    public void ExitGame()
    {
        
        Application.Quit();
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Scene1");

        
    }
 
   

    
}
