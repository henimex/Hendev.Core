using Core.CrossCuttingConcerns.Serilog.ConfigurationModels;
using Core.CrossCuttingConcerns.Serilog.Messages;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;

namespace Core.CrossCuttingConcerns.Serilog.Loggers;

public class MsSqlLogger : LoggerServiceBase
{
    public MsSqlLogger(IConfiguration configuration)
    {
        MsSqlConfiguration logConfig =
            configuration.GetSection("Loggers:Serilog:MsSqlLogging:Config").Get<MsSqlConfiguration>() ??
            throw new Exception(SerilogMessages.NullOptionsMessage);

        MSSqlServerSinkOptions sinkOptions = new()
        {
            TableName = logConfig.TableName,
            AutoCreateSqlTable = logConfig.AutoCreateSqlTable,
            AutoCreateSqlDatabase = logConfig.AutoCreateSqlTable,
        };

        ColumnOptions columnOptions = new();

        Logger seriLogConfig = new LoggerConfiguration().WriteTo
            .MSSqlServer(
                connectionString: logConfig.ConnectionString,
                sinkOptions: sinkOptions,
                columnOptions: columnOptions).CreateLogger();

        Logger = seriLogConfig;
    }
}