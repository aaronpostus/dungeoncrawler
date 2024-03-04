using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmBattleManager : MonoBehaviour
{

    public static RhythmBattleManager instance { get; private set; }

    [SerializeField] public GameObject rhythmUIPrefab;

    private GameObject rhythmUI;

    void Start()
    {
        instance = this;
    }

    public void ActivateRhythmUI()
    {
        rhythmUI = Instantiate(rhythmUIPrefab);
    }

    public void DeactivateRhythmUI()
    {
        Destroy(rhythmUI);
    }

    public bool isRhythmUIActive()
    {
        return rhythmUI != null;
    }
}
