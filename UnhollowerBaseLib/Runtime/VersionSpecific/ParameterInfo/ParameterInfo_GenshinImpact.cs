using System;
using System.Runtime.InteropServices;

namespace UnhollowerBaseLib.Runtime.VersionSpecific.ParameterInfo {
    [ApplicableToUnityVersionsSince("2017.4.30")]
    internal class NativeParameterInfoStructHandler_GenshinImpact : INativeParameterInfoStructHandler {
        public unsafe Il2CppParameterInfo*[] CreateNewParameterInfoArray(int paramCount) {
            var ptr = (Il2CppParameterInfo_GenshinImpact*)Marshal.AllocHGlobal(Marshal.SizeOf<Il2CppParameterInfo_GenshinImpact>() * paramCount);
            var res = new Il2CppParameterInfo*[paramCount];
            for (var i = 0; i < paramCount; i++) {
                ptr[i] = default;
                res[i] = (Il2CppParameterInfo*)&ptr[i];
            }
            return res;
        }

        public unsafe INativeParameterInfoStruct Wrap(Il2CppParameterInfo* paramInfoPointer) {
            if ((IntPtr)paramInfoPointer == IntPtr.Zero) return null;
            else return new NativeParameterInfoStructWrapper((IntPtr)paramInfoPointer);
        }

        public unsafe INativeParameterInfoStruct Wrap(Il2CppParameterInfo* paramInfoListBegin, int index) {
            if ((IntPtr)paramInfoListBegin == IntPtr.Zero) return null;
            else return new NativeParameterInfoStructWrapper((IntPtr)paramInfoListBegin + (Marshal.SizeOf<Il2CppParameterInfo_GenshinImpact>() * index));
        }

        public bool HasNamePosToken => true;

#if DEBUG
        public string GetName() => "NativeParameterInfoStructHandler_GenshinImpact";
#endif

        [StructLayout(LayoutKind.Sequential)]
        internal unsafe struct Il2CppParameterInfo_GenshinImpact {
            public IntPtr name; // const char*
            public int position;
            public uint token;
            public Il2CppTypeStruct* parameter_type; // const
        }

        internal unsafe class NativeParameterInfoStructWrapper : INativeParameterInfoStruct {
            public NativeParameterInfoStructWrapper(IntPtr pointer) {
                Pointer = pointer;
            }

            public IntPtr Pointer { get; }

            public Il2CppParameterInfo* ParameterInfoPointer => (Il2CppParameterInfo*)Pointer;

            public bool HasNamePosToken => true;

            private Il2CppParameterInfo_GenshinImpact* NativeParameter => (Il2CppParameterInfo_GenshinImpact*)Pointer;

            public ref IntPtr Name => ref NativeParameter->name;

            public ref int Position => ref NativeParameter->position;

            public ref uint Token => ref NativeParameter->token;

            public ref Il2CppTypeStruct* ParameterType => ref NativeParameter->parameter_type;
        }
    }
}