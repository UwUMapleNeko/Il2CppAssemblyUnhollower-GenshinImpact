﻿using System;
using System.Runtime.InteropServices;

namespace UnhollowerBaseLib.Runtime.VersionSpecific.MethodInfo {
    [ApplicableToUnityVersionsSince("2017.4.30")]
    public unsafe class NativeMethodInfoStructHandler_GenshinImpact : INativeMethodInfoStructHandler {
        public INativeMethodInfoStruct CreateNewMethodStruct() {
            var pointer = Marshal.AllocHGlobal(Marshal.SizeOf<Il2CppMethodInfo_GenshinImpact>());
            *(Il2CppMethodInfo_GenshinImpact*)pointer = default;

            return new NativeMethodInfoStructWrapper(pointer);
        }

        public INativeMethodInfoStruct Wrap(Il2CppMethodInfo* methodPointer) {
            if ((IntPtr)methodPointer == IntPtr.Zero) return null;
            else return new NativeMethodInfoStructWrapper((IntPtr)methodPointer);
        }

        [DllImport("UserAssembly", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern IntPtr il2cpp_method_get_from_reflection(IntPtr method);

        public IntPtr GetMethodFromReflection(IntPtr method) {
            return IL2CPP.GetIl2CppMethodFromReflection(method);
        }

        public IntPtr CopyMethodInfoStruct(IntPtr origMethodInfo) {
            int sizeOfMethodInfo = Marshal.SizeOf<Il2CppMethodInfo_GenshinImpact>();
            IntPtr copiedMethodInfo = Marshal.AllocHGlobal(sizeOfMethodInfo);

            object temp = Marshal.PtrToStructure<Il2CppMethodInfo_GenshinImpact>(origMethodInfo);
            Marshal.StructureToPtr(temp, copiedMethodInfo, false);

            return copiedMethodInfo;
        }

        public IntPtr il2cpp_method_get_class(IntPtr method) => (IntPtr)((Il2CppMethodInfo_GenshinImpact*)method)->klass;
        public IntPtr il2cpp_method_get_name(IntPtr method) => ((Il2CppMethodInfo_GenshinImpact*)method)->name;
        public uint il2cpp_method_get_param_count(IntPtr method) => ((Il2CppMethodInfo_GenshinImpact*)method)->parameters_count;
        public IntPtr il2cpp_method_get_return_type(IntPtr method) => (IntPtr)((Il2CppMethodInfo_GenshinImpact*)method)->return_type;
        public uint il2cpp_method_get_token(IntPtr method) => ((Il2CppMethodInfo_GenshinImpact*)method)->token;

#if DEBUG
        public string GetName() => "NativeMethodInfoStructHandler_GenshinImpact";
#endif

        [StructLayout(LayoutKind.Sequential)]
        internal struct Il2CppMethodInfo_GenshinImpact {
            public IntPtr methodPointer;
            public IntPtr invoker_method;
            public IntPtr name; // const char*
            public Il2CppClass* klass;
            public Il2CppTypeStruct* return_type;
            public Il2CppParameterInfo* parameters;

            public IntPtr someRtData;
            /*union
            {
                const Il2CppRGCTXData* rgctx_data; /* is_inflated is true and is_generic is false, i.e. a generic instance method #1#
                const Il2CppMethodDefinition* methodDefinition;
            };*/

            public IntPtr someGenericData;
            /*/* note, when is_generic == true and is_inflated == true the method represents an uninflated generic method on an inflated type. #1#
            union
            {
                const Il2CppGenericMethod* genericMethod; /* is_inflated is true #1#
                const Il2CppGenericContainer* genericContainer; /* is_inflated is false and is_generic is true #1#
            };*/

            public int customAttributeIndex;
            public uint token;
            public Il2CppMethodFlags flags;
            public Il2CppMethodImplFlags iflags;
            public ushort slot;
            public byte parameters_count;

            public MethodInfoExtraFlags extra_flags;
            /*uint8_t is_generic : 1; /* true if method is a generic method definition #1#
            uint8_t is_inflated : 1; /* true if declaring_type is a generic instance or if method is a generic instance#1#
            uint8_t wrapper_type : 1; /* always zero (MONO_WRAPPER_NONE) needed for the debugger #1#
            uint8_t is_marshaled_from_native : 1*/
        }


        internal class NativeMethodInfoStructWrapper : INativeMethodInfoStruct {
            public NativeMethodInfoStructWrapper(IntPtr pointer) {
                Pointer = pointer;
            }

            public int StructSize => Marshal.SizeOf<Il2CppMethodInfo_GenshinImpact>();

            public IntPtr Pointer { get; }

            public Il2CppMethodInfo* MethodInfoPointer => (Il2CppMethodInfo*)Pointer;

            private Il2CppMethodInfo_GenshinImpact* NativeMethod => (Il2CppMethodInfo_GenshinImpact*)Pointer;

            public ref IntPtr Name => ref NativeMethod->name;

            public ref ushort Slot => ref NativeMethod->slot;

            public ref IntPtr MethodPointer => ref NativeMethod->methodPointer;

            public ref Il2CppClass* Class => ref NativeMethod->klass;

            public ref IntPtr InvokerMethod => ref NativeMethod->invoker_method;

            public ref Il2CppTypeStruct* ReturnType => ref NativeMethod->return_type;

            public ref Il2CppMethodFlags Flags => ref NativeMethod->flags;

            public ref byte ParametersCount => ref NativeMethod->parameters_count;

            public ref Il2CppParameterInfo* Parameters => ref NativeMethod->parameters;

            public ref MethodInfoExtraFlags ExtraFlags => ref NativeMethod->extra_flags;
        }
    }
}