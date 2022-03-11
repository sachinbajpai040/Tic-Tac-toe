using UnityEngine;
using UnityEngine.UI;
public class StartScript : MonoBehaviour
{
    public Button startButton;
    public GameObject gameBoard;
    public InputField player1Name;
    public InputField player2Name;
    public Button resumeButton;
	public Button resultButton;
    void Start()
    {
        Button btn = startButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }
    void TaskOnClick()
    {
        gameBoard.GetComponent<GameScript>().isStarted = true;
        if (player1Name.text == "")
            player1Name.text = "Player 1";
        if (player2Name.text == "")
            player2Name.text = "Player 2";
        gameBoard.GetComponent<GameScript>().playerName[0] = player1Name.text;
        gameBoard.GetComponent<GameScript>().playerName[1] = player2Name.text;
        gameBoard.GetComponent<GameScript>().gameWon.text = player1Name.text + "'s TileScript";
        gameBoard.GetComponent<GameScript>().isStarted = true;
        gameBoard.GetComponent<GameScript>().Showboard();
		resultButton.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        player1Name.gameObject.SetActive(false);
        player2Name.gameObject.SetActive(false);
		resumeButton.gameObject.SetActive(false);
    }
}
