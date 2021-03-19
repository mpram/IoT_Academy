# Deploy IoT Edge modules at Scale #

## Scenario ##

 ![Architecture](./media/architecture.png 'Architecure')


## **Content - Hands-on Lab:** ##
- [Exercise 1: Create a Custom IoT Central app](#exercise-1-create-a-custom-iot-central-app)
  - [Task 1: Creating an Application](#task-1-creating-an-application)


## **Exercise 1: Setting up your Environment** ##


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

### **Task 2: Connect Virtual Machine to IoT Hub** ###

In this step we will connect the virtual machine jsut created in previous step to Azure IoT Hub, assigning the device01.

1. Launch putty locally, copy the IP of the virtual machine in the overview tab

You will find the public IP for your VM as shown below:

![VM Public IP](./media/vm-ip.png 'VM Public IP')

Copy and paste the IP in the **Host Name** section in Putty, then click **Yes** to continue. Now you will see the **Login** steps, use the credentials from the script creation you applied in previous step.

2. Once login run the following command to edit the connection string to your device

```bash
sudo nano /etc/iotedge/config.yaml
```

Now you can replace the connection string from Azure portal to the config.yaml file in your device: 

![Device Connection String](./media/device-cs.png 'Device Connection String')


The connectio string to paste in the section above you will find it in Azure IoT Hub, **Automatic Device Management** section, **IoT Edge**, click in **device01**, copy **Primary Connection String**


![Device Connection String](./media/connection-string.png 'Device Connection String')

After you replace the connection string, **Crtl+X** to save the changes and **Y** to confirm 

Restart your edge deamon with the below command: 

```bash
sudo systemctl restart iotedge
```

After a few minutes you should be able to see the edgeAgent container running in your Virtual machine executing the following command:

```bash
sudo iotedge list
```

## **Exercise 2: Assigning Tags** ##
In this exercise you will learn how to assign tags to your devices using different tools. According to our architecture we will assign Tags according to the following distribution: 

- Devices: 1-3 Dev Env, Location Tampa
- Devices: 4-6 Dev Env, Location Seattle
- Devices: 7-13 Dev Prod, Location Seattle
- Devices: 14-13 Dev Prod, Location Tampa



### **Task 1: Device Twins** ###
In this first task you will assign task using the Azure Portal, modifiying the device twin of the edge device. 

1. Go to Azure IoT Hub, **Automatic Device Management**, then **IoT Edge** select **device01**:


![Device Twin](./media/device-twin.png 'Device Twin')

Once you open the devie twin you can add your tags, copy and paste the following json right below **version** section:

```json
  "tags": {
    "env": "dev",
    "location": "Tampa"
  },
  ```

 Then click **Save** on the top

Your Device Twin should look like the below screen:

![Portal Tags](./media/tags-portal.png 'Device Twin Tags')


### **Task 2: Visual Studio Code** ###

Next we will assing tags using Visual Studio Code.

1. Launch Visual Studio Code.
If it is your fisrt time using VS Code with Azure IoT hub, you will need to follow the next step:

Click in the left menu **Extensions**, in search box type **Azure IoT Tools**, once you select the extension, you will have an **install** buttom on the right screen, click on it to start the installation

![VS Code Extensiones](./media/extensions-vscode.png 'VS Code Azure IoT Hub Extension')

2. After this step Go to the **View** menu select **Command Palette** in the search box type **Azure IoT Hub: Select IoT Hub** follow the steps to select Subscription, Resource group and finally the IoT Hub created for this training. 

3. In the left menu select **Files** you should see at the bottom a new section for **Azure IoT Hub**, expanding this section you should see your IoT Hub and all the devices.



  ![VS Code IoT Hub](./media/vscode-iothub.png 'VS Code Azure IoT Hub ')

4. Right click on **device02** select **Edit Device Twin**

5. At the bottom of the new file, you will a tags section, add there the following json

```json
  "tags": {
    "env": "dev",
    "location": "Tampa"
  }
```
Your new file should like the below sreen:

![VS Code IoT Hub](./media/vscode-device-twin.png 'VS Code Azure IoT Hub ')

6. Save your new file, **Ctrl+S**. After saving, right click any area of the new file and select **Update Device Twin**. 

7. You should receive a message in the terminal specifiying **Device Twin updated successfully**. You can validate the changes in Azure Portal accessing the device twin of your device.


### **Task 3: IoT Hub Explorer** ###

1. Launch Azure Iot explorer locally. Add a new connection 

![IoT explorer](./media/iot-explorer.png 'IoT Explorer')

2. In the new screen paste the IoT Hub **Primary connection string** you will find it in the **Shared access policies** section.


![IoT explorer](./media/iot-hub-cs.png
 'IoT Explorer')

 3. After pasting the connection string, click **Save**, next you will see the list of all your devices. Select **device03**

4. Select **Device Twin** and assign the new tags after the version section, then click **Save**


```json
  "tags": {
    "env": "dev",
    "location": "Tampa"
  }
```
Your new twin should look like the below image: 

![IoT explorer](./media/explorer-tags.png 'IoT Explorer')

5. Now that you know some of the tools available complete the rest of the tags assignment for the devices with you prefer tool.


## **Exercise 3: Querying Devices** ##

1. To query your devices, you can use the **Query explorer**. Go to your IoT Hub, **Explorers**, then clikc in **Query Explorer** now you should be able to write your queries on the right side panel.

![IoT explorer](./media/query-explorer.png 'IoT Explorer')


2. Try different queries to identify devices by location or environment or both

```sql
SELECT * FROM devices where tags.location='Tampa'
```

```sql
SELECT * FROM devices where tags.location='Tampa' and tags.env='prod'
```

3. You can run queries based on grouping functions accorting to properties or status jobs.

```sql
SELECT properties.reported.telemetryConfig.status AS status,
    COUNT() AS numberOfDevices
  FROM devices
  GROUP BY properties.reported.telemetryConfig.status
  ```


