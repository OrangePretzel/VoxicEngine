﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voxic.Chunks;
using Voxic.Math;
using Voxic.Rendering;
using Voxic.Worlds;

// TODO: Remove/Extract functionality from this class
public class TesterThing : MonoBehaviour
{
    public List<ChunkRenderer> chunks = new List<ChunkRenderer>();

    public Object ChunkPrefab;
    public GameObject chunkContainer;

    private World world;

    private void OnEnable()
    {
        Debug.Log("Tester Thing Started");
        CreateChunkContainer();

        WorldSettings settings = new WorldSettings(16, 1);
        world = new World(settings);
        StartCoroutine(CreateChunks());
    }

    private void CreateChunkContainer()
    {
        chunkContainer = null;
        chunkContainer = GameObject.Find("Chunk Container");
        if (chunkContainer != null)
        {
            Destroy(chunkContainer);
            chunks.Clear();
        }
        chunkContainer = new GameObject("Chunk Container");
    }

    private bool done = false;
    private string message = "";
    private System.DateTime startTime = System.DateTime.MinValue;
    private void OnGUI()
    {
        if (!done)
            GUI.Label(new Rect(0, 20, 1000, 100), string.Format("Elapsed Time (s): {0}\n{1}", (System.DateTime.Now - startTime).TotalSeconds, message));
        else
            GUI.Label(new Rect(0, 20, 1000, 100), string.Format("{0}", message));
    }

    private IEnumerator CreateChunks()
    {
        startTime = System.DateTime.Now;
        done = false;

        int s = 0;
        for (int i = -s; i <= s; i++)
        {
            for (int k = -s; k <= s; k++)
            {
                CreateChunk(new IntVector3(i, 0, k));
            }
            //Debug.LogFormat("{0}% done generation!", 100 * (s + i) / (2 * (float)s));
            message = string.Format("{0}% done generation!", 100 * (s + i) / (2 * (float)s));
            yield return new WaitForEndOfFrame();
        }

        for (int i = 0; i < chunks.Count; i++)
        {
            chunks[i].RenderChunkAsync();
            //Debug.LogFormat("{0}% done rendering!", 100 * i / (float)chunks.Count);
            message = string.Format("{0}% done rendering!", 100 * i / (float)chunks.Count);
            yield return new WaitForEndOfFrame();
        }

        message = string.Format("Completed in (s): {0}\nChunks: {1}\nVoxels: {2}", (System.DateTime.Now - startTime).TotalSeconds, (2 * s + 1) * (2 * s + 1), (2 * s + 1) * (2 * s + 1) * 16 * 16 * 16);
        done = true;
    }

    private void CreateChunk(IntVector3 chunkPos)
    {
        Chunk chunk = world.LoadChunk(chunkPos);

        // Instantiate chunk renderer
        GameObject crObj = (GameObject)Instantiate(ChunkPrefab, (Vector3)world.PosHelper.ChunkToWorldPosition(chunk.ChunkPosition), Quaternion.identity);
        ChunkRenderer cr = crObj.GetComponent<ChunkRenderer>();
        chunks.Add(cr);

        crObj.transform.parent = chunkContainer.transform;
        cr.SetChunk(chunk);
    }
}