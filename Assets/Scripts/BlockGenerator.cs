using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    public LevelBlock levelBlock;
    public LevelBlock lastLevelBlock;
    public LevelBlock currentLevelBlock;
    public Transform blockPipe;
    public float blockGenerationTime = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        AddNewBlock();
        InvokeRepeating("AddNewBlockPipe", 0.0f, blockGenerationTime);
    }

    // Update is called once per frame
    void Update()
    {
        float xPosCam = Camera.main.transform.position.x;
        float xPosOld = lastLevelBlock.exitPoint.position.x;

        if (xPosCam > (xPosOld + lastLevelBlock.width / 2))
        {
            RemoveOldBlock();
        }
    }

    public void AddNewBlock()
    {
        LevelBlock block = (LevelBlock) Instantiate(levelBlock);
        block.transform.SetParent(this.transform, false);
        Vector3 blockPosition = Vector3.zero;
        blockPosition = new Vector3(
            lastLevelBlock.exitPoint.position.x + block.width / 2,
            lastLevelBlock.exitPoint.position.y,
            lastLevelBlock.exitPoint.position.z
        );
        block.transform.position = blockPosition;
        currentLevelBlock = block;
    }

    public void RemoveOldBlock()
    {
        Destroy(lastLevelBlock.gameObject);
        lastLevelBlock = currentLevelBlock;
        AddNewBlock();
    }

    public void AddNewBlockPipe()
    {
        float distanceToGenerate = levelBlock.width / 2;
        float randomHeight = Random.Range(-4, 8);
        Transform pipeBlock = Instantiate(blockPipe);
        Vector3 pipePos = Vector3.zero;
        pipePos = new Vector3(Camera.main.transform.position.x + distanceToGenerate, randomHeight, 0);
        pipeBlock.transform.position = pipePos;
    }
}
