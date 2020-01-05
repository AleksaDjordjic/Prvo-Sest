using Bindings;
using BotAudioModule;
using BotColorReactModule;
using BotFunModule;
using BotHallMonitorModule;
using BotMemeGeneratorModule;
using BotMiscellaneousModule;
using BotSchoolModule;
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
        public static FunModule funModule;
        public static AudioModule audioModule;
        public static SchoolModule schoolModule;
        public static ColorReactModule colorReactModule;
        public static HallMonitorModule hallMonitorModule;
        public static ServerManagmentModule managmentModule;
        public static MiscellaneousModule miscellaneousModule;
        public static MemeGeneratorModule memeGeneratorModule;

        public static void SetupModules(DiscordSocketClient socketClient, IServiceCollection serviceCollection, ref IServiceProvider serviceProvider)
        {
            managmentModule = new ServerManagmentModule(socketClient, 659161580013092875, Static.Color, Static.Prefix, "1/6",
                @"https://cdn.discordapp.com/attachments/659186662819233831/659191565985644555/1-6_Logo.png", 
                $"`{Static.Prefix}advance-hall-monitor` - Prebacuje <#659343822127497216> na sledecu nedelju\n" +
                $"`{Static.Prefix}add-test <date> <subject> <type> <comment>` - Dodaje Test sa datumom <data> (Format: DD/MM/YYYY h/m/s qq, Primer: 14/4/2020 5/30/0 PM), predmetom <subject> (ID Predmeta), tipom <type> (0 - {(TestType)0}, 1 - {(TestType)1}), i komentarom <comment> (nije obavezno) \n" +
                $"`{Static.Prefix}remove-test <id>` - Skloni test sa ID-em <id>");
            funModule = new FunModule(socketClient, Static.Color, Static.Prefix);
            schoolModule = new SchoolModule(socketClient, Static.Color, Static.Prefix);
            colorReactModule = new ColorReactModule(socketClient, Static.Color, Static.Prefix);
            hallMonitorModule = new HallMonitorModule(socketClient, Static.Color, Static.Prefix);
            miscellaneousModule = new MiscellaneousModule(socketClient, Static.Color, Static.Prefix);
            memeGeneratorModule = new MemeGeneratorModule(socketClient, Static.Color, Static.Prefix);

            audioModule = new AudioModule(socketClient, serviceCollection, Static.Color, Static.Prefix);
            serviceProvider = serviceCollection.BuildServiceProvider();
            audioModule.FinalInit(serviceProvider);
        }

        public static async Task RegisterModuleCommands(CommandService commandService, IServiceProvider serviceProvider)
        {
            await commandService.AddModulesAsync(Assembly.GetEntryAssembly(), serviceProvider);
            await commandService.AddModulesAsync(typeof(FunModule).Assembly, serviceProvider);
            await commandService.AddModulesAsync(typeof(AudioModule).Assembly, serviceProvider);
            await commandService.AddModulesAsync(typeof(SchoolModule).Assembly, serviceProvider);
            await commandService.AddModulesAsync(typeof(ColorReactModule).Assembly, serviceProvider);
            await commandService.AddModulesAsync(typeof(HallMonitorModule).Assembly, serviceProvider);
            await commandService.AddModulesAsync(typeof(MiscellaneousModule).Assembly, serviceProvider);
            await commandService.AddModulesAsync(typeof(MemeGeneratorModule).Assembly, serviceProvider);
            await commandService.AddModulesAsync(typeof(ServerManagmentModule).Assembly, serviceProvider);
        }
    }
}