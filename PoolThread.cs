using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace threading
{
	// Using the Thread Pool
	class PoolThread
	{
		static int spinner = 0;
		static int waiting = 0;
		static int running = 0;
		static int completed = 0;

		public void StartWorkers()
		{
			Random r = new Random();
			for (int i = 0; i < 100; i++)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(Worker), new Parms(5000));
				waiting++;
			}
			while (running != 0)
			{
				DumpStatus();
				Thread.Sleep(50);
			}
			DumpStatus();
		}

		private void DumpStatus()
		{
			Console.SetCursorPosition(0, 0);
			switch (spinner++ % 4)
			{
				case 0: Console.WriteLine(@"|"); break;
				case 1: Console.WriteLine(@"/"); break;
				case 2: Console.WriteLine(@"-"); break;
				case 3: Console.WriteLine(@"\"); break;
			}
			Console.WriteLine("  waiting: {0,3}", waiting);
			Console.WriteLine("  running: {0,3}", running);
			Console.WriteLine("completed: {0,3}", completed);
			Console.WriteLine("           ---");
			Console.WriteLine("           {0,3}", waiting + running + completed);
		}

		private void Worker(object state)
		{
			Interlocked.Decrement(ref waiting);
			Interlocked.Increment(ref running);
			Parms parm = (Parms)state;
			Thread.Sleep(parm.interval);
			Interlocked.Decrement(ref running);
			Interlocked.Increment(ref completed);
		}
	}

	class Parms
	{
		public int interval;

		public Parms(int interval)
		{
			this.interval = interval;
		}
	}
}
