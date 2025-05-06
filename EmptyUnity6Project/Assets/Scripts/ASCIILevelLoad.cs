using System;
using System.IO;
using Unity.Cinemachine;
using UnityEngine;

public class ASCIILevelLoader : MonoBehaviour
{
    //Prefabs
    public GameObject prefabPlayer;
    public GameObject prefabBorder;
    public GameObject prefabObstacle;
    public GameObject prefabObstacle1;
    public GameObject prefabObstacle2;
    public GameObject prefabGoal;
    public CinemachineCamera camera;
    
    //File variables
    string filePath;
    public string fileName;

    public int currentLevel;

    //Property to auto load a level whenever CurrentLevel changes
    public int CurrentLevel
    {
        set
        {
            currentLevel = value;
            LoadLevel();
        }
        get
        {
            return currentLevel;
        }
    }

    //Offsets
    public float offsetX = -3;
    public float offsetY = -3;

    //GameObject for parenting all level objects to
    GameObject levelHolder;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        CurrentLevel = currentLevel; //set the level to 0 at load level 0 at the start
    }

    public void LoadLevel()
    {
        //if we have a current level, destroy it
        if (levelHolder != null)
        {
            Destroy(levelHolder);
        }

        //create a new level hodlder
        levelHolder = new GameObject("levelHolder");

        filePath = Application.dataPath;

        //string fileContents = File.ReadAllText(filePath + fileName);
        //Debug.Log(fileContents);

        //load each line of the file into separate lines in an array
        string[] lines = 
            File.ReadAllLines(
                filePath + fileName.Replace("<num>", currentLevel.ToString()));

        //looping through each line of the file
        for (int y = 0; y < lines.Length; y++)
        {
            Debug.Log(lines[y]);

            string line      = lines[y]; //getting the string for this line
            char[] charArray = line.ToCharArray();//turn that string into a char array
            
            //loop through each character on this line
            for (int x = 0; x < charArray.Length; x++)
            {
                char c = charArray[x];

                GameObject newObj = null; //create a holder for a new prefab instance

                switch (c)
                {
                    case 'P': //we got a P in the file
                        newObj = Instantiate<GameObject>(prefabPlayer);
                        camera.Follow = newObj.transform;
                        break;
                    case 'B': //we got a W in the file
                        newObj = Instantiate<GameObject>(prefabBorder);
                        break;
                    case '*':
                        newObj = Instantiate<GameObject>(prefabObstacle);
                        break;
                    case '&':
                        newObj = Instantiate<GameObject>(prefabObstacle1);
                        break;
                    case '%':
                        newObj = Instantiate<GameObject>(prefabObstacle2);
                        break;
                    case 'G':
                        newObj = Instantiate<GameObject>(prefabGoal);
                        break;
                    default:
                        break;
                }

                //if we created a gameObject for this character
                if (newObj != null)
                {
                    //put it underneath the levelholder object
                    newObj.transform.parent = levelHolder.transform;
                    //and set it's position
                    newObj.transform.position =
                        new Vector3(x, -y, 0);
                }
            }
        }
            
        levelHolder.transform.position = 
            new Vector3(offsetX, offsetY, 0);

    }
}
