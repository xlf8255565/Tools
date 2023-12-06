using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace Tools
{
    public class AppSettings
    {
        private IConfiguration _configuration;

        public AppSettings(IConfiguration configuration)
        {
            _configuration = configuration;
            foreach (var item in typeof(AppSettings).GetProperties())
            {
                if (item.CanWrite)
                    item.SetValue(this, configuration.GetSection(item.Name).Get(item.PropertyType));
            }
        }

        //每次都重新获取配置文件
        protected T GetValue<T>([CallerMemberName] string? propertyName = null)
        {
            return _configuration.GetSection(propertyName).Get<T>();
        }

        public ObservableCollection<MachineAccount> Machines => GetValue<ObservableCollection<MachineAccount>>();
        public List<CmdConfigEntity> Cmds => GetValue<List<CmdConfigEntity>>();
    }
    public partial class CmdConfigEntity:ObservableObject
    {
        public string GroupName { get; set; }
        [ObservableProperty]
        public List<CmdCommmand> _commands;
    }

    public class CmdCommmand
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class MachineAccount
    {
        public string Title { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string WindowsPassword { get; set; }
    }
}
