using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] private FMODUnity.EventReference _footsteps;
    private FMOD.Studio.EventInstance footsteps;

    private void Awake()
    {
        if (!_footsteps.IsNull)
        {
            footsteps = FMODUnity.RuntimeManager.CreateInstance(_footsteps);
        }
    }
    public void PlayFootsteps()
    {
        if (footsteps.isValid())
        {
            footsteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
            GroundSwitch();
            footsteps.start();
        }
    }

    private void GroundSwitch()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position + Vector3.up * 0.5f, -Vector3.up);

        if (Physics.Raycast(ray, out hit, 1.0f, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            GameObject obj = hit.collider.GameObject();

            switch (obj.tag)
            {
                case "Ground":
                    footsteps.setParameterByName("Footsteps", 0);
                    break;
                case "Water":
                    footsteps.setParameterByName("Footsteps", 1);
                    break;
                case "Gravel":
                    footsteps.setParameterByName("Footsteps", 2);
                    break;
                default:
                    footsteps.setParameterByName("Footsteps", 0);
                    break;
            }
        }
    }
}
