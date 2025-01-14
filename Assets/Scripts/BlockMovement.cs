using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    public LocaleControlsetter LocaleControlsetter;
    public List<GameObject> prefabList;
    public GameObject enviornment;
    public float rightEdge = 13;
    public float leftEdge = -13;
    public float bottomEdge = -13;
    public float topEdge = 13;
    public GameObject spawnLocation;
    private Vector3 distFromCenter = new Vector3(0, 0, 0);
    private float moveSpeed = 0.05f;
    private bool objectAlreadyExists = false;
    private bool isActive = true;

    private Rigidbody rb;
    private Collider box;
    private GameObject currentBlock;
    private BlockLogic currentScript;

    [Header("Object Placement Colors")]
    public Color placedColor;
    public Color invalidPlacementColor;
    public Color validPlacementColor;

    // Update is called once per frame
    void Update()
    {
        if (!objectAlreadyExists)
        {
            currentBlock = Instantiate(prefabList[(int)(Random.value * prefabList.Count)], 
                        spawnLocation.transform.position, 
                        Quaternion.Euler(0, 0, 0), 
                        enviornment.transform);

            rb = currentBlock.GetComponent<Rigidbody>();
            rb.isKinematic = true;

            box = currentBlock.GetComponent<Collider>();
            box.isTrigger = true;

            currentScript = currentBlock.GetComponent<BlockLogic>();

            objectAlreadyExists = true;
        }

        if (Input.GetKey(LocaleControlsetter.place) && isActive && !currentScript.isColliding)
        {
            isActive = false;
            rb.isKinematic = false;
            box.isTrigger = false;
            currentBlock.GetComponent<Renderer>().material.color = placedColor;
            currentBlock.layer = 3;
            StartCoroutine(Wait());
        }

        if (isActive)
        {
            currentBlock.GetComponent<Renderer>().material.color = !currentScript.isColliding ? validPlacementColor : invalidPlacementColor;
            currentBlock.transform.position = new Vector3(currentBlock.transform.position.x, spawnLocation.transform.position.y, currentBlock.transform.position.z);
            MoveBlockRelativeToCamera();
            RotateBlock();
        }
    }

    void RotateBlock(){
        if (Input.GetKeyDown(LocaleControlsetter.rotateBlockLeft)) {
            currentBlock.transform.Rotate(new Vector3(0, -90, 0), Space.World);
        }
        if (Input.GetKeyDown(LocaleControlsetter.rotateBlockRight)) {
            currentBlock.transform.Rotate(new Vector3(0, 90, 0), Space.World);
        }

        if (Input.GetKeyDown(LocaleControlsetter.rotateBlockForward)) {
            currentBlock.transform.Rotate(LocaleControlsetter.localeCamera.transform.right * 90, Space.World);
        }
        if (Input.GetKeyDown(LocaleControlsetter.rotateBlockBack)) {
            currentBlock.transform.Rotate(LocaleControlsetter.localeCamera.transform.right * -90, Space.World);
        }
    }

    void MoveBlockRelativeToCamera()
    {
        // Get Player Input
        float blockVerticalMovement = 0;
        if (Input.GetKey(LocaleControlsetter.upInput))
        {
            blockVerticalMovement = 1;
        } else if (Input.GetKey(LocaleControlsetter.downInput))
        {
            blockVerticalMovement = -1;
        }

        float blockHorizontalMovement = 0;
        if (Input.GetKey(LocaleControlsetter.rightInput))
        {
            blockHorizontalMovement = 1;
        }
        else if (Input.GetKey(LocaleControlsetter.leftInput))
        {
            blockHorizontalMovement = -1;
        }

        // Get Camera Normalized Directional Vectors 
        Vector3 forward = LocaleControlsetter.localeCamera.transform.forward;
        Vector3 right = LocaleControlsetter.localeCamera.transform.right;
        forward.y = 0;
        right.y = 0;
        forward = forward.normalized;
        right = right.normalized;

        // Create direction-relative-input vectors
        Vector3 forwardRelativeVerticalDirection = blockVerticalMovement * forward;
        Vector3 rightRelativeHorizontalDirection = blockHorizontalMovement * right;

        // Create camera relative movement
        Vector3 cameraRelativeMovement = forwardRelativeVerticalDirection + rightRelativeHorizontalDirection;
        
        // Check if block is trying to move out of bounds
        if(distFromCenter.x >= rightEdge) 
        {
            cameraRelativeMovement.x = cameraRelativeMovement.x >=0 ? 0 : cameraRelativeMovement.x;
        }
        else if(distFromCenter.x <= leftEdge) 
        {
            cameraRelativeMovement.x = cameraRelativeMovement.x <=0 ? 0 : cameraRelativeMovement.x;
        }
        else if(distFromCenter.z >= topEdge) 
        {
            cameraRelativeMovement.z = cameraRelativeMovement.z >=0 ? 0 : cameraRelativeMovement.z;
        }
        else if(distFromCenter.z <= bottomEdge) 
        {
            cameraRelativeMovement.z = cameraRelativeMovement.z <=0 ? 0 : cameraRelativeMovement.z;
        }

        // Apply camera relative movement
        currentBlock.transform.Translate(cameraRelativeMovement.normalized * moveSpeed, Space.World);
        distFromCenter += cameraRelativeMovement.normalized * moveSpeed;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        isActive = true;
        objectAlreadyExists = false;
    }
}
