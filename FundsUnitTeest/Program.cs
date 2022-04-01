//using BusinessLogic;
//using dotnet_core_xunit_test.Mock.Entities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;
//using Repository;

//public class Program
//{
//    public static void Main(string[] args)
//    {
//        var builder = new ConfigurationBuilder()

//          .AddJsonFile("appsettings.json")
//.Build();
//        var serviceProvider = new ServiceCollection()
//            .AddLogging()
//            .AddDbContext<TestDbContextMock>(ServiceLifetime.Singleton)
//            .AddSingleton<DbContext, TestDbContextMock>()
//        .AddSingleton<IRepository, PersistentRepository>()
//        .AddSingleton<IConfiguration>(builder)
//        .AddSingleton<ICustomersManager, CustomersManager>()
//        .AddSingleton<IAccountsManager, AccountsManager>()
//            .BuildServiceProvider();

//    }
//}