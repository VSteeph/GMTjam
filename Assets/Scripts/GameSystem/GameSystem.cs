﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public PlayerController Player1;
    public PlayerController Player2;
    public bool isGodMode;
    public bool isPlayer1Alive;
    public bool isPlayer2Alive;

    [Header("Explosion")]
    public LayerMask ExplosionLayer;

    private PlayerController lastPlayerAlive;
    private static GameSystem gameSystem;
    public static GameSystem Instance
    {
        get
        {
            if(gameSystem == null)
            {
                gameSystem = GameObject.Find("GameManager").GetComponent<GameSystem>();
            }
            return gameSystem;
        }
    }

    public void SetAlive(PlayerController player)
    {
        if (player == Player1)
            isPlayer1Alive = true;
        if (player == Player2)
            isPlayer2Alive = true;
    }

    public void KillPlayer(PlayerController player)
    {
        if(player == Player1)
        {
            isPlayer1Alive = false;
            lastPlayerAlive = Player2;
        }
        if(player == Player2)
        {
            isPlayer2Alive = false;
            lastPlayerAlive = Player1;
        }
        if(CheckGameOver())
        {
            GameOver();
        }
        else
        {
            DisablePlayer(player);
        }
    }

    public bool CheckGameOver()
    {
        if (!isPlayer2Alive && !isPlayer1Alive && !isGodMode)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
    }

    private void DisablePlayer(PlayerController player)
    {
        if(isGodMode)
        {
            player.SwapToEnergyVisual();
            player.EnablentityPhysic();
            player.DisableEnemyDetection();
        }
        else
        {
            player.DisableEntityVisual();
            player.DisablEntityPhysic();
            player.DisableEnemyDetection();
            player.transform.parent = lastPlayerAlive.transform;
            player.transform.localPosition = Vector3.zero;
        }
    }

    private void EnablePlayer()
    {

    }

    #region Utilities
    public void TriggerExplosion(Vector2 orginExplosion, float Radius, float force)
    {
        var colliders = Physics2D.OverlapCircleAll(orginExplosion, Radius, ExplosionLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            var currentRb = colliders[i].GetComponent<Rigidbody2D>();
            if (currentRb  != null)
            {
                var direction = currentRb.position - orginExplosion;
                float relativeForce = Mathf.InverseLerp(Radius, 0, direction.magnitude);
                var normalizeDirection = direction.normalized;
                currentRb.AddForceAtPosition(normalizeDirection * force * relativeForce, new Vector2(transform.position.x, transform.position.y) + normalizeDirection);
            }
        }

    }
    #endregion

    #region EntityManagement
    public IEnumerator DestroyEntity(GameObject objectToDestroy)
    {
        yield return new WaitForSeconds(1);
        Destroy(objectToDestroy);
    }

    #endregion
}
