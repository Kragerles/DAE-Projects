using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject itemTab;
    public bool isOpen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenu.SetActive(false);
        itemTab.SetActive(true);
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isOpen) closeMenu();
            else openMenu();
        }
    }

    public void openMenu(){
        pauseMenu.SetActive(true);
        itemTab.SetActive(false);
        isOpen = true;
        Time.timeScale = 0f;
    }

    public void closeMenu(){
        pauseMenu.SetActive(false);
        itemTab.SetActive(true);
        isOpen = false;
        Time.timeScale = 1f;
    }
}
