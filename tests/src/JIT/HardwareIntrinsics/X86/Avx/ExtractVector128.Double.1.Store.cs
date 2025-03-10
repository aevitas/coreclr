// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

/******************************************************************************
 * This file is auto-generated from a template file by the GenerateTests.csx  *
 * script in tests\src\JIT\HardwareIntrinsics\X86\Shared. In order to make    *
 * changes, please update the corresponding template and run according to the *
 * directions listed in the file.                                             *
 ******************************************************************************/

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace JIT.HardwareIntrinsics.X86
{
    public static partial class Program
    {
        private static void ExtractVector128Double1Store()
        {
            var test = new SimpleUnaryOpTest__ExtractVector128Double1Store();

            if (test.IsSupported)
            {
                // Validates basic functionality works, using Unsafe.Read
                test.RunBasicScenario_UnsafeRead();

                if (Avx.IsSupported)
                {
                    // Validates basic functionality works, using Load
                    test.RunBasicScenario_Load();

                    // Validates basic functionality works, using LoadAligned
                    test.RunBasicScenario_LoadAligned();
                }

                // Validates calling via reflection works, using Unsafe.Read
                test.RunReflectionScenario_UnsafeRead();

                if (Avx.IsSupported)
                {
                    // Validates calling via reflection works, using Load
                    test.RunReflectionScenario_Load();

                    // Validates calling via reflection works, using LoadAligned
                    test.RunReflectionScenario_LoadAligned();
                }

                // Validates passing a static member works
                test.RunClsVarScenario();

                // Validates passing a local works, using Unsafe.Read
                test.RunLclVarScenario_UnsafeRead();

                if (Avx.IsSupported)
                {
                    // Validates passing a local works, using Load
                    test.RunLclVarScenario_Load();

                    // Validates passing a local works, using LoadAligned
                    test.RunLclVarScenario_LoadAligned();
                }

                // Validates passing the field of a local works
                test.RunLclFldScenario();

                // Validates passing an instance member works
                test.RunFldScenario();
            }
            else
            {
                // Validates we throw on unsupported hardware
                test.RunUnsupportedScenario();
            }

            if (!test.Succeeded)
            {
                throw new Exception("One or more scenarios did not complete as expected.");
            }
        }
    }

    public sealed unsafe class SimpleUnaryOpTest__ExtractVector128Double1Store
    {
        private const int VectorSize = 32;

        private const int Op1ElementCount = VectorSize / sizeof(Double);
        private const int RetElementCount = 16 / sizeof(Double);

        private static Double[] _data = new Double[Op1ElementCount];

        private static Vector256<Double> _clsVar;

        private Vector256<Double> _fld;

        private SimpleUnaryOpTest__DataTable<Double, Double> _dataTable;

        static SimpleUnaryOpTest__ExtractVector128Double1Store()
        {
            var random = new Random();

            for (var i = 0; i < Op1ElementCount; i++) { _data[i] = (double)(random.NextDouble()); }
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector256<Double>, byte>(ref _clsVar), ref Unsafe.As<Double, byte>(ref _data[0]), VectorSize);
        }

        public SimpleUnaryOpTest__ExtractVector128Double1Store()
        {
            Succeeded = true;

            var random = new Random();

            for (var i = 0; i < Op1ElementCount; i++) { _data[i] = (double)(random.NextDouble()); }
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector256<Double>, byte>(ref _fld), ref Unsafe.As<Double, byte>(ref _data[0]), VectorSize);

            for (var i = 0; i < Op1ElementCount; i++) { _data[i] = (double)(random.NextDouble()); }
            _dataTable = new SimpleUnaryOpTest__DataTable<Double, Double>(_data, new Double[RetElementCount], VectorSize);
        }

        public bool IsSupported => Avx.IsSupported;

        public bool Succeeded { get; set; }

        public void RunBasicScenario_UnsafeRead()
        {
            Avx.ExtractVector128(
                (Double*)_dataTable.outArrayPtr,
                Unsafe.Read<Vector256<Double>>(_dataTable.inArrayPtr),
                1
            );

            ValidateResult(_dataTable.inArrayPtr, _dataTable.outArrayPtr);
        }

        public void RunBasicScenario_Load()
        {
            Avx.ExtractVector128(
                (Double*)_dataTable.outArrayPtr,
                Avx.LoadVector256((Double*)(_dataTable.inArrayPtr)),
                1
            );

            ValidateResult(_dataTable.inArrayPtr, _dataTable.outArrayPtr);
        }

        public void RunBasicScenario_LoadAligned()
        {
            Avx.ExtractVector128(
                (Double*)_dataTable.outArrayPtr,
                Avx.LoadAlignedVector256((Double*)(_dataTable.inArrayPtr)),
                1
            );

            ValidateResult(_dataTable.inArrayPtr, _dataTable.outArrayPtr);
        }

        public void RunReflectionScenario_UnsafeRead()
        {
            typeof(Avx).GetMethod(nameof(Avx.ExtractVector128), new Type[] { typeof(Double*), typeof(Vector256<Double>), typeof(byte) })
                                     .Invoke(null, new object[] {
                                        Pointer.Box(_dataTable.outArrayPtr, typeof(Double*)),
                                        Unsafe.Read<Vector256<Double>>(_dataTable.inArrayPtr),
                                        (byte)1
                                     });

            ValidateResult(_dataTable.inArrayPtr, _dataTable.outArrayPtr);
        }

        public void RunReflectionScenario_Load()
        {
            typeof(Avx).GetMethod(nameof(Avx.ExtractVector128), new Type[] { typeof(Double*), typeof(Vector256<Double>), typeof(byte) })
                                     .Invoke(null, new object[] {
                                        Pointer.Box(_dataTable.outArrayPtr, typeof(Double*)),
                                        Avx.LoadVector256((Double*)(_dataTable.inArrayPtr)),
                                        (byte)1
                                     });

            ValidateResult(_dataTable.inArrayPtr, _dataTable.outArrayPtr);
        }

        public void RunReflectionScenario_LoadAligned()
        {
            typeof(Avx).GetMethod(nameof(Avx.ExtractVector128), new Type[] {  typeof(Double*), typeof(Vector256<Double>), typeof(byte) })
                                     .Invoke(null, new object[] {
                                        Pointer.Box(_dataTable.outArrayPtr, typeof(Double*)),
                                        Avx.LoadAlignedVector256((Double*)(_dataTable.inArrayPtr)),
                                        (byte)1
                                     });

            ValidateResult(_dataTable.inArrayPtr, _dataTable.outArrayPtr);
        }

        public void RunClsVarScenario()
        {
            Avx.ExtractVector128(
                (Double*)_dataTable.outArrayPtr,
                _clsVar,
                1
            );
        }

        public void RunLclVarScenario_UnsafeRead()
        {
            var firstOp = Unsafe.Read<Vector256<Double>>(_dataTable.inArrayPtr);
            Avx.ExtractVector128((Double*)_dataTable.outArrayPtr, firstOp, 1);
        }

        public void RunLclVarScenario_Load()
        {
            var firstOp = Avx.LoadVector256((Double*)(_dataTable.inArrayPtr));
            Avx.ExtractVector128((Double*)_dataTable.outArrayPtr, firstOp, 1);
        }

        public void RunLclVarScenario_LoadAligned()
        {
            var firstOp = Avx.LoadAlignedVector256((Double*)(_dataTable.inArrayPtr));
            Avx.ExtractVector128((Double*)_dataTable.outArrayPtr, firstOp, 1);
        }

        public void RunLclFldScenario()
        {
            var test = new SimpleUnaryOpTest__ExtractVector128Double1Store();
            Avx.ExtractVector128((Double*)_dataTable.outArrayPtr, test._fld, 1);
        }

        public void RunFldScenario()
        {
            Avx.ExtractVector128((Double*)_dataTable.outArrayPtr, _fld, 1);
        }

        public void RunUnsupportedScenario()
        {
            Succeeded = false;

            try
            {
                RunBasicScenario_UnsafeRead();
            }
            catch (PlatformNotSupportedException)
            {
                Succeeded = true;
            }
        }

        private void ValidateResult(Vector256<Double> firstOp, void* result, [CallerMemberName] string method = "")
        {
            Double[] inArray = new Double[Op1ElementCount];
            Double[] outArray = new Double[RetElementCount];

            Unsafe.Write(Unsafe.AsPointer(ref inArray[0]), firstOp);
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Double, byte>(ref outArray[0]), ref Unsafe.AsRef<byte>(result), VectorSize);

            ValidateResult(inArray, outArray, method);
        }

        private void ValidateResult(void* firstOp, void* result, [CallerMemberName] string method = "")
        {
            Double[] inArray = new Double[Op1ElementCount];
            Double[] outArray = new Double[RetElementCount];

            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Double, byte>(ref inArray[0]), ref Unsafe.AsRef<byte>(firstOp), VectorSize);
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Double, byte>(ref outArray[0]), ref Unsafe.AsRef<byte>(result), VectorSize);

            ValidateResult(inArray, outArray, method);
        }

        private void ValidateResult(Double[] firstOp, Double[] result, [CallerMemberName] string method = "")
        {
            if (BitConverter.DoubleToInt64Bits(result[0]) != BitConverter.DoubleToInt64Bits(firstOp[2]))
            {
                Succeeded = false;
            }
            else
            {
                for (var i = 1; i < RetElementCount; i++)
                {
                    if ((BitConverter.DoubleToInt64Bits(result[i]) != BitConverter.DoubleToInt64Bits(firstOp[i + 2])))
                    {
                        Succeeded = false;
                        break;
                    }
                }
            }

            if (!Succeeded)
            {
                Console.WriteLine($"{nameof(Avx)}.{nameof(Avx.ExtractVector128)}<Double>(Vector256<Double><9>): {method} failed:");
                Console.WriteLine($"  firstOp: ({string.Join(", ", firstOp)})");
                Console.WriteLine($"   result: ({string.Join(", ", result)})");
                Console.WriteLine();
            }
        }
    }
}
