﻿using ReasignAddressesCAP32.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace ReasignAddressesCAP32
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region Attributies
        private List<Device> _devices;
        private Device _selectedDevice;
        #endregion

        #region Properties
        public List<Device> Devices
        {
            get => _devices;
            set
            {
                _devices = value;
                OnPropertyChanged();
            }
        }

        public Device SelectedDevice
        {
            get => _selectedDevice;
            set
            {
                _selectedDevice = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructors
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }
        #endregion

        #region Events
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new TedisNetEntities())
                {
                    // 4: CAP32
                    Devices = context.Devices.Where(d => d.DeviceTypeId == 4).OrderBy(d => d.Name).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        #region Methods: Tags refactoring
        private void BtnStartCAP32TagsRefactoring_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int rowChanged = 0;
                if (SelectedDevice != null)
                {
                    using (var context = new TedisNetEntities())
                    {
                        // Check tags
                        CheckBadTags(context, SelectedDevice);
                        // Refactoring cap32 tags
                        var tagsCAP32 = context.Tags.Where(t => t.DeviceId == SelectedDevice.Id)
                            .OrderBy(t => t.ShortName).ToList(); // Like AI.001, AI.002
                                                                 // Id  Name
                                                                 // 1   DI
                                                                 // 3   AI
                                                                 // 5   DO
                                                                 // 8   SYS
                                                                 // 20  ES
                                                                 // 29  EC
                                                                 // 31  CDI
                                                                 // 33  CDO
                                                                 // 36  AO
                                                                 // 38  GE
                                                                 // 40  VA
                                                                 // 45  SPO
                                                                 // 47  SPI
                        var tagsCAP32DIs = tagsCAP32.Where(t => t.TagClass.ParentTagClassId == 1).ToList();
                        var tagsCAP32CDIs = tagsCAP32.Where(t => t.TagClass.ParentTagClassId == 31).ToList();
                        var tagsCAP32AIs = tagsCAP32.Where(t => t.TagClass.ParentTagClassId == 3).ToList();
                        var tagsCAP32DOandCDOs = tagsCAP32.Where(t => t.TagClass.ParentTagClassId == 5).ToList();

                        var lastDIandCDI = SetDataTagsCAP32(tagsCAP32DIs);
                        var lastAI = SetDataTagsCAP32(tagsCAP32AIs, lastDIandCDI);
                        var lastDOandCDO = SetDataTagsCAP32(tagsCAP32DOandCDOs); // PROSA hasn't command digital output (double)

                        // Get sub devices
                        var subDevices = context.Devices.Where(d => d.ParentDeviceId == SelectedDevice.Id)
                            .OrderBy(d => d.Name).ToList();

                        foreach (var subDevice in subDevices)
                        {
                            // Check tags
                            CheckBadTags(context, subDevice);

                            var tagsDevice = context.Tags.Where(t => t.DeviceId == subDevice.Id)
                                .OrderBy(t => t.ShortName).ToList(); // Like AI.001, AI.002
                            var tagsDIs = tagsDevice.Where(t => t.TagClass.ParentTagClassId == 1).ToList();
                            var tagsCDIs = tagsDevice.Where(t => t.TagClass.ParentTagClassId == 31).ToList();
                            var tagsAIs = tagsDevice.Where(t => t.TagClass.ParentTagClassId == 3).ToList();
                            var tagsDOs = tagsDevice.Where(t => t.TagClass.ParentTagClassId == 5).ToList();
                            var tagsCDOs = tagsDevice.Where(t => t.TagClass.ParentTagClassId == 33).ToList();

                            lastDIandCDI = SetDataTagsSubDevice(lastDIandCDI, tagsDIs);
                            lastDIandCDI = SetDataTagsSubDevice(lastDIandCDI, tagsCDIs);
                            lastAI = SetDataTagsSubDevice(lastAI, tagsAIs);
                            lastDOandCDO = SetDataTagsSubDevice(lastDOandCDO, tagsDOs);
                            lastDOandCDO = SetDataTagsSubDevice(lastDOandCDO, tagsCDOs);
                        }

                        rowChanged = context.SaveChanges();
                    }
                }
                MessageBox.Show($"{rowChanged} Tags cambiados!", "100%", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CheckBadTags(TedisNetEntities context, Device device)
        {
            var badTags = context.Tags.Where(t => t.DeviceId == device.Id
                && (t.TagClassId == 1 || t.TagClassId == 3 || t.TagClassId == 5 || t.TagClassId == 31
                || t.TagClass.ParentTagClassId == null)).ToList();
            if (badTags.Count > 0)
            {
                var ids = string.Join(",", badTags.Select(t => t.Id));
                var shortNames = string.Join(",", badTags.Select(t => t.ShortName));
                throw new Exception($"Tags con TagClass masters DI/AI/CDI: '{ids}' ({shortNames}), Device id: '{device.Id}', name: '{device.Name}'");
            }
        }

        private int SetDataTagsSubDevice(int lastAddress, List<Tag> tags)
        {
            int address = lastAddress;
            int terminal = 1;
            foreach (var tag in tags)
            {
                // Don't change tag.Address, need original configuration (Address = MODBUS Address + Offset, Prosa Address, ...)

                // Distinct set by parent tag class
                switch (tag.TagClass.ParentTagClassId)
                {
                    case 31: // CDI: Case SEPAM, other case to study
                        tag.LocalAddress = null;
                        tag.TelecontrolAddress = address;
                        tag.TelecontrolIOAddress = null;
                        tag.DeviceTerminal = null;
                        address++;
                        break;
                    case 1: // DI: Case SEPAM, other case to study
                        // Check if is operand of CDI
                        // Composición de 2 tags (RTU): Operation Type #1
                        if (tag.Operands.Count > 0 && tag.Operands.Any(o => o.Operation.OperationTypeId == 1))
                        {
                            tag.TelecontrolAddress = null;
                        }
                        else
                        {
                            tag.TelecontrolAddress = address;
                            address++;
                        }
                        tag.LocalAddress = null;
                        tag.TelecontrolIOAddress = null;
                        tag.DeviceTerminal = terminal;
                        terminal++;
                        break;
                    default:
                        tag.LocalAddress = null;
                        tag.TelecontrolAddress = address;
                        tag.TelecontrolIOAddress = null;
                        tag.DeviceTerminal = terminal;
                        address++;
                        terminal++;
                        break;
                }
            }
            return address;
        }

        private int SetDataTagsCAP32(List<Tag> tagsCAP32, int lastDIandCDI = 0)
        {
            int address = 0;
            int terminal = 1;
            foreach (var tag in tagsCAP32)
            {
                tag.Address = null;
                tag.LocalAddress = null;
                tag.TelecontrolAddress = address;
                tag.TelecontrolIOAddress = null;
                tag.DeviceTerminal = terminal;
                address++;
                terminal++;
            }
            return address;
        }
        #endregion

        #region Methods: Ports & Channels
        private void BtnStartCAP32PortChannels_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int rowChanged = 0;
                Dictionary<int, Device> uniqueSlaveAddress = new Dictionary<int, Device>();
                Dictionary<int, Device> uniqueDeviceAddress = new Dictionary<int, Device>();
                if (SelectedDevice != null)
                {
                    using (var context = new TedisNetEntities())
                    {
                        // Check if parent CAP32 has port and channel for communicate with CC
                        var portsCAP32 = context.Ports.Where(p => p.DeviceId == SelectedDevice.Id).ToList();
                        Require(portsCAP32.Count > 0, "CAP32 no tiene nigun puerto asignado");

                        // CC device is 1
                        var channelToCC = context.Channels.FirstOrDefault(c => c.MasterPort.DeviceId == 1 && c.SlavePort.DeviceId == SelectedDevice.Id);
                        Require(channelToCC != null, "CAP32 no tiene canal al CC");

                        // Check address of CAP32 for CC
                        var portCap32ToCC = channelToCC.SlavePort;
                        Require(portCap32ToCC.Address.HasValue, "CAP32 no tiene direccion asignada en su puerto vinculado al CC");

                        var subDevices = context.Devices.Where(d => d.ParentDeviceId == SelectedDevice.Id)
                            .OrderBy(d => d.Name).ToList();

                        // Check if CAP32 has children
                        if (subDevices.Count > 0)
                        {
                            // In this case CAP32 need to have minimum 1 port, where is master for communicate with children
                            var portsMasterCap32 = portsCAP32.Where(p => p.IsMaster).ToList();
                            Require(portsCAP32.Count > 0, $"CAP32 no tiene nigun puerto master hacia sus {subDevices.Count} esclavos");

                            // Check if all devices has ports (is necesary every subDevice has port and channel to parent)
                            foreach (var subDevice in subDevices)
                            {
                                // Check slave number                                    
                                Require(subDevice.SlaveNumber.HasValue, $"{subDevice.Name} no tiene nigun numero de esclavo asignado siendo hijo directo del CAP32 '{SelectedDevice.Name}'");
                                var slaveNumber = subDevice.SlaveNumber.Value;

                                // Check unique slave number
                                Require(!uniqueSlaveAddress.TryGetValue(slaveNumber, out Device addedDevice),
                                    $"El numero de esclavo del dispositivo '{subDevice.Name}' no es unico, " +
                                    $"ya existe otro dispositivo con mismo numero '{slaveNumber}': " +
                                    $"{addedDevice?.Name}");

                                uniqueSlaveAddress.Add(slaveNumber, subDevice);

                                // Check device address                                    
                                Require(subDevice.Address.HasValue, $"{subDevice.Name} no tiene niguna dirección asignada '{SelectedDevice.Name}'");
                                var deviceAddress = subDevice.Address.Value;

                                // Check unique device address
                                Require(!uniqueDeviceAddress.TryGetValue(deviceAddress, out Device alreadyAddedDevice),
                                    $"El numero de del dispositivo '{subDevice.Name}' no es unico, " +
                                    $"ya existe otro dispositivo con mismo numero '{deviceAddress}': " +
                                    $"{alreadyAddedDevice?.Name}");

                                uniqueDeviceAddress.Add(deviceAddress, subDevice);

                                // Check channel between CAP32 and subDevice
                                var channelCap32andSubDevice = context.Channels.SingleOrDefault(c => c.MasterPort.DeviceId == SelectedDevice.Id
                                    && c.SlavePort.DeviceId == subDevice.Id);

                                Require(channelCap32andSubDevice != null,
                                    $"Subdispositivo '{subDevice.Name}' no tiene canal al CAP32 '{SelectedDevice.Name}'");

                                // Check device address is equal port address
                                var portAddress = channelCap32andSubDevice.SlavePort.Address;
                                Require(portAddress == deviceAddress, $"Direccion en el puerto no coincide con la direccion en el dispositivo");
                            }

                            // Check if exist SlaveAddress is consecutive and start with 1
                            var orderedSubDevices = subDevices.OrderBy(d => d.SlaveNumber).ToList();
                            for (int i = 1; i <= orderedSubDevices.Count; i++)
                            {
                                Require(orderedSubDevices[i - 1].SlaveNumber == i, $"Los subdispositivos del CAP32 tienen que " +
                                    $"tener numeracion de esclavos consecutiva y empezar por 1 (no es lo mismo que dirección del dispositivo). " +
                                    $"Dispositivo que rompe la secuencia es: '{orderedSubDevices[i - 1].Name}', tiene numero de esclavo '{orderedSubDevices[i - 1].SlaveNumber}', " +
                                    $"pero tendria que tener '{i}'");
                            }
                        }
                        rowChanged = context.SaveChanges();
                    }
                }
                MessageBox.Show($"{rowChanged} Cambios!", "100%", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Require(bool condition, string error)
        {
            if (!condition)
            {
                throw new Exception(error);
            }
        }
        #endregion
    }
}