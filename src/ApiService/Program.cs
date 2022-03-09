WebApplication
    .CreateBuilder(args)
    .ConfigureBuilder()
    .ConfigureServices()
    .Build()
    .ConfigurePipeline()
    .Run();