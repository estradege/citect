# Citect
Toolkit over Citect SCADA software.
Install this package from [NuGet](https://www.nuget.org/packages/Citect.CtApi/)
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

### 1) Execute ciCode function
```c#
using (var ctApi = new CtApi())
{
     ctApi.Open();
     var result = ctApi.Cicode("PageDisplay(Alarm)");
}
```
