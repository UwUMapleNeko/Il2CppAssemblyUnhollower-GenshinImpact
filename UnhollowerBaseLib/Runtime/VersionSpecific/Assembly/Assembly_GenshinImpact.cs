﻿using System;
using System.Runtime.InteropServices;

namespace UnhollowerBaseLib.Runtime.VersionSpecific.Assembly {
    [ApplicableToUnityVersionsSince("2017.4.30")]
    public unsafe class NativeAssemblyStructHandler_GenshinImpact : INativeAssemblyStructHandler {
        public INativeAssemblyStruct CreateNewAssemblyStruct() {
            var pointer = Marshal.AllocHGlobal(Marshal.SizeOf<Il2CppAssembly_GenshinImpact>());

            *(Il2CppAssembly_GenshinImpact*)pointer = default;

            return new NativeAssemblyStruct(pointer);
        }

        public INativeAssemblyStruct Wrap(Il2CppAssembly* assemblyPointer) {
            if ((IntPtr)assemblyPointer == IntPtr.Zero) return null;
            else return new NativeAssemblyStruct((IntPtr)assemblyPointer);
        }

        public IntPtr il2cpp_assembly_get_name(IntPtr assembly) => ((Il2CppAssembly_GenshinImpact*)assembly)->aname.name;

#if DEBUG
        public string GetName() => "NativeAssemblyStructHandler_GenshinImpact";
#endif

        [StructLayout(LayoutKind.Sequential)]
        internal struct Il2CppAssembly_GenshinImpact {
            public Il2CppImage* image;
            public uint token;
            public int referencedAssemblyStart;
            public int referencedAssemblyCount;
            public Il2CppAssemblyName_GenshinImpact aname;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct Il2CppAssemblyName_GenshinImpact {
            public IntPtr name; // const char* 
            public IntPtr culture; // const char*
            public IntPtr hash_value; // const char*
            public IntPtr public_key; // const char*
            public uint hash_alg;
            public int hash_len;
            public uint flags;
            public int major;
            public int minor;
            public int build;
            public int revision;
            public long public_key_token; // PUBLIC_KEY_BYTE_LENGTH
        }

        internal class NativeAssemblyStruct : INativeAssemblyStruct {
            public NativeAssemblyStruct(IntPtr pointer) {
                Pointer = pointer;
            }

            public IntPtr Pointer { get; }

            public Il2CppAssembly* AssemblyPointer => (Il2CppAssembly*)Pointer;

            private Il2CppAssembly_GenshinImpact* NativeAssembly => (Il2CppAssembly_GenshinImpact*)Pointer;

            public ref Il2CppImage* Image => ref NativeAssembly->image;

            public ref IntPtr Name => ref NativeAssembly->aname.name;

            public ref int Major => ref NativeAssembly->aname.major;

            public ref int Minor => ref NativeAssembly->aname.minor;

            public ref int Build => ref NativeAssembly->aname.build;

            public ref int Revision => ref NativeAssembly->aname.revision;
        }
    }
}