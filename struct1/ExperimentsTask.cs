using System.Collections.Generic;

namespace StructBenchmarking
{
    public interface ICreationTask
    {
        ICreationTask Struct(int size);
        ICreationTask Classes(int size);

    }

    public class DoArray : ICreationTask
    {
        public ICreationTask Struct(int size)
        {
            return new StructArrayCreationTask(size);
        }
        public ICreationTask Classes(int size)
        {
            return new ClassArrayCreationTask(size);
        }
    }

    public class Build : ICreationTask
    {
        public ICreationTask Classes(int size)
        {
            return new MethodCallWithClassArgumentTask(size);
        }
        
        public ICreationTask Struct(int size)
        {
            return new MethodCallWithStructArgumentTask(size);
        }
    }
    
    public class Experiments
    {
        public static ChartData BuildChartDataForArrayCreation(
            IBenchmark benchmark, int repetitionsCount)
        {
            var classesTimes = new Class(new Classe)
            var structuresTimes = new List<ExperimentResult>();
            
            
            
            
            return new ChartData
            {
                Title = "Create array",
                ClassPoints = classesTimes,
                StructPoints = structuresTimes,
            };
        }

        public static ChartData BuildChartDataForMethodCall(
            IBenchmark benchmark, int repetitionsCount)
        {
            var classesTimes = new List<ExperimentResult>();
            var structuresTimes = new List<ExperimentResult>();

            foreach (var result in Constants.FieldCounts)
            {
                classesTimes.Add(new ExperimentResult(result,
                    benchmark.MeasureDurationInMs(new MethodCallWithClassArgumentTask(result), repetitionsCount)));

                structuresTimes.Add(new ExperimentResult(result,
                    benchmark.MeasureDurationInMs(new MethodCallWithStructArgumentTask(result), repetitionsCount)));
            }

                return new ChartData
            {
                Title = "Call method with argument",
                ClassPoints = classesTimes,
                StructPoints = structuresTimes,
            };
        }
    }
}