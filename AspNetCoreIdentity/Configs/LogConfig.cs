using KissLog.AspNetCore;
using KissLog.CloudListeners.Auth;
using KissLog.CloudListeners.RequestLogsListener;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.Text;

namespace AspNetCoreIdentity.Configs
{
    public static class LogConfig
    {
        public static void ConfigureKissLog(IOptionsBuilder options, IConfiguration configuration)
        {
            // optional KissLog configuration
            options.Options
                .AppendExceptionDetails((Exception ex) =>
                {
                    StringBuilder sb = new StringBuilder();

                    if (ex is NullReferenceException nullRefException)
                    {
                        sb.AppendLine("Important: check for null references");
                    }

                    return sb.ToString();
                });

            // KissLog internal logs
            options.InternalLog = (message) =>
            {
                Debug.WriteLine(message);
            };

            // register logs output
            RegisterKissLogListeners(options, configuration);
        }

        private static void RegisterKissLogListeners(IOptionsBuilder options, IConfiguration configuration)
        {
            // multiple listeners can be registered using options.Listeners.Add() method

            // register KissLog.net cloud listener
            options.Listeners.Add(new RequestLogsApiListener(new Application(
                configuration["KissLog.OrganizationId"],    //  "80c1aaa7-f29a-4f61-870d-192e97a6380a"
                configuration["KissLog.ApplicationId"])     //  "8fa7dda6-d97b-48af-9bd8-988857ea0b8c"
            )
            {
                ApiUrl = configuration["KissLog.ApiUrl"]    //  "https://api.kisslog.net"
            });
        }
    }
}
