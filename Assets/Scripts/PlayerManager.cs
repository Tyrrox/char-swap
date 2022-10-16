using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Manager used in an individual level to control the swapping and loading of the characters for the level.
    public static PlayerManager playerManager { get; private set; }

    private GameObject[] playerCharacters;
    private PlayerController[] playerCharacterConts;
    private BoxCollider2D[] playerCharacterColls;
    private int currentCharacterID = 0;
    private bool isTransforming = false;

    private Transform currentRespawnTransform;
    private List<Transform> oldRespawnTransforms;
    private float respawnTime = 1f;

    private int swapCounter = 0;

    void Start()
    {
        if (playerManager != null && playerManager != this)
        {
            Destroy(this);
        }
        else
        {
            playerManager = this;
        }
        InstantiatePlayerCharacters();
        oldRespawnTransforms = new List<Transform>();
    }
    void Update()
    {
        // Character swapping.
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(CycleCharacter());
        }
    }

    public void InstantiatePlayerCharacters()
    {
        GameObject[] playerCharacterPrefabs = GameManager.gameManager.GetPlayerCharacterPrefabs();
        int totalChars = playerCharacterPrefabs.Length;
        playerCharacters = new GameObject[totalChars];
        playerCharacterConts = new PlayerController[totalChars];
        playerCharacterColls = new BoxCollider2D[totalChars];

        // Get spawn locations
        GameObject playerSpawn = GameObject.Find("PlayerSpawn");
        GameObject disabledSpawn = GameObject.Find("DisabledSpawn");

        // Update respawn location to player spawn
        currentRespawnTransform = playerSpawn.transform;

        // Instantiate main player character first (always the first prefab)
        GameObject newPlayer = Instantiate(playerCharacterPrefabs[0], playerSpawn.transform.position, Quaternion.identity);
        playerCharacters[0] = newPlayer;
        playerCharacterConts[0] = newPlayer.GetComponent<PlayerController>();
        playerCharacterColls[0] = newPlayer.GetComponent<BoxCollider2D>();

        // Instantiate the rest of the players and disable them
        for (int i = 1; i < playerCharacterPrefabs.Length; i++)
        {
            newPlayer = Instantiate(playerCharacterPrefabs[i], disabledSpawn.transform.position, Quaternion.identity);
            playerCharacters[i] = newPlayer;
            playerCharacterConts[i] = newPlayer.GetComponent<PlayerController>();
            playerCharacterColls[i] = newPlayer.GetComponent<BoxCollider2D>();

            // Freeze the character so that they are frozen in the first transformation
            newPlayer.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

            newPlayer.SetActive(false);
        }
    }

    public IEnumerator CycleCharacter()
    {
        if (!isTransforming)
        {
            // Flag for only one transformation at a time
            isTransforming = true;

            // Get transformation objects
            GameObject currentChar = playerCharacters[currentCharacterID];
            PlayerController currentCharPCon = playerCharacterConts[currentCharacterID];
            BoxCollider2D currentCharColl = playerCharacterColls[currentCharacterID];

            // Complete transform out animation, wait until finishes
            yield return StartCoroutine(currentCharPCon.TransformCharacterOut());

            // character swap logic
            Vector2 charBottomPos = new Vector2(currentCharColl.bounds.center.x, currentCharColl.bounds.center.y - currentCharColl.bounds.extents.y);
            bool facingRight = currentCharPCon.IsFacingRight();
            currentChar.SetActive(false);

            currentCharacterID += 1;
            if (currentCharacterID >= playerCharacters.Length)
            {
                currentCharacterID = 0;
            }

            // change character and components
            currentChar = playerCharacters[currentCharacterID];
            currentChar.SetActive(true);
            currentCharPCon = playerCharacterConts[currentCharacterID];
            currentCharColl = playerCharacterColls[currentCharacterID];

            // change position so bottom of collider is at the same point in the world
            currentChar.transform.position = charBottomPos + new Vector2(currentChar.transform.position.x - currentCharColl.bounds.center.x,
                                                                         currentCharColl.bounds.extents.y + (currentChar.transform.position.y - currentCharColl.bounds.center.y));

            // Swap facing direction if it has changed since before

            if (currentCharPCon.IsFacingRight() != facingRight)
            {
                currentCharPCon.SetFacingRight(facingRight);
            }

            // Complete transform in animation, wait until finishes
            yield return StartCoroutine(currentCharPCon.TransformCharacterIn());

            isTransforming = false;

            // end of level swap counter
            swapCounter += 1;
        }
    }

    public int GetSwapCounter()
    {
        return swapCounter;
    }

    public void UpdateRespawnLocation(Transform newLocationTransform)
    {
        // Only update the transform if we're changing location and we haven't already been there
        if (currentRespawnTransform != newLocationTransform && !oldRespawnTransforms.Contains(newLocationTransform))
        {
            oldRespawnTransforms.Add(currentRespawnTransform);
            currentRespawnTransform = newLocationTransform;
        }
    }

    public IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(respawnTime);
        playerCharacters[currentCharacterID].transform.position = currentRespawnTransform.position;
        playerCharacterConts[currentCharacterID].Respawn();
    }
}
