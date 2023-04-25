using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //ī�޶��� 1��Ī 3��Ī ��ȯ �� �÷��̾� ����

    //Ȱ��ȭ�� 3��Ī
    [SerializeField]
    private bool thirdPersonView = true;
    [SerializeField]
    private Vector3 offSet;
    [SerializeField]
    private float followSpeed = 0.15f;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(thirdPersonView)
        {
            thirdPersonViewCameraMove();
        }
    }

    private void thirdPersonViewCameraMove()
    {
        Vector3 camera_pos = player.transform.position + offSet;
        Vector3 lerp_pos = Vector3.Lerp(transform.position, camera_pos, followSpeed);
        transform.position = lerp_pos;
        transform.LookAt(player.transform);
    }
}
