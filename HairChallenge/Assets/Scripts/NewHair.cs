using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewHair : MonoBehaviour
{
    Transform parent;
    [SerializeField]
    int addHair = 8;

    [SerializeField]
    GameObject partPrefab;

    [SerializeField]
    float partDistance = 0.29f;

    [SerializeField]
    Color color;

    bool newHairTime;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hair")
        {
            parent = other.gameObject.transform.parent;
            int hairLength = parent.childCount;

            Destroy(transform.gameObject, 0.01f);

            //Give color first

            for (int c = 0; c < hairLength; c++)
            {
                parent.transform.GetChild(c).gameObject.GetComponent<Renderer>().material.color = color;
            }

            for (int i = hairLength; i <= hairLength + addHair; i++)
            {
                if (i == hairLength)
                {
                    print(i - 1);
                    
                    parent.transform.GetChild(i - 1).GetComponent<Rigidbody>().drag = 0f;
                }
                else
                {
                    GameObject tmp;

                    Vector3 lastOne = parent.transform.Find((parent.transform.childCount).ToString()).transform.position;
                    Quaternion lastOneRot = parent.transform.Find((parent.transform.childCount).ToString()).transform.rotation;
                    parent.transform.Find((parent.transform.childCount).ToString()).transform.eulerAngles = new Vector3(90, 0, 0);

                    tmp = Instantiate(partPrefab, new Vector3(lastOne.x, lastOne.y, lastOne.z - partDistance), Quaternion.identity, parent.transform);
                    tmp.transform.eulerAngles = new Vector3(90, 0, 0);

                    tmp.name = parent.transform.childCount.ToString();

                    tmp.GetComponent<Renderer>().material.color = color;

                    //  GameObject a = parent.transform.GetChild(i - 1).gameObject;

                    tmp.GetComponent<CharacterJoint>().connectedBody = parent.transform.Find((parent.transform.childCount - 1).ToString()).GetComponent<Rigidbody>();

                    if (i == hairLength + addHair)
                    {
                        parent.transform.Find((parent.transform.childCount).ToString()).GetComponent<Rigidbody>().drag = 35f;
                        //parent.transform.GetChild(i).GetComponent<Rigidbody>().drag = 35f;
                    }
                }
            }
        }
    }
}
