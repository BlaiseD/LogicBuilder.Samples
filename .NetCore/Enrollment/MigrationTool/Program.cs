﻿using Microsoft.EntityFrameworkCore;
using System;

namespace MigrationTool
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            using (MigrationContext context = new MigrationContext())
            {
                context.Database.Migrate();
                context.Database.EnsureCreated();
            }
         }
    }
}
