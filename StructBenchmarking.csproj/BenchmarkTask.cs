using System;
using System.Diagnostics;
using System.Text;
using NUnit.Framework;

namespace StructBenchmarking
{
    public class Benchmark : IBenchmark
	{
        public double MeasureDurationInMs(ITask task, int repetitionCount)
        {
            task.Run();
            GC.Collect();                   
            GC.WaitForPendingFinalizers();
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (var i = 0; i < repetitionCount; i++)
                task.Run();
            stopwatch.Stop();
            var measureDuration = stopwatch.Elapsed.TotalMilliseconds / repetitionCount;
            return measureDuration;
        }
	}
    
    public class CreateStringBuilderString : ITask
    {
        private readonly int _stringSize;
        public CreateStringBuilderString(int size)
        {
            _stringSize = size;
        }
        public void Run()
        {
            var stringBuilderString = new StringBuilder();
            for (var i = 0; i < _stringSize; i++) 
                stringBuilderString.Append('a');
            var str = stringBuilderString.ToString();
        }
    }
 
    public class CreateStringConstructorString : ITask
    {
        private readonly int _stringSize;
        public CreateStringConstructorString(int size)
        {
            _stringSize = size;
        }
        public void Run()
        {
            var str = new string('a', _stringSize);
        }
    }
    
    [TestFixture]
    public class RealBenchmarkUsageSample
    {
        [Test]
        public void StringConstructorFasterThanStringBuilder()
        {
            const int count = 20000;
            const int size = 10000;
            
            var stringBuilderString = new CreateStringBuilderString(size);
            var stringConstructorString = new CreateStringConstructorString(size);
            var benchmark = new Benchmark();
            
            Assert.Less(benchmark.MeasureDurationInMs(stringConstructorString, count),
                        benchmark.MeasureDurationInMs(stringBuilderString, count));
        }
    }
}