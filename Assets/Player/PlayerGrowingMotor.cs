using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrowingMotor : MonoBehaviour
{
    [SerializeField]
    private GameObject tailPrefab;

    private GameObject lastTail;

    public List<GameObject> tails = new List<GameObject>();

    Player player;

    void Start()
    {
        player = GetComponentInParent<Player>();

        GameObject tail = Instantiate(tailPrefab);
        tail.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 1f);
        tail.GetComponent<TailData>().player = null;
        tail.GetComponent<TailData>().index = tails.Count;
        tail.name = player.name + " - Tail" + tails.Count;

        #region physics

        CharacterJoint joint = gameObject.AddComponent<CharacterJoint>();
        joint.connectedBody = tail.GetComponent<Rigidbody>();
        joint.anchor = new Vector3(0, 0, -0.5f);
        joint.axis = new Vector3(1, 0, 0);
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = new Vector3(0, 0, 0.5f);

        SoftJointLimitSpring TwistLimit = new SoftJointLimitSpring();
        TwistLimit.spring = 0;
        TwistLimit.damper = 0;
        joint.twistLimitSpring = TwistLimit;

        SoftJointLimit LowTwistLimit = new SoftJointLimit();
        TwistLimit.spring = 177f;
        TwistLimit.damper = 0;
        joint.lowTwistLimit = LowTwistLimit;

        SoftJointLimit HighTwistLimit = new SoftJointLimit();
        TwistLimit.spring = 177f;
        TwistLimit.damper = 0;
        joint.highTwistLimit = HighTwistLimit;

        SoftJointLimitSpring SwingLimitSpring = new SoftJointLimitSpring();
        TwistLimit.spring = 0;
        TwistLimit.damper = 0;
        joint.swingLimitSpring = SwingLimitSpring;

        SoftJointLimit SwingLimit1 = new SoftJointLimit();
        SwingLimit1.limit = 80f;
        SwingLimit1.bounciness = 0f;
        SwingLimit1.contactDistance = 0f;
        joint.swing1Limit = SwingLimit1;

        SoftJointLimit SwingLimit2 = new SoftJointLimit();
        SwingLimit2.limit = 0f;
        SwingLimit2.bounciness = 0f;
        SwingLimit2.contactDistance = 0f;
        joint.swing2Limit = SwingLimit2;

        joint.projectionDistance = 0f;
        joint.projectionAngle = 0f;

        #endregion physics

        lastTail = tail;
        tails.Add(tail);
    }

    public void AddTail(int amount = 1)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject tail = Instantiate(tailPrefab);

            TailData lastTailData = lastTail.GetComponent<TailData>();
            tail.transform.position = 
                new Vector3(lastTailData.nexTailLoacation.position.x, 
                    lastTailData.nexTailLoacation.position.y, 
                    lastTailData.nexTailLoacation.position.z);

            tail.transform.rotation = new Quaternion();

            tail.GetComponent<TailData>().player = player;
            tail.GetComponent<TailData>().index = tails.Count;
            tail.name = player.name + " - Tail" + tails.Count;

            #region physics

            CharacterJoint joint = lastTail.AddComponent<CharacterJoint>();
            joint.connectedBody = tail.GetComponent<Rigidbody>();
            joint.anchor = new Vector3(0, 0, -0.5f);
            joint.axis = new Vector3(1, 0, 0);
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = new Vector3(0, 0, 0.5f);

            SoftJointLimitSpring TwistLimit = new SoftJointLimitSpring();
            TwistLimit.spring = 0;
            TwistLimit.damper = 0;
            joint.twistLimitSpring = TwistLimit;

            SoftJointLimit LowTwistLimit = new SoftJointLimit();
            TwistLimit.spring = 177f;
            TwistLimit.damper = 0;
            joint.lowTwistLimit = LowTwistLimit;

            SoftJointLimit HighTwistLimit = new SoftJointLimit();
            TwistLimit.spring = 177f;
            TwistLimit.damper = 0;
            joint.highTwistLimit = HighTwistLimit;

            SoftJointLimitSpring SwingLimitSpring = new SoftJointLimitSpring();
            TwistLimit.spring = 0;
            TwistLimit.damper = 0;
            joint.swingLimitSpring = SwingLimitSpring;

            SoftJointLimit SwingLimit1 = new SoftJointLimit();
            SwingLimit1.limit = 80f;
            SwingLimit1.bounciness = 0f;
            SwingLimit1.contactDistance = 0f;
            joint.swing1Limit = SwingLimit1;

            SoftJointLimit SwingLimit2 = new SoftJointLimit();
            SwingLimit2.limit = 0f;
            SwingLimit2.bounciness = 0f;
            SwingLimit2.contactDistance = 0f;
            joint.swing2Limit = SwingLimit2;

            joint.projectionDistance = 0f;
            joint.projectionAngle = 0f;

            #endregion physics

            lastTail = tail;
            tails.Add(tail);
        }

        player.score = tails.Count;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.GetComponent<TailData>() != null)
        {
            if (collision.gameObject.GetComponent<TailData>().player != null)
            {
                Player hitedPlayer = collision.gameObject.GetComponent<TailData>().player;
                player.Die(hitedPlayer.name);
            }
        }
        if (collision.gameObject.GetComponent<LootData>() != null)
        {
            LootData loot = collision.gameObject.GetComponent<LootData>();
            player.score++;
            AddTail(loot.LootSize);

            Destroy(loot.gameObject);
        }
    }

    private void OnDisable()
    {
        foreach(GameObject tail in tails)
        {
            if(tail != null)
                tail.SetActive(false);
        }
    }

    private void OnEnable()
    {
        foreach(GameObject tail in tails)
        {
            if (tail != null)
                tail.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        foreach(GameObject tail in tails)
        {
            Destroy(tail);
        }
    }
}
