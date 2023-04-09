using Photon.Pun;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public LayerMask collisionMask;
    float speed = 10;
    float damage = 1;
    float lifeTime = 3f;
    float skinWidth = 0.1f;

    PhotonView pv;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }

    private void Start()
    {
        Destroy(gameObject, lifeTime);

        Collider[] initialCollisions = Physics.OverlapSphere(transform.position, 0.1f, collisionMask);
        if (initialCollisions.Length > 0)
        {
            OnHitObject(initialCollisions[0], transform.position);
        }
    }
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    private void Update()
    {
        float moveDistance = speed * Time.deltaTime;

        CheckCollision(moveDistance);       //Here on collision enter is not used becoz we dont want to add the rigidbody...


        transform.Translate(transform.forward * Time.deltaTime * speed, Space.World);
        /// rememeber very importnadt...
        //        transform.Translate(transform.forward*Time.deltaTime*speed);
        // here transform will not come... transform point toward one direction...
    }
    void CheckCollision(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, moveDistance + skinWidth, collisionMask, QueryTriggerInteraction.Collide))
        {// we want it to collide with triggers..
         //which is why queryTriggerIntegraction is means..

            Destroy(gameObject);
            OnHitObject(hit.collider, hit.point);

        }
    }


    void OnHitObject(Collider c, Vector3 hitPoint)
    {
        IDamagable damagable = c.gameObject.GetComponent<IDamagable>();
        if (damagable != null)
        {
            pv.RPC("RPC_Shoot", RpcTarget.All, hitPoint);
            damagable.TakeDamage(damage);
        }
    }

    [PunRPC]
    void RPC_Shoot(Vector3 hitPosition)
    {
        if (pv.IsMine)
            Debug.Log(hitPosition.ToString());
    }
}
