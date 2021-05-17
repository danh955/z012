# Architecture

### Projects

- MT.BlazorUi
- MT.BlazorUi.Application
- Hilres.Y2kDate
- Hilres.StockDb.Repository
- Hilres.StockDb.Loader.Yahoo
- Test.<i>project</i>

Lets see if I can do more vertical slices this time.  More independent projects.

- _Domain_ — Each domain entity
- _Application_ — Aggregate root corresponding to each use case
- _Persistence_ — Each database table
- _Infrastructure_ — Each functional area of the operating system (file operations, etc.) and/or external resources
- _Presentation_ — Aggregate root corresponding to each screen or web page
- _Cross-cutting_ (Common) — Each cross-cutting concern (Logging, Security, etc.)
- From [A Template for Clean Domain-Driven Design Architecture](https://medium.com/software-alchemy/a-template-for-clean-domain-driven-design-architecture-e386ad235f32)
 
### Using

- [Visual Studio v16.9](https://visualstudio.microsoft.com/vs/preview)
- [.NET 5.0](https://dotnet.microsoft.com/download/dotnet/5.0)

### NuGet

- <s>[CsvHelper](https://www.nuget.org/packages/CsvHelper)</s>
- [MediatR.Extensions.Microsoft.DependencyInjection](https://www.nuget.org/packages/MediatR.Extensions.Microsoft.DependencyInjection)
- [Microsoft.EntityFrameworkCore](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore)
- [Microsoft.EntityFrameworkCore.Sqlite](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Sqlite)
- [Microsoft.Extensions.Http](https://www.nuget.org/packages/Microsoft.Extensions.Http)

### Roslyn Analyzers

- [StyleCop.Analyzers](https://www.nuget.org/packages/StyleCop.Analyzers)
- <s>[Microsoft.CodeAnalysis.NetAnalyzers](https://www.nuget.org/packages/Microsoft.CodeAnalysis.NetAnalyzers)</s>
  - Right click Project file > Properties > Code Analysis > "Enable .NET Analyzers" = checked.  (default is checked for C# 9.0)
- Hmm, maybe [xunit.analyzers](https://www.nuget.org/packages/xunit.analyzers)

### Unit testing

- [xUnit](https://www.nuget.org/packages/xunit)
- [xUnit.runner.visualstudio](https://www.nuget.org/packages/xunit.runner.visualstudio)
- [Moq](https://www.nuget.org/packages/Moq)
- [Microsoft.NET.Test.Sdk](Microsoft.NET.Test.Sdk) - needed for Visual Studio IDE and xUnit

### Visual Studio Settings

- Tools > Options > Text Editor > C# > Advanced > Place 'System' directives first when sorting using = Checked.

### Visual Studio Extensions

- [CodeMaid](https://marketplace.visualstudio.com/items?itemName=SteveCadwallader.CodeMaid)
- [Visual Studio Spell Checker (VS2017 and Later)](https://marketplace.visualstudio.com/items?itemName=EWoodruff.VisualStudioSpellCheckerVS2017andLater)

### CSS Framework

- [Chota](https://github.com/jenil/chota)

### NasdaqTrader refinance:
- [Symbol Lookup](http://www.nasdaqtrader.com/Trader.aspx?id=symbollookup)
- [Symbol Look-Up/Directory Data Fields & Definitions](http://www.nasdaqtrader.com/trader.aspx?id=symboldirdefs)

#### URL to download the symbols:
- http://www.nasdaqtrader.com/dynamic/SymDir/nasdaqlisted.txt
- http://www.nasdaqtrader.com/dynamic/SymDir/otherlisted.txt
