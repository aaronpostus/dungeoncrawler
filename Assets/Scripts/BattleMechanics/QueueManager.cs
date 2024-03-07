using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueManager : MonoBehaviour
{
    public static QueueManager instance { get; private set; }

    [SerializeField] private GameObject playerQueue;

    [SerializeField] private GameObject enemyNextUp;

    [SerializeField] private GameObject attackIcon;

    [SerializeField] private GameObject strongAttackIcon;

    [SerializeField] private GameObject runIcon;

    [SerializeField] private GameObject healIcon;

    private List<GameObject> queue;
    private List<GameObject> icons;

    private int queueLength;

    private const int distBetweenIcons = 100;

    private void Awake()
    {
        instance = this;

        queue = new List<GameObject>();
        icons = new List<GameObject>();

        queueLength = 0;
    }

    public void addToQueue(string name)
    {
        Vector3 queuePosition = playerQueue.transform.position;
        if (name == "Attack")
        {
            queue.Add(Instantiate(attackIcon, new Vector3(queuePosition.x, queuePosition.y - (distBetweenIcons * (queueLength + 1)), queuePosition.z), Quaternion.Euler(0, 0, 0)));
            icons.Add(attackIcon);
        }
        else if (name == "Strong Attack")
        {
            queue.Add(Instantiate(strongAttackIcon, new Vector3(queuePosition.x, queuePosition.y - (distBetweenIcons * (queueLength + 1)), queuePosition.z), Quaternion.Euler(0, 0, 0)));
            icons.Add(strongAttackIcon);
        }
        else if (name == "Run")
        {
            queue.Add(Instantiate(runIcon, new Vector3(queuePosition.x, queuePosition.y - (distBetweenIcons * (queueLength + 1)), queuePosition.z), Quaternion.Euler(0, 0, 0)));
            icons.Add(runIcon);
        }
        else if (name == "Heal")
        {
            queue.Add(Instantiate(healIcon, new Vector3(queuePosition.x, queuePosition.y - (distBetweenIcons * (queueLength + 1)), queuePosition.z), Quaternion.Euler(0, 0, 0)));
            icons.Add(healIcon);
        }
        //Debug.Log("Icon length is: " + icons.Count + ". Icon name is " + icons[icons.Count - 1].gameObject.name);
        queue[queueLength].transform.SetParent(playerQueue.transform, true);
        queueLength++;
    }

    public void removeTopFromQueue()
    {
        //Debug.Log("Removed: " + queue[0]);
        if (queueLength > 1)
        {
            for (int i = 1; i < queueLength; i++)
            {
                queue[i].transform.position = new Vector3(queue[i].transform.position.x, queue[i].transform.position.y + distBetweenIcons, queue[i].transform.position.z);
            }
        }

        queue[0].SetActive(false);
        queue.Remove(queue[0]);
        queueLength--;
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
        enemyNextUp.SetActive(false);
    }
}
