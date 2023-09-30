using System;
using System.Diagnostics;
using System.Text;
using NUnit.Framework;
using NUnit;
 
namespace StructBenchmarking
{
    public class Benchmark : IBenchmark
    {
        public double MeasureDurationInMs(ITask task, int repetitionCount)
        {
            GC.Collect();                   
            GC.WaitForPendingFinalizers();
            
            task.Run();

            var stopWatch = new Stopwatch();

            stopWatch.Start();
            
            for (var i = 0; i < repetitionCount; i++)
                task.Run();
            
            stopWatch.Stop();
            
            return (double)stopWatch.ElapsedMilliseconds / repetitionCount;
        }
    }

    [TestFixture]
    public class RealBenchmarkUsageSample
    {
        public class StringBuilderTime : ITask
        {
			private readonly int countsRepeat;

			public countsRepeatTask(int countsRepeat)
			{
    			this.countsRepeat = countsRepeat;
			}

            public void Run()
            {
                var stringBuilder = new StringBuilder();
                for (var i = 0; i < countsRepeat; i++)
                    stringBuilder.Append('a');
                stringBuilder.ToString();
            }
        }
 
        public class StringConstructorTime : ITask
        {
			private readonly int countsRepeat;

			public countsRepeatTask(int countsRepeat)
			{
    			this.countsRepeat = countsRepeat;
			}

            public void Run()
            {
                new string('a', countsRepeat);
            }
        }

        [Test]
        
        public void StringConstructorFasterThanStringBuilder()
        {
            var stringBuilder = new StringBuilderTime();
            var stringConstructor = new StringConstructorTime();
            var repeats = 10000;
            var benchmark = new Benchmark();

            var timeStringBuilder = benchmark.MeasureDurationInMs(stringBuilder, repeats);
            var timeStringConstructor = benchmark.MeasureDurationInMs(stringConstructor, repeats);
            
            Assert.Less(timeStringConstructor, timeStringBuilder);
        }
    }
}