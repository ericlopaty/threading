using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace threading
{
	// wrapper class encapsulates the logic to start the thread with parameters passed into its constructor.
	class ThreadWrapper
	{
		public void StartWorkers()
		{
			string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			Thread[] t = new Thread[4];
			t[0] = new Wrapper(letters, 0).Start();
			t[1] = new Wrapper(letters, 1).Start();
			t[2] = new Wrapper(letters, 2).Start();
			t[3] = new Wrapper(letters, 3).Start();
			for (int i = 0; i < t.Length; i++)
				t[i].Join();
		}
	}

	class Wrapper
	{
		static Random rand = new Random(DateTime.Now.Millisecond);
		string letters;
		int top;
		Thread thread;

		public Wrapper(string letters, int top)
		{
			this.letters = letters;
			this.top = top;
		}

		void ThreadProc()
		{
			try
			{
				DumpLetters(letters, top);
			}
			catch (System.Threading.ThreadInterruptedException)
			{
			}
		}

		public Thread Start()
		{
			thread = new Thread(new ThreadStart(ThreadProc));
			thread.Start();
			return thread;
		}

		public static void DumpLetters(string letters, int top)
		{
			int left = 0;
			for (int i = 0; i < letters.Length; i++)
			{
				try
				{
					Monitor.Enter(Program.synch);
					Console.SetCursorPosition(left++, top);
					Console.Write(letters[i]);
				}
				finally
				{
					Monitor.Exit(Program.synch);
				}
				Thread.Sleep(rand.Next(500));
			}
		}
	}
}
