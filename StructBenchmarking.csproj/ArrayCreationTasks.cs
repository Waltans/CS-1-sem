using System;
using System.Runtime.CompilerServices;

namespace StructBenchmarking
{
    public class StructArrayCreationTask : ITask
    {
        private readonly int size;
        private object array;

        public StructArrayCreationTask(int size)
        {
            this.size = size;
        }
        [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
        public void Run()
        {
            switch (size)
            {
                case 16:
                    array = new S16[Constants.ArraySize];
                    break;
                case 32:
                    array = new S32[Constants.ArraySize];
                    break;
                case 64:
                    array = new S64[Constants.ArraySize];
                    break;
                case 128:
                    array = new S128[Constants.ArraySize];
                    break;
                case 256:
                    array = new S256[Constants.ArraySize];
                    break;
                case 512:
                    array = new S512[Constants.ArraySize];
                    break;
                default:
                    throw new ArgumentException();
            }
        }
    }


    public class ClassArrayCreationTask : ITask
    {
        private readonly int size;
        public C16[] C16;
        public C32[] C32;
        public C64[] C64;
        public C128[] C128;
        public C256[] C256;
        public C512[] C512;

        public ClassArrayCreationTask(int size)
        {
            this.size = size;
        }

        [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
        public void Run()
        {
            switch (size)
            {
                case 16:
                    C16 = new C16[Constants.ArraySize];
                    for (var i = 0; i < Constants.ArraySize; i++) C16[i] = new C16();
                    break;
                case 32:
                    C32 = new C32[Constants.ArraySize];
                    for (var i = 0; i < Constants.ArraySize; i++) C32[i] = new C32();
                    break;
                case 64:
                    C64 = new C64[Constants.ArraySize];
                    for (var i = 0; i < Constants.ArraySize; i++) C64[i] = new C64();
                    break;
                case 128:
                    C128 = new C128[Constants.ArraySize];
                    for (var i = 0; i < Constants.ArraySize; i++) C128[i] = new C128();
                    break;
                case 256:
                    C256 = new C256[Constants.ArraySize];
                    for (var i = 0; i < Constants.ArraySize; i++) C256[i] = new C256();
                    break;
                case 512:
                    C512 = new C512[Constants.ArraySize];
                    for (var i = 0; i < Constants.ArraySize; i++) C512[i] = new C512();
                    break;
                default:
                    throw new ArgumentException();
            }
        }
    }
}