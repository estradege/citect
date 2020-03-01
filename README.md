# Citect
Toolkit over Citect SCADA software.
Install this package from [NuGet](https://www.nuget.org/packages/Citect.CtApi/).
```
dotnet add package Citect.CtApi
```

## Samples
### 1) Read and Write tags
```c#
using (var ctApi = new CtApi())
{
     ctApi.Open();
     ctApi.TagWrite("MyTagName", "MyTagValue");
     var myTag = ctApi.TagRead("MyTagName");
}
```

### 2) Execute CiCode function
```c#
using (var ctApi = new CtApi())
{
     ctApi.Open();
     var result = ctApi.Cicode("PageDisplay(Alarm)");
}
```

### 3) Get database objects
```c#
using (var ctApi = new CtApi())
{
     ctApi.Open();
     var alarms = ctApi.Find("Alarm", "TAG=BP12*", "", "TAG", "NAME", "DESC");
     foreach (var alarm in alarms)
     {
          Console.WriteLine($"TAG={alarm["TAG"]}, NAME={alarm["NAME"]}, DESC={alarm["DESC"]}");
     }
}
```
