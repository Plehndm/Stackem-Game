using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    public LocaleControlsetter LocaleControlsetter;

    public List<GameObject> prefabList;
    public GameObject enviornment;
    public GameObject spawnLocation;
    public Color selectedColor;
    private float moveSpeed = 0.05f;
    private bool objectAlreadyExists = false;
    private bool isActive = true;
    
    private Rigidbody rb;
    private Collider box;
    private GameObject currentBlock;
    private BlockLogic currentScript;

    private Color currentColor;
    private Color invalidPlacementColor;
    private Color validPlacementColor;

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

            currentColor = currentBlock.GetComponent<Renderer>().material.color;

            currentBlock.GetComponent<Renderer>().material.color = selectedColor;

            currentScript = currentBlock.GetComponent<BlockLogic>();

            objectAlreadyExists = true;
        }

        currentBlock.GetComponent<Renderer>().material.color = !currentScript.isColliding ? validPlacementColor : invalidPlacementColor;

        if (Input.GetKey(LocaleControlsetter.place) && isActive && !currentScript.isColliding)
        {
            isActive = false;
            rb.isKinematic = false;
            box.isTrigger = false;
            currentBlock.GetComponent<Renderer>().material.color = currentColor;
            currentBlock.layer = 3;
            StartCoroutine(Wait());
        }

        if (isActive)
        {
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

        // Create and apply camera relative movement
        Vector3 cameraRelativeMovement = forwardRelativeVerticalDirection + rightRelativeHorizontalDirection;
        currentBlock.transform.Translate(cameraRelativeMovement.normalized * moveSpeed, Space.World);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        isActive = true;
        objectAlreadyExists = false;
    }
}
