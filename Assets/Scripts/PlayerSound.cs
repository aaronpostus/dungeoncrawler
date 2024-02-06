using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSound : MonoBehaviour
{
    // create a unity event reference and instance for each sound
    [SerializeField] private FMODUnity.EventReference _footsteps;
    [SerializeField] private FMODUnity.EventReference _pain;
    [SerializeField] private FMODUnity.EventReference _death;
    [SerializeField] private FMODUnity.EventReference _jumpStart;
    [SerializeField] private FMODUnity.EventReference _jumpLand;
    [SerializeField] private FMODUnity.EventReference _kick;

    private FMOD.Studio.EventInstance footsteps;
    private FMOD.Studio.EventInstance pain;
    private FMOD.Studio.EventInstance death;
    private FMOD.Studio.EventInstance jumpStart;
    private FMOD.Studio.EventInstance jumpLand;
    private FMOD.Studio.EventInstance kick;

    // if event is active, create an instance to call proper method
    private void Awake()
    {
        if (!_footsteps.IsNull)
        {
            footsteps = FMODUnity.RuntimeManager.CreateInstance(_footsteps);
        }
        if (!_pain.IsNull)
        {
            pain = FMODUnity.RuntimeManager.CreateInstance(_pain);
        }
        if (!_death.IsNull)
        {
            death = FMODUnity.RuntimeManager.CreateInstance(_death);
        }
        if (!_jumpStart.IsNull)
        {
            jumpStart = FMODUnity.RuntimeManager.CreateInstance(_jumpStart);
        }
        if (!_jumpLand.IsNull)
        {
            jumpLand = FMODUnity.RuntimeManager.CreateInstance(_jumpLand);
        }
        if (!_kick.IsNull)
        {
            kick = FMODUnity.RuntimeManager.CreateInstance(_kick);
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

    public void PlayPain()
    {
        if (pain.isValid())
        {
            pain.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
            pain.start();

        }
    }

    public void PlayDeath()
    {
        if (death.isValid())
        {
            death.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
            death.start();

        }
    }

    public void PlayJumpStart()
    {
        if (jumpStart.isValid())
        {
            jumpStart.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
            GroundSwitch();
            jumpStart.start();

        }
    }

    public void PlayJumpLand()
    {
        if (jumpLand.isValid())
        {
            jumpLand.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
            GroundSwitch();
            jumpLand.start();
        }
    }

    public void PlayKick()
    {
        if (kick.isValid())
        {
            kick.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
            kick.start();
        }
    }

    private void GroundSwitch()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position + Vector3.up * 0.5f, -Vector3.up);

        if (Physics.Raycast(ray, out hit, 1.0f, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            GameObject obj = hit.collider.GameObject();

            switch (obj.tag) // placeholder for now; based on materials in future
            {
                case "Ground":
                    footsteps.setParameterByName("Terrain", 0);
                    jumpStart.setParameterByName("Terrain", 0);
                    jumpLand.setParameterByName("Terrain", 0);
                    break;
                case "Water":
                    footsteps.setParameterByName("Terrain", 1);
                    jumpStart.setParameterByName("Terrain", 1);
                    jumpLand.setParameterByName("Terrain", 1);
                    break;
                case "Gravel":
                    footsteps.setParameterByName("Terrain", 2);
                    jumpStart.setParameterByName("Terrain", 2);
                    jumpLand.setParameterByName("Terrain", 2);
                    break;
                default:
                    footsteps.setParameterByName("Terrain", 0);
                    jumpStart.setParameterByName("Terrain", 0);
                    jumpLand.setParameterByName("Terrain", 0);
                    break;
            }
        }
    }
}
