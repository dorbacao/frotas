﻿using System;
using Nancy.Hosting.Self;

namespace Kereta.Web.Api
{
    class Program
    {
        static void Main(string[] args)
        {
            var uri =
                new Uri("http://localhost:9001");

            using (var host = new NancyHost(uri))
            {
                Effort.Provider.EffortProviderConfiguration.RegisterProvider();
                
                host.Start();

                Console.WriteLine("Your application is running on " + uri);
                Console.WriteLine("Press any [Enter] to close the host.");
                Console.ReadLine();
            }
        }
    }
}
