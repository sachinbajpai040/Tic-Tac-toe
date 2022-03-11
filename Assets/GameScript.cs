using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameScript : MonoBehaviour
{
    public class GameData
    {
        public int NumberOfTurns;
        public int[] matrix = new int[9];
        public string[] NameOfPlayers = new string[2];
        public bool CanResume = true;
        public int[] stack = new int[9];
        public int SizeOfStack;
    }
    public class Result
    {
        public string[] player1name = new string[15]; 
        public string[] player2name = new string[15];
        public string[] playerWon = new string[15];
        public int NoOfMatches;
    }
    
    int[,] GameMatrix = new int[3, 3];
    int _totalTurn;
    public bool isStarted;
    public bool isGameOver;
    public string[] playerName = new string[2];
    
    public GameData CurrentGameData;
    public Result ShowResult;
    
    
    public GameObject canvas;
    public GameObject[] icons;
    
    public Button playButton;
    public Button restartButton;
    public Button undoButton;
    
    public Text gameWon;
    
    public Sprite board;
    private SpriteRenderer _spriteRenderer; 
    
    
    private void Intitialise()
    {
        ShowResult = new Result();
        if (!File.Exists(Application.dataPath + "/result.json"))
        {
            string json = JsonUtility.ToJson(ShowResult);
            File.WriteAllText(Application.dataPath + "/result.json", json);
        }
        else
        {
            string savestring = File.ReadAllText(Application.dataPath + "/result.json");
            ShowResult = JsonUtility.FromJson<Result>(savestring);
        }

        CurrentGameData = new GameData();
        CurrentGameData.SizeOfStack = 0;
        CurrentGameData.CanResume = true;
        _spriteRenderer.sprite = null;
        restartButton.gameObject.SetActive(false);
        undoButton.gameObject.SetActive(false);
        playerName[0] = "Player 1";
        playerName[1] = "Player 2";
        CurrentGameData.NameOfPlayers[0] = "Player 1";
        CurrentGameData.NameOfPlayers[1] = "Player 2";
        isStarted = false;
        isGameOver = false;
        _totalTurn = -1;
        CurrentGameData.NumberOfTurns = -1;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                GameMatrix[i, j] = -1;
                CurrentGameData.matrix[i * 3 + j] = -1;
            }
        }
    }
    public void Undo()
    {
        if (CurrentGameData.SizeOfStack == 0)
            return;
        
        isGameOver = false;
        int index = CurrentGameData.stack[--CurrentGameData.SizeOfStack];
        Debug.Log(index);
        icons[index].GetComponent<TileScript>().spriteRenderer.sprite = null;
        icons[index].GetComponent<TileScript>().isPlayed = false;
        
        gameWon.text = playerName[_totalTurn % 2] + "'s turn";
        _totalTurn--;
        int x = index / 3;
        int y = index % 3;
        GameMatrix[x, y] = -1;
        CurrentGameData.matrix[x * 3 + y] = -1;
        Save();
    }
    public void Save()
    {
        CurrentGameData.NumberOfTurns = _totalTurn;
        CurrentGameData.NameOfPlayers[0] = playerName[0];
        CurrentGameData.NameOfPlayers[1] = playerName[1];
        string json = JsonUtility.ToJson(CurrentGameData);
        File.WriteAllText(Application.dataPath + "/Save.json", json);
    }

    
    public void Resume()
    {
        string savestring = File.ReadAllText(Application.dataPath + "/Save.json");
        Debug.Log(savestring);
        CurrentGameData = JsonUtility.FromJson<GameData>(savestring);
        if (!CurrentGameData.CanResume)
        {
            CurrentGameData = new GameData();
            for (int i = 0; i < 9; i++)
                CurrentGameData.matrix[i] = -1;
            DoNotLoad();
            return;
        }
        Debug.Log("_totalTurn");
        _totalTurn = CurrentGameData.NumberOfTurns;
        isStarted = true;
        playerName[0] = CurrentGameData.NameOfPlayers[0];
        playerName[1] = CurrentGameData.NameOfPlayers[1];
        gameWon.text = playerName[(_totalTurn-1) % 2] + "'s turn";
        isStarted = true;
        Showboard();
        playButton.gameObject.SetActive(false);
        playButton.GetComponent<StartScript>().player1Name.gameObject.SetActive(false);
        playButton.GetComponent<StartScript>().player2Name.gameObject.SetActive(false);
        playButton.GetComponent<StartScript>().resultButton.gameObject.SetActive(false);
        undoButton.gameObject.SetActive(true);
        
        
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                int put = CurrentGameData.matrix[i * 3 + j];
                GameMatrix[i, j] = put;
                if (put != -1)
                {
                    icons[i * 3 + j].GetComponent<TileScript>().spriteRenderer.sprite =
                        icons[i * 3 + j].GetComponent<TileScript>().tileImages[put];
                    icons[i * 3 + j].GetComponent<TileScript>().isPlayed = true;
                }

            }
        }
    }
    public void DoNotLoad()
    {
        gameWon.text = "Previous game result is concluded \nClick on play to StartScript a new game";
    }
    void Start()
    {
        Intitialise();
    }
    private void Awake()
    {
        _spriteRenderer = GetComponent < SpriteRenderer>();
    }
    public void Showboard()
    {
        _spriteRenderer.sprite = board;
    }
    public int Playerturn()
    {
        int t = ++_totalTurn;
        Debug.Log("tt " +_totalTurn);
        return t % 2;
    }
    private bool Vertical(int x, int y,int player)
    {
        int count = 0;
        for (int i = 0; i <= y; i++)
        {
            if (GameMatrix[x,i] == player)
            {
                count++;
            }
            else
            {
                return false;
            }
        }
        for (int i = y+1; i <3; i++)
        {
            if (GameMatrix[x,i] == player)
            {
                count++;
            }
            else
            {
                return false;
            }
        }

        if (count == 3)
            return true;
		return false;
    }
    private bool Horizontal(int x, int y, int player)
    {
        int count = 0;
        for (int i = 0; i <= x; i++)
        {
            if (GameMatrix[i,y] == player)
            {
                count++;
            }
            else
            {
                return false;
            }
        }
        for (int i = x+1; i <3; i++)
        {
            if (GameMatrix[i,y] == player)
            {
                count++;
            }
            else
            {
                return false;
            }
        }
        if (count == 3)
            return true;
		return false;
    }

    private bool Diagonal(int x, int y, int player)
    {
        if ((x == 0 && y == 1) || (x == 1 && y == 0) || (x == 1 && y == 2) || (x == 2 && y == 1))
        {
            return false;
        }
        int i = x, j = y;
        int count = 0;
        while (i >= 0 && j >= 0)
        {
            
            if (GameMatrix[i,j] == player)
            {
                count++;
                i--;
                j--;
            }
            else
            {
                count = 0;
                break;
            }
        }
        i = x+1;
		j = y+1;
        while (i < 3 && j < 3)
        {
            Debug.Log("in first block "+j);
            if (GameMatrix[i,j] == player)
            {
                count++;
                i++;
                j++;
            }
            else
            {
                count = 0;
                break;
            }
        }
        if (count == 3)
            return true;
        else
        {
            count = 0;
        }
        i = x;
		j = y;
        while (i >= 0 && j < 3)
        {
            if (GameMatrix[i,j] == player)
            {
                count++;
                i--;
                j++;
            }
            else
            {
                count = 0;
                break;
            }
        }
        i = x+1;
		j = y-1;
        while (i > 3 && j >=0)
        {
            if (GameMatrix[i,j] == player)
            {
                count++;
                i++;
                j--;
            }
            else
            {
                count = 0;
                break;
            }
        }
        if (count == 3)
            return true;
        else
        {
            return false;
        }
    }
    private bool IsWon(int index, int player)
    {
        int x = index / 3;
        int y = index % 3;
        return Vertical(x, y, player) || Horizontal(x, y, player) || Diagonal(x, y, player);
    }
    public void Played(int tile, int player)
    {
        int index = tile;
        int x = index / 3;
        int y = index % 3;

        GameMatrix[x, y] = player;
        CurrentGameData.matrix[x * 3 + y] = player;
        undoButton.gameObject.SetActive(true);
        gameWon.text = playerName[(player + 1) % 2] + "'s turn";

        CurrentGameData.stack[CurrentGameData.SizeOfStack] = index;
        Debug.Log("index "+index);
        CurrentGameData.SizeOfStack++;
        Save();
        if (IsWon(index, player))
        {
            isGameOver = true;
            CurrentGameData.CanResume = false;
            Save();
            gameWon.text = playerName[player] + " Won";
            gameWon.text += "\n" + "Press restart to play again";
            
            undoButton.gameObject.SetActive(false);
            restartButton.gameObject.SetActive(true);
            
            
            
            SaveResult(playerName[player]);
            
        }
        else if (_totalTurn == 8||isGameOver)
        {
            SaveResult("Game Draw");
            CurrentGameData.CanResume = false;
            undoButton.gameObject.SetActive(false);
            gameWon.text = "Game Draw"+"\nPress restart to play again";
            restartButton.gameObject.SetActive(true);
            Save();
        }
        
    }
    public void SaveResult(string resultString)
    {
        string json;
        string savestring = File.ReadAllText(Application.dataPath + "/result.json");
        ShowResult = JsonUtility.FromJson<Result>(savestring);
        
        if (ShowResult.NoOfMatches == 15)
        {
            for (int i = 1; i < 15; i++)
            {
                // ShowResult.result[i - 1] = ShowResult.result[i];
                ShowResult.player1name[i - 1] = ShowResult.player1name[i];
                ShowResult.player2name[i - 1] = ShowResult.player2name[i];
                ShowResult.playerWon[i - 1] = ShowResult.playerWon[i];
            }
            ShowResult.NoOfMatches--;
        }
        int ind = ShowResult.NoOfMatches++;
        Debug.Log(ind);
        ShowResult.player1name[ind] = playerName[0];
        ShowResult.player2name[ind] = playerName[1];
        ShowResult.playerWon[ind] = resultString;
        json = JsonUtility.ToJson(ShowResult);
        File.WriteAllText(Application.dataPath + "/result.json", json);
    }
    
}
