using System;
using Godot;
using Godot.Collections;
using GodotUtilities;
using VampireSurvivors.scripts.components;
using VampireSurvivors.scripts.enemies;
using Array = Godot.Collections.Array;

namespace VampireSurvivors.scripts;

public partial class WaveManager : Node2D
{
	private PackedScene skeletonEnemyScene = GD.Load<PackedScene>("res://scenes/enemies/skeleton_enemy.tscn");

	private PackedScene skeletonWarriorEnemyScene = GD.Load<PackedScene>("res://scenes/enemies/skeleton_warrior_enemy.tscn");

	private int currentWaveIndex;

	private int spawnedEnemiesFromWave;

	private int deadEnemiesFromWave;

	private int[] waveAmounts = [10, 20, 30];

	private Timer timer = new() { OneShot = true, WaitTime = 0.3f };

	private Array<PackedScene> enemies = new();

	public override void _Ready()
	{
		enemies.Add(skeletonEnemyScene);
		enemies.Add(skeletonWarriorEnemyScene);

		AddChild(timer);

		timer.Timeout += TimerOnTimeout;

		StartWave(0);
	}

	private void StartWave(int index)
	{
		GD.Print($"Wave {index} started");

		spawnedEnemiesFromWave = 0;
		deadEnemiesFromWave = 0;
		currentWaveIndex = index;

		timer.Start();
	}

	private void EndWave()
	{
		if (currentWaveIndex >= waveAmounts.Length - 1)
		{
			GD.Print("You completed the game!");
			return;
		}

		GD.Print($"Wave {currentWaveIndex} ended");
		StartWave(currentWaveIndex + 1);
	}

	private void TimerOnTimeout()
	{
		SpawnEnemy();

		if (spawnedEnemiesFromWave < waveAmounts[currentWaveIndex])
		{
			timer.Start();
		}
	}

	private PackedScene GetRandomEnemyScene()
	{
		var index = Random.Shared.Next(0, enemies.Count);

		return enemies[index];
	}

	private void SpawnEnemy()
	{
		var randomEnemy = GetRandomEnemyScene();
		var spawnedEnemy = randomEnemy.Instantiate<Enemy>();
		var spawnPosition = GetRandomSpawn();
		spawnedEnemy.Name = $"Enemy {spawnedEnemiesFromWave}";
		spawnedEnemy.Position = spawnPosition;
		spawnedEnemy.OnDeath += OnEnemyDeath;

		GetTree().CurrentScene.AddChild(spawnedEnemy);

		spawnedEnemiesFromWave++;

		return;

		Vector2 GetRandomSpawn()
		{
			float angle = (float)(Random.Shared.NextDouble() * Math.PI * 2);
			float distance = Random.Shared.Next(600, 800);

			return distance * Vector2.FromAngle(angle);
		}
	}

	private void OnEnemyDeath(Enemy enemy)
	{
		enemy.OnDeath -= OnEnemyDeath;
		deadEnemiesFromWave++;

		if (deadEnemiesFromWave >= waveAmounts[currentWaveIndex])
		{
			EndWave();
		}
	}
}
