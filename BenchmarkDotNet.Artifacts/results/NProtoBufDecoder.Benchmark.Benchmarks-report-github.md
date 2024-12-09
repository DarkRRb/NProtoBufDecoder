```

BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5131/22H2/2022Update)
Intel Core i7-8750H CPU 2.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.403
  [Host]     : .NET 8.0.10 (8.0.1024.46610), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.10 (8.0.1024.46610), X64 RyuJIT AVX2


```
| Method    | Mean     | Error     | StdDev    | Gen0     | Gen1     | Gen2    | Allocated |
|---------- |---------:|----------:|----------:|---------:|---------:|--------:|----------:|
| Scenario1 | 1.232 ms | 0.0246 ms | 0.0502 ms | 207.0313 | 160.1563 | 64.4531 |   1.18 MB |
