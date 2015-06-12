using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace threading
{
	class Program
	{
		public static Synch synch = new Synch();

		static void Main(string[] args)
		{
			int n = -1;
			while (n != 0)
			{
				Console.Clear();
				Console.Write("1 Manual Thread\n2 Thread Wrapper\n3 Thread Pool\n>");
				try
				{
					n = int.Parse(Console.ReadLine());
				}
				catch
				{
					n = 0;
				}
				Console.Clear();
				if (n == 1)
					new ManualThread().StartWorkers();
				else if (n == 2)
					new ThreadWrapper().StartWorkers();
				else if (n == 3)
					new PoolThread().StartWorkers();
				Console.SetCursorPosition(0, Console.WindowHeight - 1);
				Console.Write("Press ENTER to continue.");
				Console.ReadLine();
			}
		}
	}
}