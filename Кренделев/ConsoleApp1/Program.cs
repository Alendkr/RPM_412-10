using System;
using System.Reflection;

Type myType = typeof(Work);

Console.WriteLine("Методы:");
foreach (MethodInfo method in myType.GetMethods())
{
    string modificator = "";
    if (method.IsAbstract) modificator += "IsAbstract ";
    if (method.IsFamily) modificator += "IsFamily ";
    if (method.IsFamilyAndAssembly) modificator += "IsFamilyAndAssembly ";
    if (method.IsAssembly) modificator += "IsAssembly ";
    if (method.IsPrivate) modificator += "IsPrivate ";
    if (method.IsPublic) modificator += "IsPublic ";
    if (method.IsConstructor) modificator += "IsConstructor ";
    if (method.IsVirtual) modificator += "virtual ";
    if (method.IsStatic) modificator += "IsStatic ";

    Console.WriteLine($"{modificator}{method.ReturnType.Name} {method.Name} ()");
}

Console.WriteLine("Конструкторы:");
foreach (ConstructorInfo ctor in myType.GetConstructors(
    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
{
    string modificator = "";

    // получаем модификатор доступа Для каждого конструктора вывести значения для следующих свойств: IsFamily, IsFamilyAndAssembly, IsFamilyOrAssembly, IsAssembly, IsPrivate, IsPublic.

    if (ctor.IsPublic)
        modificator += "public";
    else if (ctor.IsPrivate)
        modificator += "private";
    else if (ctor.IsAssembly)
        modificator += "internal";
    else if (ctor.IsFamily)
        modificator += "protected";
    else if (ctor.IsFamilyAndAssembly)
        modificator += "private protected";
    else if (ctor.IsFamilyOrAssembly)
        modificator += "protected internal";

    Console.Write($"{modificator} {myType.Name}(");
    // получаем параметры конструктора
    ParameterInfo[] parameters = ctor.GetParameters();
    for (int i = 0; i < parameters.Length; i++)
    {
        var param = parameters[i];
        Console.Write($"{param.ParameterType.Name} {param.Name}");
        if (i < parameters.Length - 1) Console.Write(", ");
    }
    Console.WriteLine(")");
}

Console.WriteLine("Поля:");
foreach (FieldInfo field in myType.GetFields(
    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static))
{
    string modificator = "";

    // получаем модификатор доступа
    if (field.IsPublic)
        modificator += "public ";
    else if (field.IsPrivate)
        modificator += "private ";
    else if (field.IsAssembly)
        modificator += "internal ";
    else if (field.IsFamily)
        modificator += "protected ";
    else if (field.IsFamilyAndAssembly)
        modificator += "private protected ";
    else if (field.IsFamilyOrAssembly)
        modificator += "protected internal ";

    // если поле статическое
    if (field.IsStatic) modificator += "static ";

    Console.WriteLine($"{modificator}{field.FieldType.Name} {field.Name}");
}
Console.WriteLine("Свойства:");
foreach (PropertyInfo prop in myType.GetProperties(
    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static))
{
    Console.Write($"{prop.PropertyType} {prop.Name} {{");

    // если свойство доступно для чтения
    if (prop.CanRead) Console.Write("get;");
    // если свойство доступно для записи
    if (prop.CanWrite) Console.Write("set;");
    Console.WriteLine("}");
}
class Work
{
    static void Main()

    {
        // Define some integers for a division operation.
        int[] values = { 10, 7 };
        foreach (var value in values)
        {
            try
            {
                Console.WriteLine("{0} divided by 2 is {1}", value, DivideByTwo(value));
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("{0}: {1}", e.GetType().Name, e.Message);
            }
            Console.WriteLine();
        }
    }

    static int DivideByTwo(int num)
    {
        // If num is an odd number, throw an ArgumentException.
        if ((num & 1) == 1)
            throw new ArgumentException(String.Format("{0} is not an even number", num),
                                      "num");

        // num is even, return half of its value.
        return num / 2;
    }
}