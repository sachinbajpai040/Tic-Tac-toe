using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class switchscene : MonoBehaviour
{
    // Start is called before the first frame update
    public Button yourButton;
    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }
    void TaskOnClick()
    {
        yourButton.gameObject.SetActive(false);
        if (SceneManager.GetActiveScene().name == "SampleScene")
            SceneManager.LoadScene("Result");
        else
            SceneManager.LoadScene("SampleScene");

        //gotoAndstop("SampleScene");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}