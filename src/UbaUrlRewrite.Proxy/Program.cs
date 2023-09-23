// <copyright file="Program.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Proxy
{
	using Serilog;
	using Serilog.Events;
	using Serilog.Extensions.Logging;
	using Serilog.Sinks.SystemConsole.Themes;

	public class Program
	{
		public static async Task Main(string[] args)
		{
			ILogger<Program> logger = new SerilogLoggerFactory(new LoggerConfiguration().MinimumLevel.Information()
				.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
				.WriteTo.Console(theme: AnsiConsoleTheme.Literate)
				.CreateLogger()).CreateLogger<Program>();

			logger.LogInformation("Starting up");

			try
			{
				WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
				WebApplication app = builder.ConfigureServices().ConfigurePipeline();
				await app.RunAsync();
			}
			catch (Exception e) when (e is not HostAbortedException)
			{
				logger.LogCritical(e, "Unhandled exception");
			}
			finally
			{
				logger.LogInformation("Shut down complete");
			}
		}
	}
}