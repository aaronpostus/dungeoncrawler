using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueManager : MonoBehaviour
{
    public static QueueManager instance { get; private set; }

    [SerializeField] private GameObject playerQueue;

    //[SerializeField] private GameObject enemyNextUp;

    [SerializeField] private GameObject attackIcon;

    [SerializeField] private GameObject strongAttackIcon;

    [SerializeField] private GameObject runIcon;

    [SerializeField] private GameObject healIcon;

    public int queueMax = 5;

    private List<GameObject> queue;
    private List<GameObject> icons;

    private int queueLength;

    private const int distBetweenIcons = 100;
    private const int distDown = 50;

    private RhythmBattleManager rhythmBattleManager;

    private void Awake()
    {
        instance = this;

        queue = new List<GameObject>();
        icons = new List<GameObject>();

        queueLength = 0;
    }

    private void Start()
    {
        rhythmBattleManager = RhythmBattleManager.instance;
    }

    public void addToQueue(string name)
    {
        if (queueLength < queueMax)
        {
            Vector3 queuePosition = playerQueue.transform.position;
            if (name == rhythmBattleManager.attack)
            {
                queue.Add(Instantiate(attackIcon, new Vector3(queuePosition.x + (distBetweenIcons * (queueLength + 1)), queuePosition.y - distDown, queuePosition.z), Quaternion.Euler(0, 0, 0)));
                icons.Add(attackIcon);
            }
            else if (name == rhythmBattleManager.attack)
            {
                queue.Add(Instantiate(strongAttackIcon, new Vector3(queuePosition.x + (distBetweenIcons * (queueLength + 1)), queuePosition.y - distDown, queuePosition.z), Quaternion.Euler(0, 0, 0)));
                icons.Add(strongAttackIcon);
            }
            else if (name == rhythmBattleManager.run)
            {
                queue.Add(Instantiate(runIcon, new Vector3(queuePosition.x + (distBetweenIcons * (queueLength + 1)), queuePosition.y - distDown, queuePosition.z), Quaternion.Euler(0, 0, 0)));
                queueMax = queue.Count;
                icons.Add(runIcon);
            }
            else if (name == rhythmBattleManager.heal)
            {
                queue.Add(Instantiate(healIcon, new Vector3(queuePosition.x + (distBetweenIcons * (queueLength + 1)), queuePosition.y - distDown, queuePosition.z), Quaternion.Euler(0, 0, 0)));
                icons.Add(healIcon);
            }
            //Debug.Log("Icon length is: " + icons.Count + ". Icon name is " + icons[icons.Count - 1].gameObject.name);
            queue[queueLength].transform.SetParent(playerQueue.transform, true);
            queueLength++;
        }
        else
        {
            Debug.Log("Tried to queue more than the max amount queue at once.");
        }
    }

    public void removeTopFromQueue()
    {
        //Debug.Log("Queue Length = " + queueLength);
        if (queueLength > 0)
        {
            //Debug.Log("Removed: " + queue[0]);
            for (int i = 1; i < queueLength; i++)
            {
                    queue[i].transform.position = new Vector3(queue[i].transform.position.x - distBetweenIcons, queue[i].transform.position.y, queue[i].transform.position.z);
            }
            
            queue[0].SetActive(false);
            queue.Remove(queue[0]);
            queueLength--;
        }
    }

    public GameObject currentIcon(int currentSequence)
    {
        if(icons.Count > 0)
        {
            //Debug.Log("Icon length is: " + currentSequence + ". Icon name is " + icons[currentSequence].gameObject.name);
            return icons[currentSequence];
        }
        else
        {
            return null;
        }
    }

    public void DeactivateQueue()
    {
        playerQueue.SetActive(false);
        //enemyNextUp.SetActive(false);
    }
}
