using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Tools.ViewModels;

namespace Tools.Samples
{
    internal class SampleSunflowerViewModel : SunflowerViewModel
    {
        public SampleSunflowerViewModel()
        {
            Machines = new System.Collections.ObjectModel.ObservableCollection<MachineAccount>
            {
                new MachineAccount(){Title="测试1"},
                new MachineAccount() { Title = "测试2" }
            };
        }
    }
}
