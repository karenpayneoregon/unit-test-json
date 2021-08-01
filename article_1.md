# About 

Working with Json using [System.Text.Json](https://docs.microsoft.com/en-us/dotnet/api/system.text.json?view=net-5.0) 

If you have been working with Json data for years most likely the choice library was NewtonSoft.Json installed from [NuGet package](https://www.nuget.org/packages/Newtonsoft.Json/).

Starting .NET 5.0, there is a new way to interact with Json from [System.Text.Json](https://docs.microsoft.com/en-us/dotnet/api/system.text.json?view=net-5.0).

The classes is not as mature as [Json.net](https://www.newtonsoft.com/json) although it’s still young and maturing and with that well worth trying out. Note the classes under System.Text.Json are typically faster than [Json.Net](https://www.newtonsoft.com/json) and the default serializer for ASP.NET Core.

# Learning to work with System.Text.Json

Typically there are three paths how a coder/developer approaches learning something new

1. Finding code samples on the web, try them out until one sample works, tries to adapt to their task and largely fails which then they go off and ask in a developer forum. If they get a solution nine times of ten they use the code without learning anything from it

1. Seek out documentation and code samples, integrate code into a project. If lucky all is good while when things don’t go as planned a) waste time tinkering with code b) ask in a forum like in the first path. Its 10/90 if they take time to learn where 90 is they don’t take time to learn.

1. Seek out documentation and code samples followed by either creating console or test projects along with mocked data to learn outside of a project. Once comfortable if the code is needed for more than one project some coders/programmers will create a class project which can be reused. There are different levels of reusability, hard code for specific classes or use [generics](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/types/generics).



</br>

# Projects overview


For this post path three will be used. High level there is a class project for serializing/deserializing and another class project for concrete classes for  serializing/deserializing.

In the class project below there are simple/generic methods which are easy to use from code samples in an included unit test project.

![img](assets/jsonClassProject.png)

Concrete classes for interating with Json data both locally and from the web.

![img](assets/containers.png)

A unit test project for those experienced with testing will be easy to work with while those who never written unit test before may be confusing to understand although we will step through each aspect of not just serializing and deserializing Json data but also step through the anatomy of code flow in this unit test project.

![imag](assets/unitTest.png)

## Unit test base classes

Unit test class MainTest is setup as a partial class so that in the root level there are only test methods while under the Base folder there are properties which are reused in more than one test method. Also, MainTest implements a class responsible for instantiating an instance of [TestContext class](https://docs.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.testtools.unittesting.testcontext?view=visualstudiosdk-2019) which provides access to various pieces of information like test name which in the MainTest we can perform operations prior to a test method executing or after a test method runs.


```csharp
[TestInitialize]
public void Initialization()
```

Code to test if a specific test method is going to execute the following code is generally used.

```csharp
if (TestContext.TestName == "ContactSerialize")
{
    if (File.Exists(ContactSerializeFileName))
    {
        File.Delete(ContactSerializeFileName);
    }
}
```

There is a flaw, if a programmer renames the test name then the above must also change. A better way is to use [nameof expression](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/nameof). 

Now if the test method name changes performing a refactor/rename of the test method will be done below/

```csharp
if (TestContext.TestName == nameof(ContactSerialize))
{
    if (File.Exists(ContactSerializeFileName))
    {
        File.Delete(ContactSerializeFileName);
    }
}
```

As code grows for `Initialization` method it can be easy to not rename a test name when done as a string. Below shows how things can get complex in MainTest.cs

```csharp
[TestInitialize]
public void Initialization()
{

    if (TestContext.TestName == nameof(ContactsDeserialize) || TestContext.TestName == nameof(ContactSerialize))
    {
        ReadFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "contacts.json");

        if (TestContext.TestName == nameof(ContactSerialize))
        {
            if (File.Exists(ContactSerializeFileName))
            {
                File.Delete(ContactSerializeFileName);
            }
        }

    }
    else if (TestContext.TestName == nameof(CustomersDeserialize) || TestContext.TestName == nameof(CustomerSerialize))
    {
        ReadFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "customers.json");

        if (File.Exists(CustomersSerializeFileName))
        {
            File.Delete(CustomersSerializeFileName);
        }
    }

}
```

There are several properties setup for use in test methods which point to json files for serializing an deserializing.

```csharp
/// <summary>
/// File to read for <see cref="Contact"/> test
/// </summary>
public string ContactSerializeFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ContactsSerialized.json");

/// <summary>
/// File to read for <see cref="Customer"/> test
/// </summary>
public string CustomersSerializeFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CustomersSerialized.json");

/// <summary>
/// File used for PowerShell operations
/// </summary>
public string ComputerDetailsFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "computerDateInfo.json");
public string WeatherFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Weather.json");
```


## Serializing/deserializing test methods

All code for the following is included in class projects [ContainerLibrary](https://github.com/karenpayneoregon/unit-test-json/tree/master/ContainerLibrary) and [Json.Library](https://github.com/karenpayneoregon/unit-test-json/tree/master/Json.Library)

### Basic deserializing 

Where json is read in from a [file](https://github.com/karenpayneoregon/unit-test-json/blob/master/JsonTestProject/contacts.json) which creates a List&lt;[Contact](https://github.com/karenpayneoregon/unit-test-json/blob/master/ContainerLibrary/Classes/Contact.cs)&gt;

```csharp
var contactsList = JsonSerializer.Deserialize<List<Contact>>(json);
```

The above can be a language extension which means no need to write code out like above.

```csharp
List<Contact> contacts = json.JSonToList<Contact>();
```

### Basic serializing

Common practice is to write code to serialize in line with code in a (when working with desktop or console projects) with code that read information from a grid control etc.

Doing serializing this way means

- Duplicate code for each place serializing is needed
- When copying to another project and code reqirements change creates additional work to fix each place said code is used.

A better way is to place the code into a class project where all methods are generic such as the one for SSSSS.


```csharp
/// <summary>
/// Save List&lt;T&gt; to file
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="sender">Type to save</param>
/// <param name="fileName">File to save too</param>
/// <param name="format">true to format json, false not to format json</param>
/// <returns>
/// name value tuple, success of operation and a exception on failure
/// </returns>
public static (bool result, Exception exception) JsonToFile<T>(this T sender, string fileName, bool format = true)
{

    try
    {
        /*
         * explore other options besides WriteIndented
         */
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        
        File.WriteAllText(fileName, JsonSerializer.Serialize(sender, format ? options : null));

        return (true, null);

    }
    catch (Exception exception)
    {
        return (false, exception);
    }

}
```

Used against [Contact](https://github.com/karenpayneoregon/unit-test-json/blob/master/ContainerLibrary/Classes/Contact.cs)

```csharp
[TestMethod]
[TestTraits(Trait.Serialize)]
public void ContactSerialize()
{
    List<Contact> contacts = File.ReadAllText(ReadFileName).JSonToList<Contact>();
    var (success, exception) = contacts.JsonToFile(ContactSerializeFileName);
    Assert.IsTrue(File.Exists(ContactSerializeFileName) && success);
}
```

Used against [Customer](https://github.com/karenpayneoregon/unit-test-json/blob/master/ContainerLibrary/Classes/Customer.cs)

```csharp
[TestMethod]
[TestTraits(Trait.Serialize)]
public void CustomerSerialize()
{
    var json = File.ReadAllText(ReadFileName);
    List<Customer> customers = json.JSonToList<Customer>();

    var singleCustomer = customers.FirstOrDefault(customer => customer.Identifier == 2);


    singleCustomer.CompanyName = singleCustomer.CompanyName.ToLower();
    Assert.IsTrue(singleCustomer.CompanyName == "ana trujillo emparedados y helados");
    var (success, exception) = customers.JsonToFile(CustomersSerializeFileName);
    Assert.IsTrue(File.Exists(CustomersSerializeFileName) && success);
}
```

By using generics, same as deserialize operations in a single class project code is easy to use and maintain.

This brings us back to unit testing the code. When changes are made in the class project there needs to be unit test for each project which uses the code above to ensure changes didn't break something.

# Caveats

The majority of code presented in unit test read and write to local Json files, for working with Json read from a web file see the following test methods which use a free [service](https://jsonplaceholder.typicode.com) with simple Json files.

 [JsonPlaceHolder_Get_User](https://github.com/karenpayneoregon/unit-test-json/blob/master/JsonTestProject/MainTest.cs#L126) and 
[JsonPlaceHolder_Get_Posts](https://github.com/karenpayneoregon/unit-test-json/blob/master/JsonTestProject/MainTest.cs#L151)


# Resources

- **Working with JSON**
  - JSON [serialization and deserialization](https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-overview?pivots=dotnet-5-0)
  - [How to serialize and deserialize](https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-how-to?pivots=dotnet-5-0) (marshal and unmarshal) JSON in .NET
  - [Migrate](https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-migrate-from-newtonsoft-how-to?pivots=dotnet-5-0) from Newtonsoft.Json to System.Text.Json
  - System.Text.Json [source generator](https://devblogs.microsoft.com/dotnet/try-the-new-system-text-json-source-generator/)
- **Testing**
  - [Testing in .NET](https://docs.microsoft.com/en-us/dotnet/core/testing/)
  - [Telerik JustMock](https://www.telerik.com/products/mocking.aspx) - mocking tool for crafting unit tests.
- **Data**
  - Fake json https://jsonplaceholder.typicode.com/


# Summary

In the article basics have been presented for serializing and deserializing to and from Json files. There is quite more to working with Json dependent on business requirement although with understanding basics will make more advance work easier.



