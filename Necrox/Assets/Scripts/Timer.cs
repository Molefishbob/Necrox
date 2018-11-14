using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
	{
		private float _currentTime;

		/// <summary>
		/// Tells if the timer has been finished
		/// </summary>
		public bool IsCompleted
		{
			get
			{
				return CurrentTime <= 0;
			}
		}

		/// <summary>
		/// Tells if the timer is running or not
		/// </summary>
		public bool IsRunning { get; private set; }

		// Kertoo tämänhetkisen ajan
		public float CurrentTime
		{
			get { return _currentTime; }
			private set
			{
				_currentTime = Mathf.Max(value, 0);
			}
		}

		/// <summary>
		/// Tells if the timer is running or not
		/// </summary>
		public void StartTimer()
		{
			IsRunning = true;
		}

		/// <summary>
		/// Tells if the timer is running or not
		/// </summary>
		public void Stop()
		{
			IsRunning = false;
		}

		/// <summary>
		/// Sets the time
		/// </summary>
		public void SetTime(float time)
		{
			if(!IsRunning)
			{
				CurrentTime = time;
			}
		}

		private void Awake()
		{
			Stop();
		}

		private void Update()
		{
			if(IsRunning)
			{
				CurrentTime -= Time.deltaTime;
				if(IsCompleted)
				{
					Stop();
				}
			}
		}
	}
