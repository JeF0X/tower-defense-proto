using CG.Combat;
using CG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CG.AI
{
	public class EnemyAI : AIStateMachine
	{
		public float speed = 10f;
		public bool attackHomebase;

		[NonSerialized] public PlaceableObject target = null;
		[NonSerialized] public Waypoint currentWaypoint = null;
		[NonSerialized] public List<Waypoint> path = null;
		[NonSerialized] public NavMeshAgent navMeshAgent;

		private ObjectManager objectManager;

		public ObjectManager ObjectManager => objectManager;
		

		private void Start()
		{
			navMeshAgent = GetComponent<NavMeshAgent>();
			try
			{
				objectManager = FindObjectOfType<ObjectManager>();
				currentWaypoint = GetComponentInParent<EnemySpawner>().GetStartWaypoint();
				SetAIState(new Begin(this));
			}
			catch (System.Exception)
			{
				Debug.LogError("ObjectManager not found");
				throw;
			}
		}

		private void Update()
		{
			AIState.Tick();
		}
	}
}
