using System;
using System.Reflection;
using System.Runtime.InteropServices;

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
[Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E4")]
interface Work
{
    void MyMethod();
}

// Guid for the coclass MyTestClass.
[Guid("936DA01F-9ABD-4d9d-80C7-02AF85C822A8")]
public class MyTestClass : Work
{
    public void MyMethod() { }

    public static void Main(string[] args)
    {
        GuidAttribute IMyInterfaceAttribute = (GuidAttribute)Attribute.GetCustomAttribute(typeof(Work), typeof(GuidAttribute));

        System.Console.WriteLine("IMyInterface Attribute: " + IMyInterfaceAttribute.Value);

        // Use the string to create a guid.
        Guid myGuid1 = new Guid(IMyInterfaceAttribute.Value);
        // Use a byte array to create a guid.
        Guid myGuid2 = new Guid(myGuid1.ToByteArray());

        if (myGuid1.Equals(myGuid2))
            System.Console.WriteLine("myGuid1 equals myGuid2");
        else
            System.Console.WriteLine("myGuid1 does not equal myGuid2");

        // Equality operator can also be used to determine if two guids have same value.
        if (myGuid1 == myGuid2)
            System.Console.WriteLine("myGuid1 == myGuid2");
        else
            System.Console.WriteLine("myGuid1 != myGuid2");
    }
}