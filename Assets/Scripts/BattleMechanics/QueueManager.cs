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

    private int queueLength;

    private const int distBetweenIcons = 100;

    private void Awake()
    {
        instance = this;

        queue = new List<GameObject>();

        queueLength = 0;
    }

    public void addToQueue(string name)
    {
        Vector3 queuePosition = playerQueue.transform.position;
        if (name == "Attack")
        {
            queue.Add(Instantiate(attackIcon, new Vector3(queuePosition.x, queuePosition.y - (distBetweenIcons * (queueLength + 1)), queuePosition.z), Quaternion.Euler(0, 0, 0)));
        }
        else if (name == "Strong Attack")
        {
            queue.Add(Instantiate(strongAttackIcon, new Vector3(queuePosition.x, queuePosition.y - (distBetweenIcons * (queueLength + 1)), queuePosition.z), Quaternion.Euler(0, 0, 0)));
        }
        else if (name == "Run")
        {
            queue.Add(Instantiate(runIcon, new Vector3(queuePosition.x, queuePosition.y - (distBetweenIcons * (queueLength + 1)), queuePosition.z), Quaternion.Euler(0, 0, 0)));
        }
        else if (name == "Attack")
        {
            queue.Add(Instantiate(healIcon, new Vector3(queuePosition.x, queuePosition.y - (distBetweenIcons * (queueLength + 1)), queuePosition.z), Quaternion.Euler(0, 0, 0)));
        }
        queue[queueLength].transform.SetParent(playerQueue.transform, true);
        queueLength++;
    }

    public void removeTopFromQueue()
    {
        Debug.Log("Removed: " + queue[0]);
        if (queueLength > 1)
        {
            for (int i = 1; i < queueLength; i++)
            {
                queue[i].transform.position = new Vector3(queue[i].transform.position.x, queue[i].transform.position.y + distBetweenIcons, queue[i].transform.position.z);
            }
        }

        queue.Remove(queue[0]);
        queueLength--;
    }
}
