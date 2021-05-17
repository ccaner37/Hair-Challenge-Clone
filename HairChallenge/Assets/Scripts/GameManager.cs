using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public bool isRunning;
	public bool endGame;

	private static GameManager instance = null;
	public static GameManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new GameObject("GameManager").AddComponent<GameManager>();
			}

			return instance;
		}
	}

	private void OnEnable()
	{
		instance = this;
	}
}
