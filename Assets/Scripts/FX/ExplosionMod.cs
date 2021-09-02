using UnityEngine;

public class ExplosionMod : MonoBehaviour
{
    [SerializeField] float timeAlive;
    float cTime;

    void OnEnable()
    {
        cTime = timeAlive;
    }

    void OnDisable()
    {
        cTime = timeAlive;
    }

    void LateUpdate()
    {
        cTime = cTime < 0 ? 0 : cTime -= Time.fixedDeltaTime;
        if (cTime.Equals(0))
            gameObject.SetActive(false);
    }
}
