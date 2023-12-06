using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tools.Helpers
{
    public class LocalConfig
    {
        static LocalConfig _current;
        static string _fileName = AppDomain.CurrentDomain.BaseDirectory + @"\LocalConfig.json";
        private static bool _isInit;
        private static object _lock = new object();
        private string _readQrcodeHotKey = "F1";
        private string _createQrcodeHotKey = "F2";
        private string _connectMachineHotKey = "F3";
        private bool _isAutoLock;
        private string _lockTime;
        private bool _isCommand;

        public static LocalConfig Current
        {
            get
            {
                if (_current == null)
                {
                    _isInit = true;
                    if (File.Exists(_fileName))
                        _current = JsonConvert.DeserializeObject<LocalConfig>(File.ReadAllText(_fileName));
                    else
                        _current = new LocalConfig();
                    _isInit = false;
                }
                return _current;
            }
            set { _current = value; }
        }

        private void Save()
        {
            lock (_lock)
            {
                if (_isInit) return;
                var direct = Path.GetDirectoryName(_fileName);
                if (!Directory.Exists(direct))
                    Directory.CreateDirectory(direct);
                File.WriteAllText(_fileName, JsonConvert.SerializeObject(Current));
            }
        }

        public string CreateQrcodeHotKey
        {
            get => _createQrcodeHotKey; set
            {

                _createQrcodeHotKey = value;
                Save();
            }
        }
        public string ReadQrcodeHotKey
        {
            get => _readQrcodeHotKey; set
            {

                _readQrcodeHotKey = value;
                Save();
            }
        }
        public string ConnectMachineHotKey
        {
            get => _connectMachineHotKey; set
            {
                _connectMachineHotKey = value;
                Save();
            }
        }
        public bool IsAutoLock
        {
            get => _isAutoLock; set
            {
                _isAutoLock = value;
                Save();
            }
        }
        public bool IsCommand
        {
            get => _isCommand; set
            {
                _isCommand = value;
                Save();
            }
        }
        public string LockTime
        {
            get => _lockTime; set
            {
                _lockTime = value;
                Save();
            }
        }
    }
}
