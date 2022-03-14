using UnityEngine;
using UnityEngine.UI;

public class UndoScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameboard;
    public Button undoButton;
    void Start()
    {
        Button btn = undoButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }
    void TaskOnClick()
    {
        Debug.Log("You have clicked the Undo button!");
        gameboard.GetComponent<GameScript>().Undo();
    }
}