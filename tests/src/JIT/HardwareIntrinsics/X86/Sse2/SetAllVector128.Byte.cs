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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace JIT.HardwareIntrinsics.X86
{
    public static partial class Program
    {
        private static void SetAllVector128Byte()
        {
            bool skipIf32Bit = typeof(Byte) == typeof(Int64) ? true :
                                     typeof(Byte) == typeof(UInt64) ? true : false;

            if (skipIf32Bit && !Environment.Is64BitProcess)
            {
                return;
            }

            var test = new SimpleScalarUnaryOpTest__SetAllVector128Byte();

            if (test.IsSupported)
            {
                // Validates basic functionality works, using Unsafe.Read
                test.RunBasicScenario_UnsafeRead();

                // Validates calling via reflection works, using Unsafe.Read
                test.RunReflectionScenario_UnsafeRead();

                if (Sse2.IsSupported)
                {
                    // Validates calling via reflection works, using Load
                    test.RunReflectionScenario();
                }

                // Validates passing a static member works
                test.RunClsVarScenario();

                // Validates passing a local works, using Unsafe.Read
                test.RunLclVarScenario_UnsafeRead();

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

    public sealed unsafe class SimpleScalarUnaryOpTest__SetAllVector128Byte
    {
        private const int VectorSize = 16;

        private const int Op1ElementCount = 2;
        private const int RetElementCount = VectorSize / sizeof(Byte);

        private static Byte[] _data = new Byte[Op1ElementCount];

        private static Byte _clsVar;

        private Byte _fld;

        private SimpleScalarUnaryOpTest__DataTable<Byte, Byte> _dataTable;

        static SimpleScalarUnaryOpTest__SetAllVector128Byte()
        {
            var random = new Random();

            for (int i = 0; i < Op1ElementCount; i++)
            {
                _data[i] = (byte)(random.Next(byte.MinValue, byte.MaxValue));
            }

            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Byte, byte>(ref _clsVar), ref Unsafe.As<Byte, byte>(ref _data[0]), (uint)Marshal.SizeOf<Byte>());
        }

        public SimpleScalarUnaryOpTest__SetAllVector128Byte()
        {
            Succeeded = true;

            var random = new Random();

            for (var i = 0; i < Op1ElementCount; i++)
            {
                _data[i] = (byte)(random.Next(byte.MinValue, byte.MaxValue));
            }

            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Byte, byte>(ref _fld), ref Unsafe.As<Byte, byte>(ref _data[0]), (uint)Marshal.SizeOf<Byte>());

            for (var i = 0; i < Op1ElementCount; i++)
            {
                _data[i] = (byte)(random.Next(byte.MinValue, byte.MaxValue));
            }

            _dataTable = new SimpleScalarUnaryOpTest__DataTable<Byte, Byte>(_data, new Byte[RetElementCount], VectorSize);
        }

        public bool IsSupported => Sse2.IsSupported;

        public bool Succeeded { get; set; }

        public void RunBasicScenario_UnsafeRead()
        {
            var result = Sse2.SetAllVector128(
                Unsafe.Read<Byte>(_dataTable.inArrayPtr)
            );

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_dataTable.inArrayPtr, _dataTable.outArrayPtr);
        }

        public void RunReflectionScenario_UnsafeRead()
        {
            var method = typeof(Sse2).GetMethod(nameof(Sse2.SetAllVector128), new Type[] { typeof(Byte) });
            var result = method.Invoke(null, new object[] { Unsafe.Read<Byte>(_dataTable.inArrayPtr)});

            Unsafe.Write(_dataTable.outArrayPtr, (Vector128<Byte>)(result));
            ValidateResult(_dataTable.inArrayPtr, _dataTable.outArrayPtr);
        }

        public void RunReflectionScenario()
        {
            var method = typeof(Sse2).GetMethod(nameof(Sse2.SetAllVector128), new Type[] { typeof(Byte) });
            Byte parameter = (Byte) _dataTable.inArray[0];
            var result = method.Invoke(null, new object[] { parameter });

            Unsafe.Write(_dataTable.outArrayPtr, (Vector128<Byte>)(result));
            ValidateResult(parameter, _dataTable.outArrayPtr);
        }

        public void RunClsVarScenario()
        {
            var result = Sse2.SetAllVector128(
                _clsVar
            );

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_clsVar, _dataTable.outArrayPtr);
        }

        public void RunLclVarScenario_UnsafeRead()
        {
            var firstOp = Unsafe.Read<Byte>(_dataTable.inArrayPtr);
            var result = Sse2.SetAllVector128(firstOp);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(firstOp, _dataTable.outArrayPtr);
        }

        public void RunLclFldScenario()
        {
            var test = new SimpleScalarUnaryOpTest__SetAllVector128Byte();
            var result = Sse2.SetAllVector128(test._fld);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(test._fld, _dataTable.outArrayPtr);
        }

        public void RunFldScenario()
        {
            var result = Sse2.SetAllVector128(_fld);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_fld, _dataTable.outArrayPtr);
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

        private void ValidateResult(Byte firstOp, void* result, [CallerMemberName] string method = "")
        {
            Byte[] inArray = new Byte[Op1ElementCount];
            Byte[] outArray = new Byte[RetElementCount];

            Unsafe.Write(Unsafe.AsPointer(ref inArray[0]), firstOp);
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Byte, byte>(ref outArray[0]), ref Unsafe.AsRef<byte>(result), VectorSize);

            ValidateResult(inArray, outArray, method);
        }

        private void ValidateResult(void* firstOp, void* result, [CallerMemberName] string method = "")
        {
            Byte[] inArray = new Byte[Op1ElementCount];
            Byte[] outArray = new Byte[RetElementCount];

            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Byte, byte>(ref inArray[0]), ref Unsafe.AsRef<byte>(firstOp), VectorSize);
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Byte, byte>(ref outArray[0]), ref Unsafe.AsRef<byte>(result), VectorSize);

            ValidateResult(inArray, outArray, method);
        }

        private void ValidateResult(Byte[] firstOp, Byte[] result, [CallerMemberName] string method = "")
        {
            if (result[0] != firstOp[0])
            {
                Succeeded = false;
            }
            else
            {
                for (var i = 1; i < RetElementCount; i++)
                {
                    if (result[i] != firstOp[0])
                    {
                        Succeeded = false;
                        break;
                    }
                }
            }

            if (!Succeeded)
            {
                Console.WriteLine($"{nameof(Sse2)}.{nameof(Sse2.SetAllVector128)}<Byte>(Vector128<Byte>): {method} failed:");
                Console.WriteLine($"  firstOp: ({string.Join(", ", firstOp)})");
                Console.WriteLine($"   result: ({string.Join(", ", result)})");
                Console.WriteLine();
            }
        }
    }
}
