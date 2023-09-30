using System;
using System.Collections.Generic;

namespace StructBenchmarking
{
    public class Experiments
    {
        public static ChartData BuildChartDataForArrayCreation(
            IBenchmark benchmark, int repetitionsCount) => 
            
            BuildChartData( benchmark, new ClassArrayCreationTask(0), new StructArrayCreationTask(0), 
                repetitionsCount, "Create array");

        public static ChartData BuildChartDataForMethodCall(
            IBenchmark benchmark, int repetitionsCount) =>
            
            BuildChartData(benchmark, new MethodCallWithClassArgumentTask(0), new MethodCallWithStructArgumentTask(0), 
                repetitionsCount, "Call method with argument");
        
        public static ChartData BuildChartData(IBenchmark benchmark, ITask TaskOne, 
            ITask TaskTwo, int repetitionsCount, string title)
        {
            var classesTimes = new List<ExperimentResult>();
            var structuresTimes = new List<ExperimentResult>();

            foreach (var result in Constants.FieldCounts)
            {
                var classTest = (ITask)Activator.CreateInstance(firstTask.GetType(), result);
                var structTest = (ITask)Activator.CreateInstance(secondTask.GetType(), result);
                
                classesTimes.Add(new ExperimentResult(result,
                    benchmark.MeasureDurationInMs(classTest, repetitionsCount)));
                structuresTimes.Add(new ExperimentResult(result,
                    benchmark.MeasureDurationInMs(structTest, repetitionsCount)));
            }

            return new ChartData
            {
                Title = title,
                ClassPoints = classesTimes,
                StructPoints = structuresTimes,
            };
        }
    }
}