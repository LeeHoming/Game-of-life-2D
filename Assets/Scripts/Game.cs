using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static int SCR_Width = 64;

    public static int SCR_Height = 48;

    // Start is called before the first frame update
    public float randomlive = 90;
    public int numberAlive = 0;

    public float speed = 0.1f;

    private float timer = 0;

    Cell[,] grid = new Cell[SCR_Width, SCR_Height];

    void Start()
    {
        PlaceCells();
    }

    // Update is called once per frame
    void Update()
    {
        //timer for speed control, unit: Second
        if (timer >= speed)
        {
            timer = 0f;
            CountNeighbors();
            PopulationControl();
            numAlive();
            //Debug.Log(numberAlive);
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    void PlaceCells()
    {
        for (int y = 0; y < SCR_Height; y++)
        {
            for (int x = 0; x < SCR_Width; x++)
            {
                Cell cell =
                    Instantiate(Resources.Load("Prefabs/Cell", typeof (Cell)),
                    new Vector2(x, y),
                    Quaternion.identity) as
                    Cell;
                grid[x, y] = cell;
                grid[x, y].SetAlive(RandomAliveCell());
            }
        }
    }

    bool RandomAliveCell()
    {
        int rand = UnityEngine.Random.Range(0, 100);

        if (rand > randomlive)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void numAlive(){
        int number = 0;
        for(int y = 0; y < SCR_Height; y++){
            for(int x = 0; x < SCR_Width; x++){
                if(grid[x, y].isAlive){
                    number++;
                }
            }
        }
        numberAlive = number;
    }

    void CountNeighbors()
    {
        for (int y = 0; y < SCR_Height; y++)
        {
            for (int x = 0; x < SCR_Width; x++)
            {
                int numNeighbors = 0;

                //north
                if (y + 1 < SCR_Height)
                {
                    if (grid[x, y + 1].isAlive)
                    {
                        numNeighbors++;
                    }
                }

                //south
                if (y - 1 > 0)
                {
                    if (grid[x, y - 1].isAlive)
                    {
                        numNeighbors++;
                    }
                } //east
                if (x + 1 < SCR_Width)
                {
                    if (grid[x + 1, y].isAlive)
                    {
                        numNeighbors++;
                    }
                } //west
                if (x - 1 > 0)
                {
                    if (grid[x - 1, y].isAlive)
                    {
                        numNeighbors++;
                    }
                } //north east
                if (y + 1 < SCR_Height && x + 1 < SCR_Width)
                {
                    if (grid[x + 1, y + 1].isAlive)
                    {
                        numNeighbors++;
                    }
                } //north west
                if (y + 1 < SCR_Height && x - 1 > 0)
                {
                    if (grid[x - 1, y + 1].isAlive)
                    {
                        numNeighbors++;
                    }
                } //south east
                if (y - 1 > 0 && x + 1 < SCR_Width)
                {
                    if (grid[x + 1, y - 1].isAlive)
                    {
                        numNeighbors++;
                    }
                } //south west
                if (y - 1 > 0 && x - 1 > 0)
                {
                    if (grid[x - 1, y - 1].isAlive)
                    {
                        numNeighbors++;
                    }
                }

                grid[x, y].numNeighbors = numNeighbors;
            }
        }
    }

    void PopulationControl()
    {
        for (int y = 0; y < SCR_Height; y++)
        {
            for (int x = 0; x < SCR_Width; x++)
            {
                int num = grid[x, y].numNeighbors;

                //rule:
                //Any live cell with 2 or 3 live neiggbors survives
                //Any dead cell with 3 live neighbors becomes a live cell
                //Any other live cells die in the next generation and all other dead cells stay dead
                if (grid[x, y].isAlive)
                {
                    //cell is alive
                    if (num != 2 && num != 3)
                    {
                        grid[x, y].SetAlive(false);
                    }
                }
                else
                {
                    //cell is dead
                    if (num == 3)
                    {
                        grid[x, y].SetAlive(true);
                    }
                }
            }
        }
    }
}
