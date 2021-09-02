using UnityEngine;

public class ProbeLauncher : MonoBehaviour
{
    [SerializeField] GameObject probe;
    [SerializeField] FloatVariable probeSpeed;
    [SerializeField] Transform startingLocation;
    [SerializeField] Transform aimTarget;
    [SerializeField] GameObject aimRecticle;
    [SerializeField] LineRenderer aimLine;
    [SerializeField] BoolVariable launchProbe;
    [SerializeField] BoolVariable probeActive;
    [SerializeField] BoolVariable explodeProbe;
    [SerializeField] GameEvent ProbeExploded;
    [SerializeField] GameObject explosionEffect;
    GameObject eEffect = null;
    [SerializeField] LayerMask probeHitLayers;
    [SerializeField] BoolVariable unableToLaunch;
    bool canLaunchProbe = true;

    void Start()
    {
        aimLine.gameObject.SetActive(false);
        probe.SetActive(false);
        aimRecticle.SetActive(false);
        eEffect = Instantiate(explosionEffect, startingLocation.position, Quaternion.identity);
        eEffect.SetActive(false);
    }

    void OnEnable()
    {
        launchProbe.ValuedChangeTrue += SetProbeActive;
        explodeProbe.ValuedChangeTrue += DisableProbe;
    }

    void OnDisable()
    {
        launchProbe.ValuedChangeTrue -= SetProbeActive;
        explodeProbe.ValuedChangeTrue -= DisableProbe;
    }

    void Update()
    {
        if (!unableToLaunch.Value)
            if (probeActive.Value)
                ActivateProbe();
    }

    public void SetProbeActive()
    {
        if (canLaunchProbe)
        {
            probeActive.Value = true;
            probe.transform.position = startingLocation.position;
            canLaunchProbe = false;
        }
    }

    public void DisableProbe()
    {
        Vector3 probePos = probe.transform.position;
        probe.transform.position = startingLocation.position;
        aimLine.gameObject.SetActive(false);
        aimRecticle.SetActive(false);
        if (!canLaunchProbe)
        {
            eEffect.transform.position = probePos;
            eEffect.SetActive(true);
        }
        probe.SetActive(false);
        ProbeExploded?.Invoke();
        probeActive.Value = false;
        canLaunchProbe = true;
    }

    void ActivateProbe()
    {
        Vector3 aimPos;

        aimLine.gameObject.SetActive(true);
        aimRecticle.SetActive(true);
        aimLine.positionCount = 2;
        aimLine.SetPosition(0, startingLocation.position);

        if (Physics.Raycast(startingLocation.position, aimTarget.position - startingLocation.position, out RaycastHit hitInfo, probeHitLayers))
        {
            aimLine.SetPosition(1, hitInfo.point);
            aimPos = hitInfo.point;
            aimRecticle.transform.position = hitInfo.point;
            aimRecticle.transform.rotation = Quaternion.LookRotation(-hitInfo.normal);
        }
        else
        {
            aimLine.SetPosition(1, aimTarget.position);
            aimPos = aimTarget.transform.position;
            aimRecticle.transform.position = aimTarget.position;
            aimRecticle.transform.rotation = aimTarget.rotation;
        }

        probe.SetActive(true);
        probe.transform.position = Vector3.MoveTowards(probe.transform.position, aimPos, probeSpeed.Value * Time.fixedDeltaTime);
        probe.transform.LookAt(aimTarget.position);
        if (probe.transform.position == aimTarget.transform.position)
            DisableProbe();
    }
}