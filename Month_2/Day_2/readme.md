# Deploy IoT Edge modules at Scale # 

## Scenario ##

 ![Architecture](./media/architecture.png 'Architecure')


## **Content - Hands-on Lab:** ##
- [Exercise 1: Create a Custom IoT Central app](#exercise-1-create-a-custom-iot-central-app)
  - [Task 1: Creating an Application](#task-1-creating-an-application)


## **Exercise 1: Create a Custom IoT Central app** ##


### **Task 1: Create IoT Hub** ###


First step we will create the services we need for this architecture using CLI. For that, open cloud shell, clicking in the following icon, top right in Azure Portal:

![Cloud shell](./media/cloud-shell.png 'Cloud Shell')

1. Run the az extension add command to add the Microsoft Azure IoT Extension for Azure CLI to your CLI shell. The IOT Extension adds IoT Hub, IoT Edge, and IoT Device Provisioning Service (DPS) specific commands to Azure CLI.

```bash
az extension add --name azure-iot

```

If you never used before you will be prompted to mount an storage account, click **Create Storage** to continue. If you used before, you will skip this step.

Run the following commmand to create an Azure IoT Hub, make sure to replace the name of your Resource Group and assign a name to your IoT Hub similar to **iotacademySUFFIX**

```bash 
az iot hub create --name <YOUR_HUB_NAME_HERE> --resource-group <YOUR_RG_HERE> --sku S1
```

After a few minutes you should receive a provisioned states succeed message.


2. Once the resource is ready, we will use the below command to create 20 edge devices. Make sure to replace the <YOUR_HUB_NAME_HERE> with the name of your current IoT Hub before running the command:


```bash
az iot hub device-identity create -n <YOUR_HUB_NAME_HERE>    -d device01 --edge-enabled
az iot hub device-identity create -n <YOUR_HUB_NAME_HERE>    -d device02 --edge-enabled
az iot hub device-identity create -n <YOUR_HUB_NAME_HERE>    -d device03 --edge-enabled
az iot hub device-identity create -n <YOUR_HUB_NAME_HERE>    -d device04 --edge-enabled
az iot hub device-identity create -n <YOUR_HUB_NAME_HERE>    -d device05 --edge-enabled
az iot hub device-identity create -n <YOUR_HUB_NAME_HERE>    -d device06 --edge-enabled
az iot hub device-identity create -n <YOUR_HUB_NAME_HERE>    -d device07 --edge-enabled
az iot hub device-identity create -n <YOUR_HUB_NAME_HERE>    -d device08 --edge-enabled
az iot hub device-identity create -n <YOUR_HUB_NAME_HERE>    -d device09 --edge-enabled
az iot hub device-identity create -n <YOUR_HUB_NAME_HERE>    -d device10 --edge-enabled
az iot hub device-identity create -n <YOUR_HUB_NAME_HERE>    -d device11 --edge-enabled
az iot hub device-identity create -n <YOUR_HUB_NAME_HERE>    -d device12 --edge-enabled
az iot hub device-identity create -n <YOUR_HUB_NAME_HERE>    -d device13 --edge-enabled
az iot hub device-identity create -n <YOUR_HUB_NAME_HERE>    -d device14 --edge-enabled
az iot hub device-identity create -n <YOUR_HUB_NAME_HERE>    -d device15 --edge-enabled
az iot hub device-identity create -n <YOUR_HUB_NAME_HERE>    -d device16 --edge-enabled
az iot hub device-identity create -n <YOUR_HUB_NAME_HERE>    -d device17 --edge-enabled
az iot hub device-identity create -n <YOUR_HUB_NAME_HERE>    -d device18 --edge-enabled
az iot hub device-identity create -n <YOUR_HUB_NAME_HERE>    -d device19 --edge-enabled
az iot hub device-identity create -n <YOUR_HUB_NAME_HERE>    -d device20 --edge-enabled

```

3. After a few minutes you you should see all the new devices in your Azure IoT Hub: 

![List of devices](./media/devices-list.png 'List of devices')

4. In the next step we will create a virtual machine  as an edge device, before running the script makesure to replace your **Resource Group**, your **IoT Hub**
and assign a suffix to the dns **my-edge-vm-SUFFIX**

```bash

az deployment group create \
--resource-group <YOUR_RG_HERE> \
--template-uri "https://aka.ms/iotedge-vm-deploy" \
--parameters dnsLabelPrefix='my-edge-vm-SUFFIX' \
 --parameters adminUsername='academyuser'  \
--parameters deviceConnectionString=$(az iot hub device-identity connection-string show --device-id device01 --hub-name <YOUR_HUB_NAME_HERE>    -o tsv) \
--parameters authenticationType='password' \
 --parameters adminPasswordOrKey="IoTAcademy01!"
```

After a few minutes you should see your VM provisioned in the portal. 

![List of devices](./media/vm-edge-device.png 'List of devices')

