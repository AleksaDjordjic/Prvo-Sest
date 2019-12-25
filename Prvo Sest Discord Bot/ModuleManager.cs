﻿using BotAudioModule;
using BotColorReactModule;
using BotHallMonitorModule;
using BotServerManagmentModule;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace DiscordBot
{
    public static class ModuleManager
    {
        public static AudioModule audioModule;
        public static ServerManagmentModule managmentModule;
        public static ColorReactModule colorReactModule;
        public static HallMonitorModule hallMonitorModule;

        public static void SetupModules(DiscordSocketClient socketClient, IServiceCollection serviceCollection, ref IServiceProvider serviceProvider)
        {
            managmentModule = new ServerManagmentModule(socketClient, 659161580013092875, Static.Color, Static.Prefix, "1/6", 
                $"`{Static.Prefix}advance-hall-monitor` - Prebacuje <#659343822127497216> na sledecu nedelju");
            colorReactModule = new ColorReactModule(socketClient, Static.Color, Static.Prefix);
            hallMonitorModule = new HallMonitorModule(socketClient, Static.Color, Static.Prefix);

            audioModule = new AudioModule(socketClient, serviceCollection, Static.Color, Static.Prefix);
            serviceProvider = serviceCollection.BuildServiceProvider();
            audioModule.FinalInit(serviceProvider);
        }

        public static async Task RegisterModuleCommands(CommandService commandService, IServiceProvider serviceProvider)
        {
            await commandService.AddModulesAsync(Assembly.GetEntryAssembly(), serviceProvider);
            await commandService.AddModulesAsync(typeof(AudioModule).Assembly, serviceProvider);
            await commandService.AddModulesAsync(typeof(ServerManagmentModule).Assembly, serviceProvider);
            await commandService.AddModulesAsync(typeof(ColorReactModule).Assembly, serviceProvider);
            await commandService.AddModulesAsync(typeof(HallMonitorModule).Assembly, serviceProvider);
        }
    }
}