using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client.Exceptions;

namespace SmartMeterSimulator
{
    class DeviceManager
    {

        static string connectionString;
        static RegistryManager registryManager;

        public static string HostName { get; set; }

        public static void IotHubConnect(string cnString)
        {
            connectionString = cnString;

            //TODO: 1.Create an instance of RegistryManager from connectionString
            //registryManager = ...;

            var builder = IotHubConnectionStringBuilder.Create(cnString);

            HostName = builder.HostName;
        }
        
        /// <summary>
        /// Register a single device with the IoT hub. The device is initially registered in a
        /// disabled state.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public async static Task<string> RegisterDevicesAsync(string connectionString, string deviceId)
        {
            //Make sure we're connected
            if (registryManager == null)
                IotHubConnect(connectionString);


            //TODO: 2.Create new device
            //Device device = ...;

            //TODO: 3.Initialize device with a status of Disabled 
            //set status here...

            try
            {
                //TODO: 4.Register the new device
                //device = await ...;
            }
            catch (Exception ex)
            {
                if (ex is DeviceAlreadyExistsException ||
                    ex.Message.Contains("DeviceAlreadyExists"))
                {
                    //TODO: 5.Device already exists, get the registered device
                    //device = await ...;

                    //TODO: 6.Ensure the device is disabled until Activated later
                    //device.Status = ...;

                    //TODO: 7.Update IoT Hubs with the device status change
                    //await ...;
                }
                else
                {
                    MessageBox.Show($"An error occurred while registering one or more devices:\r\n{ex.Message}");
                }
            }

            //return the device key
            return device.Authentication.SymmetricKey.PrimaryKey;
        }

        /// <summary>
        /// Activate an already registered device by changing its status to Enabled.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="deviceId"></param>
        /// <param name="deviceKey"></param>
        /// <returns></returns>
        public async static Task<bool> ActivateDeviceAsync(string connectionString, string deviceId, string deviceKey)
        {
            //Server-side management function to enable the provisioned device 
            //to connect to IoT Hub after it has been installed locally. 
            //If device id device key are valid, Activate (enable) the device.

            //Make sure we're connected
            if (registryManager == null)
                IotHubConnect(connectionString);


            bool success = false;
            Device device = null;

            try
            {
                //TODO: 8.Fetch the device
                //device = await ...;

                //TODO: 9.Verify the device keys match
                {
                    //TODO: 10.Enable the device
                    //device.Status = ...;

                    //TODO: 11.Update IoT Hubs
                    //await ...;

                    success = true;
                }
            }
            catch(Exception)
            {
                success = false;
            }

            return success;
        }

        /// <summary>
        /// Deactivate a single device in the IoT Hub registry.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public async static Task<bool> DeactivateDeviceAsync(string connectionString, string deviceId)
        {
            //Make sure we're connected
            if (registryManager == null)
                IotHubConnect(connectionString);

            bool success = false;
            Device device;

            try
            {
                //TODO: 12.Lookup the device from the registry by deviceId
                //device = await registryManager...;

                //TODO: 13.Disable the device
                //device.Status = ...;

                //TODO: 14.Update the registry 
                //await registryManager...;

                success = true;
            }
            catch (Exception)
            {
                success = false;
            }

            return success;

        }

        /// <summary>
        /// Unregister a single device from the IoT Hub Registry
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public async static Task UnregisterDevicesAsync(string connectionString, string deviceId)
        {
            //Make sure we're connected
            if (registryManager == null)
                IotHubConnect(connectionString);

            //TODO: 15.Remove the device from the Registry
            //await registryManager...;
        }

        /// <summary>
        /// Unregister all the devices managed by the Smart Meter Simulator
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public async static Task UnregisterAllDevicesAsync(string connectionString)
        {
            //Make sure we're connected
            if (registryManager == null)
                IotHubConnect(connectionString);

            for(int i = 0; i <= 9; i++)
            {
                string deviceId = "Device" + i.ToString();

                //TODO: 16.Remove the device from the Registry
                //await registryManager...;
            }

        }
    }
}
