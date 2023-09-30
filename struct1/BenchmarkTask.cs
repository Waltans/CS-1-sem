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
            task.Run();
            var stopWatch = new Stopwatch();

            GC.Collect();                   
            GC.WaitForPendingFinalizers();
            

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
            private readonly int repeat = 10000;
            public StringBuilderTime(int repeat)
            {
                this.repeat = repeat;
            }
            [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]

            public void Run()
            {
                var stringBuilder = new StringBuilder();
                for (var i = 0; i < repeat; i++)
                    stringBuilder.Append('a');
                stringBuilder.ToString();
            }
        }
 
        public class StringConstructorTime : ITask
        {
			private readonly int repeat = 10000;
            public StringConstructorTime(int repeat)
            {
                this.repeat = repeat;
            }
            [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]

            public void Run()
            {
                new string('a', repeat);
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