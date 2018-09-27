using System;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

namespace TypeGeneration
{
    public static class TypeGenerator
    {
        public static Type[] Generate(int count)
        {
            var types = new Type[count];

            for (int i = 0; i < count; i++)
            {
                types[i] = GenerateType(i);
            }

            return types;
        }

        private static TypeBuilder CreateTypeBuilder(int index)
        {
            var assemblyName = new AssemblyName("TestTypeGen");
            var domain = System.Threading.Thread.GetDomain();
            //var domain = AppDomain.CurrentDomain;
            var assemblyBuilder = domain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);
            var moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName.Name);
            var typeBuilder = moduleBuilder.DefineType("Type" + index, TypeAttributes.Class | TypeAttributes.Public, typeof(MonoBehaviour));

            return typeBuilder;
        }

        private static Type GenerateType(int index)
        {
            var typeBuilder = CreateTypeBuilder(index);

            CreateMembers(typeBuilder, "Property" + index, typeof(int));

            var type = typeBuilder.CreateType();

            //Debug.Log(type.Name);

            return type;
        }

        private static void CreateMembers(TypeBuilder typeBuilder, string propertyName, Type propertyType)
        {
            MethodInfo methodInfo = typeof(Dummy).GetMethod("Update", BindingFlags.Instance | BindingFlags.NonPublic);
            MethodBody methodBody = methodInfo.GetMethodBody();
            //Debug.LogFormat("Method: {0}", methodBody.MaxStackSize);
            var bytes = methodBody.GetILAsByteArray();

            MethodBuilder methodBuilder = typeBuilder.DefineMethod("Update", MethodAttributes.Private);
            methodBuilder.CreateMethodBody(bytes, bytes.Length);

            //ILGenerator methIL = methodBuilder.GetILGenerator();
            //// To retrieve the private instance field, load the instance it
            //// belongs to (argument zero). After loading the field, load the 
            //// argument one and then multiply. Return from the method with 
            //// the return value (the product of the two numbers) on the 
            //// execution stack.
            //methIL.Emit(OpCodes.Ldarg_0);
            //methIL.Emit(OpCodes.Nop);
            //methIL.Emit(OpCodes.Nop);
            //methIL.Emit(OpCodes.Ret);
        }
    }
}
