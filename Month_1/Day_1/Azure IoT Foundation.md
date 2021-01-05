# Internet of Things

1) Azure IoT Foundation, theory

2) Getting familiar with Azure Portal
https://ms.portal.azure.com/


## Exercise 1: IoT Hub provisioning

### Task 1: Provision IoT Hub through the Portal


1. In your browser, navigate to the [Azure portal](https://portal.azure.com), select **+Create a resource** in the navigation pane, enter `iot` into the **Search the Marketplace** box.

2. Select **IoT Hub** from the results, and then select **Create**.

   ![+Create a resource is highlighted in the navigation page of the Azure portal, and "iot" is entered into the Search the Marketplace box. IoT Hub is highlighted in the search results.](./media/create-resource-iot-hub.png 'Create an IoT Hub')

   
3. On the **IoT Hub** blade **Basics** tab, enter the following:

   - **Subscription**: Select the subscription you are using for this hands-on lab.

   - **Resource group**: Choose Use existing and select the  resource group.

   - **Region**: Select the location you are using for this hands-on lab.

   - **IoT Hub Name**: Enter a unique name, such as `my-hub-SUFFIX`.

     ![The Basics blade for IoT Hub is displayed, with the values specified above entered into the appropriate fields.](./media/iot-hub-basics-blade.png 'Create IoT Hub Basic blade')

   - Select **Next: Size and Scale**.

   - On the **Size and scale** tab, accept the default Pricing and scale tier of **S1: Standard tier**, and select **Review + create**.

   - Select **Create** on the **Review + create** blade.

4. When the IoT Hub deployment is completed, you will receive a notification in the Azure portal. Select **Go to resource** in the notification.

   ![Screenshot of the Deployment succeeded message, with the Go to resource button highlighted.](./media/iot-hub-deployment-succeeded.png 'Deployment succeeded message')

### Task 2: Provision IoT Hub through CLI

Open cloud with the below link
    
    https://shell.azure.com/

Change to **Bash** access

   ![Screenshot of Bash access.](./media/bash.jpg 'Access Bash Link')

Once you are login run the following command to create an IoT Hub.

 ```csharp  
az iot hub create --name {your iot hub name} \
   --resource-group {your resource group name} --sku S1

```
Verify your IoT Hub has been created in the Portal. 

To delete the IoT Hub just created you can use a delete command:
```csharp 
az iot hub delete --name {your iot hub name}  --resource-group {your resource group name} 
```

### Task 3: Provision IoT Hub through VS Code

Last we will use another tool to also create an IoT Hub, in this case VS Code. For this task make sure you download VS Code in advance.
Download link: 
    https://code.visualstudio.com/Download

1. Install IoT Tools extension for VS Code:
https://marketplace.visualstudio.com/items?itemName=vsciot-vscode.azure-iot-tools

2. Go To View Explorer

  ![VS Code IoT Hub Access.](./media/vscode-view-explorer.png 'IoT Hub access')

  Now you should be able to see the **Azure IoT Hub**

   ![VS Code IoT Hub Access.](./media/vscode-iothub.png 'IoT Hub access')

To create a new IoT Hub Go to the menu **View** on the top toolbar then select **Command Palette**

   ![VS Code IoT Hub Create.](./media/vscode-command-palette.png 'IoT Hub Create')

Type **Azure IoT Hub** in the new window,  then you will see the list of commands available select  **Azure IoT Hub: Create IoT Hub** and click Enter. Then you will need to select the following parameters:

   - **Subscription**: Select the subscription you are using for this hands-on lab.
   - **Resource group**: Use existing and select the resource group.
   - **Location**: Select the location you are using for resources in this hands-on lab.
   - **SKU**: Select **S1**.
   - **Name**: Assign a name to the IoTHub `vscodeiotcreate` add suffix as needed.

After the creation process you should be able to see the new IoT Hub in Azure Portal and in VS Code.




## Exercise 2: Devices

During this exercise you will learn how to set up and edge device and connect your device to IoT Hub to start streaming data.

### Task 1: Setting up a Device

From Azure Portal select the IoT Hub created through VS Code previously, scroll down to **Automatic Device Management** then select **Add an IoT Edge device**

  ![Portal Edge Device Create.](./media/iothub-create-edgedevice.png 'Create Edge Device')

  In the new window select a name for your device **my-device-SUFFIX** and click **Save**

   ![Portal Edge Create.](./media/edge-create.png 'Edge Device')


After Creation your device will be available with new information, click on the device


 ![Portal Edge Device.](./media/device.png 'Edge Device')

Now, copy and paste in the a notepad the connection string of your device, you will need this to connect your device to IoT hub, we will use this connection string in the next task.

![Portal Connection String.](./media/device-connectionstring.png 'Device Connection String')



### Task 2: Setting up an IoT Edge Device


1. From Azure Portal select **Create resource** then from the most Popular list select **Ubuntu Server 18.04 LTS** if you don't see it type the same in the Search window.

   ![Portal Ubuntu Create.](./media/create-resource-ubuntuserver.png 'Create Ubuntu Server')

2. Then you will need to complete the following parameters in the **Basics** tab:

   - **Subscription**: Select the subscription you are using for this hands-on lab.
   - **Resource group**: Use existing and select the resource group.
   - **Virtual Machine Name**: edgedevice+SUFFIX
   - **Region**: Select the location you are using for resources in this hands-on lab.
   - **Availability Options**: Select **No Infrastructure redundancy required**.
   - **Image**: Default it is ok.
   - **Size**: Keep the default selection
   - **Authentication Type**: Select **Password**
   - **Username**: TBD
   - **Password**: TBD
   - **Allow selected ports**: SSH 22

   In **Management** Tab disable monitoring for this lab.

   Last select **Review + Create** after successfull validation you should be able to click **Create**

3. Once the resource is available clikc in the Virtual Machine, you should see in the Overview section the Public IP to connect, copy the IP.

 ![Ubuntu IP.](./media/Ubuntu-server-ip.png 'Create Ubuntu Server IP')


4. In your laptop open Putty, locally, to connect to the Virtual Machine just created and configure IoT Edge.

Paste the IP in the following window: 
 ![Putty Access.](./media/Putty-access.png 'Putty access')

**Note**: If your laptop doesnt have putty installed, you can download it from here:
https://www.putty.org/


5. Once connected you will ask to enter user and password. After successfull login, let's start to install the Edge Runtime


Install the repository configuration that matches your device operating system.

  ```bash
curl https://packages.microsoft.com/config/ubuntu/18.04/multiarch/prod.list > ./microsoft-prod.list
 ```
Copy the generated list to the sources.list.d directory.

 ```bash
sudo cp ./microsoft-prod.list /etc/apt/sources.list.d/
 ```

Install the Microsoft GPG public key.
 ```bash
curl https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.gpg
sudo cp ./microsoft.gpg /etc/apt/trusted.gpg.d/
 ```

Azure IoT Edge software packages are subject to the license terms located in each package (usr/share/doc/{package-name} or the LICENSE directory). Read the license terms prior to using a package. Your installation and use of a package constitutes your acceptance of these terms. If you do not agree with the license terms, do not use that package.

**Install a Container Engine**
Update package lists on your device.
 ```bash
    sudo apt-get update
 ```
Install the Moby engine.
   ```bash
   sudo apt-get install moby-engine
  ```
If you want to install the most recent version of the security daemon, use the following command that also installs the latest version of the libiothsm-std package:
 ```bash
   sudo apt-get install iotedge
 ```
Configure the connection to your IoT Hub, we will apply the connection string you copied in Task 1. Open the configuration file in your device to edit the connection string with the following command.


 ```bash
sudo nano /etc/iotedge/config.yaml
 
  ```

Once in the nano editor, scroll down to **Manual Provisioning configuration using a connection string** then replace the **device_connection-string** variable wit the connection string from the task 1.

 ![Config File.](./media/config-yaml-file.png 'Config File')


After configuring your connectivity, press **CrtL+X** to close the file and select  **Y** to save the changes

Now restart your edge daemon
 ```bash
sudo systemctl restart iotedge
 ```

In a few minutes you should receive a **Running** Status after running the following command:

 ```bash
sudo iotedge list
 ```

## Target audience

IoT Academy Atendees





### Hands-on lab

At the end of this hands-on lab, you will be better able to to understand core services when building an IoT Solution.

## Azure services and related products

- VS Code, installed locally
- Putty, installed locally
- Azure IoT Hub
- CLI
- Ubuntu Server


## Azure solution

Internet of Things
