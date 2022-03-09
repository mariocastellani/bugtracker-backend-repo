using System.Reflection;

namespace SharedKernel.DependencyInjection
{
    internal static class TypeHelper
    {
        public static bool InterfaceTypeInheritFrom(Type interfaceType, object criteria)
        {
            // Filter that can be used to find interfaces that inherit from another

            if (interfaceType == null || !interfaceType.IsInterface || interfaceType.IsGenericType)
                return false;

            if (criteria is not Type baseInterfaceType)
                return false;

            return (baseInterfaceType.IsGenericType) 
                ? interfaceType.GetInterface(baseInterfaceType.Name) != null
                : baseInterfaceType.IsAssignableFrom(interfaceType);
        }

        public static Type GetMostConcreteInterfaceType(Type type, Type baseInterfaceType)
        {
            // Gets the interfaces of 'type', which inherit from 'baseInterfaceType'
            var interfaces = type
                .FindInterfaces(new TypeFilter(InterfaceTypeInheritFrom), baseInterfaceType);

            var childInterfaces = new HashSet<Type>(interfaces.SelectMany(x => x.GetInterfaces()));

            // Get and return the most concrete interface of the type
            return interfaces.Except(childInterfaces).FirstOrDefault();
        }
    }
}