# Metime
Helper library for timezone calculations. Special thanks to the [**AspectInjector**](https://github.com/pamidur/aspect-injector)
# Motivation
Well.. Enterprise product's timezones suck.
# Installation
**Metime** is activated in DEBUG mode only. Using it should not affect performance in production environment. 

Available on [NuGet](https://www.nuget.org/packages/Metime)

	PM> Install-Package Metime

`Startup.cs`
```csharp
using StubberProject.Extensions;

public class Startup
{
    public void ConfigureServices(IServiceCollection services) 
    {
        ...
        services.AddMetime<DefaultOffsetResolver>();
        ...
    }
    
    public void Configure(IApplicationBuilder app) 
    {
        ...
        app.UseMetime();
        ...
    }
}
```
# Usage
Detailed example can be found in example project. (WIP)

Services with timezone data, needs to be processed can be marked with the `ConvertTimezone`.
```csharp
using Metime.Attributes;

[ConvertTimezone]
public class BlogService : IBlogService 
{
    ...
}
```
Objects that needs to be processed needs to implement `ITimezoneConvertible`. Since dates are stored in UTC format, it's default value should be UTC (explicitly better).
```csharp
using Metime.Models;

public class Blog : ITimezoneConvertible
    {
        ...
        public DateTime PostedAt { get; set; }
        [NotMapped] public TimezoneFormat Kind { get; set; } = TimezoneFormat.UTC;
    }
```
# License
MIT License

Copyright (c) 2021 Metin ÖZTÜRK

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.