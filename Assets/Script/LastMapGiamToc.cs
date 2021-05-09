
using UnityEngine;

public class LastMapGiamToc : MonoBehaviour
{
    public ThirdMovement playermovement;
    public LayerMask Player;
    public SoundBG soundBG;
    [SerializeField]
    float radius = 3f;

    void Update()
    {
        if (Physics.CheckSphere(transform.position, radius, Player))
        {
            playermovement.speed = 4f;
            soundBG.InBoss();
            Destroy(this.gameObject);
        }
    }

}
