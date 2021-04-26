# Azure IoT Academy: Windows IoT Lab

In this lab we will be setting up Azure IoT Edge for Linux on Windows (EFLOW) with a Live Video Analytics module to showcase how to Windows IoT OS enables various retail and safety scenarios.  

## **Content:** ##

- [Exercise 1: Set up Environment](#exercise-1-set-up-environment)
   - [Task 1: Virtual Network](#task-1-virtual-network)
   - [Task 2: Virtual Machine](#task-2-virtual-machine)
   - [Task 3: Connect to Virtual Machine](#task-3-connect-to-virtual-machine)
- [Exercise 2: Set up Azure Edge For Linux on Windows (EFLOW)](#exercise-2-set-up-azure-edge-for-linux-on-windows-(EFLOW))
   - [Task 1: Enable Hyper-V](#task-1-enable-hyper-v)
   - [Task 2: Set up Azure IoT Hub](#task-2-set-up-azure-iot-hub)
   - [Task 3: Register an IoT Hub Device](#task-3-register-an-iot-hub-device)
   - [Task 4: Download Windows Admin Center](#task-4-download-windows-admin-center)
   - [Task 5: Create a new deployment](#task-5-create-a-new-deployment)
   - [Task 6: Verify successful configuration](#task-6-verify-successful-configuration)


- [Exercise 4: Clean up](#exercise-7-clean-up)
   - [Task 1: Delete resources](#task-1-Delete-resources)


## **Exercise 1: Set up Environment** ##
During this exercise you will be setting up your Windows 10 IoT Enterprise environment for this lab in an Azure Virtual Machine.

### **Task 1: Virtual Network** ###

1. In your browser, navigate to the [Azure portal](https://portal.azure.com), select **+Create a resource** in the navigation pane, enter `virtual Network` into the **Search the Marketplace** box.

2. Select **Virtual Network** from the results, and then select **Create**.

  ![+Create a virtual network is selected](./media/Exercise-1/Create-VN.png 'Create a Virtual Network')

3. In **Create virtual network**, enter or select this information in the **Basics** tab:

      | Setting | Value |
      | ------- | ----- |
      | **Project details** |   |
      | Subscription | Select your subscription. |
      | Resource group | Select **Create new**.  </br> **Enter a name** for the Resource Group. </br> Select **OK**. |
      | **Instance details** |   |
      | Name | Enter **myVNetwork**. |
      | Region | Select **(US) East US**. |

      ![Virtual Network Basics Tab](./media/Exercise-1/Create-VN-Basics.png 'Virtual Network Basics Tab')

4. Select the **IP Addresses** tab, or select the **Next: IP Addresses** button at the bottom of the page.

5. In **IPv4 address space**, select the existing address space and change it to **10.1.0.0/16**.

6. Select **+ Add subnet**, then enter **MySubnet** for **Subnet name** and **10.1.0.0/24** for **Subnet address range**.

7. Select **Add**.

      ![Virtual Network Add Subnet](./media/Exercise-1/Create-VN-Add-Subnet.png 'Virtual Network Add Subnet')

8. Select the **Security** tab, or select the **Next: Security** button at the bottom of the page.

9. Under **BastionHost**, select **Enable**. Enter this information:

    | Setting            | Value                      |
    |--------------------|----------------------------|
    | Bastion name | Enter **myBastionHost** |
    | AzureBastionSubnet address space | Enter **10.1.1.0/24** |
    | Public IP Address | Select **Create new**. </br> For **Name**, enter **myBastionIP**. </br> Select **OK**. |

10. Select the **Review + create** tab or select the **Review + create** button.

    ![Virtual Network Security Tab](./media/Exercise-1/Create-VN-Security.png 'Virtual Network Security Tab')

11. Select **Create**.

    ![Virtual Network Validation](./media/Exercise-1/Create-VN-Validations.png 'Virtual Network Validation')

12. It will take a few minutes to deploy. At the end you should see the your resources deployed.

    ![Virtual Network Deployment](./media/Exercise-1/Create-VN-Deployment.png 'Virtual Network Deployment')

### **Task 2: Virtual Machine** ###

1. On the upper-left side of the portal, select **Create a resource** > **Compute** > **Virtual machine** . **Create**.

    ![Virtual Machine Create](./media/Exercise-1/Create-VM.png 'Virtual Machine Creation')

2. In **Create a virtual machine**, type or select the values in the **Basics** tab:

    | Setting | Value                                          |
    |-----------------------|----------------------------------|
    | **Project Details** |  |
    | Subscription | Select your Azure subscription |
    | Resource Group | Select Your Resource Group |
    | **Instance details** |  |
    | Virtual machine name | Enter **myVM1** |
    | Region | Select **(US) East US** |
    | Availability Options | Select **No infrastructure redundancy required** |
    | Image | Select **Windows 10 Pro, vNext - Gen1** |
    | Azure Spot instance | Select **No** |
    | Size | **Standard_D4s_v3 - 4 vcpus, 16 GiB memory** |
    | **Administrator account** |  |
    | Username | Enter a username |
    | Password | Enter a password |
    | Confirm password | Reenter password |
    | **Inbound port rules** |    |
    | Public inbound ports | Select **None**. |

    ![Virtual Machine Basics Tab](./media/Exercise-1/Create-VM-Basic.png 'Virtual Machine Basics Tab')

3. Select the **Networking** tab, or select **Next: Disks**, then **Next: Networking**.

4. In the Networking tab, select or enter:

    | Setting | Value |
    |-|-|
    | **Network interface** |  |
    | Virtual network | Select **myVNetwork**. |
    | Subnet | Select **mySubnet** |
    | Public IP | Select **None** |
    | NIC network security group | Select **Basic**|
    | Public inbound ports network | Select **None**. |

    ![Virtual Machine Networking Tab](./media/Exercise-1/Create-VM-Networking.png 'Virtual Machine Networking Tab')

5. Select the **Review + create** tab, or select the blue **Review + create** button at the bottom of the page.

6. Review the settings, and then select **Create**.

    ![Virtual Machine Validation](./media/Exercise-1/Create-VM-Validations.png 'Virtual Machine Validation')

7. It will take a few minutes to deploy. At the end you should see the your resources deployed.
    ![Virtual Machine Deployment](./media/Exercise-1/Create-VM-Deployment.png 'Virtual Machine Deployment')


### **Task 3: Connect to Virtual Machine** ###

1. Navigate to the Azure Portal Home and select your newly created virtual machine.

2. In the VM menu bar, select **Connect**, then select **Bastion**.

  ![Connect VM - Bastion](./media/Exercise-1/Connect-VM-Bastion.png 'Connect Virtual Bastion')

4. In the **Connect** page, select the blue **Use Bastion** button.

  ![Connect VM - Bastion Button](./media/Exercise-1/Connect-VM-Bastion-Button.png 'Connect Virtual Bastion Button')

5. In the **Bastion** page, enter the username and password you created for the virtual machine previously.

6. Select **Connect**.

  ![Connect VM - Connect Bastion](./media/Exercise-1/Connect-VM-Bastion-Connect.png 'Connect Virtual Machine Bastion')

7. A new tab should open, and you should be connected to your virtual machine.

8. **Accept** the default settings.

  !Bastion Settings](./media/Exercise-1/VM-Bastion-Settings.png 'Bastion Settings')

9. **We will be using this virtual machine for the remaining two exercises.**


## **Exercise 2: Set up Azure Edge For Linux on Windows (EFLOW)** ##

### **Task 1: Enable Hyper-V** ###
We are going to enable Hyper-V via PowerShell in the newly created VM.

1. Search for **PowerShell** and right click to select **Run as Administrator**.
  ![Run PowerShell as an Administrator](./media/Exercise-2/PowerShell-Admin.png 'Run PowerShell as an Administrator')

2. Run the following command:

  ```powershell
  Enable-WindowsOptionalFeature -Online -FeatureName Microsoft-Hyper-V -All
  ```

  If the command couldn't be found, make sure you're running PowerShell as Administrator.

3. When the installation has completed, reboot the VM by typing in **Y**.

  ![Hyper-V-Enabled](./media/Exercise-2/Hyper-V-Enabled.png 'Hyper-V-Enabled')

4. Reconnect to the VM.

### **Task 2: Set up Azure IoT Hub** ###

These steps can be done outside of the VM.

1. Navigate to the Azure Portal, select the **+ Create a resource** button, and then enter *IoT Hub* in the **Search the Marketplace** field.

2. Select **IoT Hub** from the search results, and then select **Create**.

  ![Create IoT Hub](./media/Exercise-2/IoTHub.png 'Create IoT Hub')

3. On the **Basics** tab, complete the fields as follows:

   - **Subscription**: Select the subscription to use for your hub.

   - **Resource Group**: Select the resource group you've created for exercise 1 above.

   - **Region**: Select the region in which you want your hub to be located. Select the location closest to you.

   - **IoT Hub Name**: Enter a name for your hub. This name must be globally unique.

      >![IMPORTANT]
      >
      > Because the IoT hub will be publicly discoverable as a DNS endpoint, be sure to avoid entering any sensitive or personally identifiable information when you name it.

  ![IoT Hub Basics](./media/Exercise-2/IoTHub-Basics.png 'IoT Hub Basics Tab')

4. Select **Next: Networking** to continue creating your hub.

   Choose the endpoints that can connect to your IoT Hub. You can select the default setting **Public endpoint (all networks)**.

   ![IoT Hub networking](./media/Exercise-2/IoTHub-Networking.png 'IoT Hub Networking Tab')

5. Select **Next: Management** to continue creating your hub.

    You can accept the default settings here.

6. Select **Next: Tags** to continue to the next screen.

7. Select **Next: Review + create** to review your choices. You see something similar to this screen, but with the values you selected when creating the hub.

8. Select **Create** to create your new hub.

  ![IoT Hub Review Tab](./media/Exercise-2/IoTHub-Review.png 'IoT Hub Review + Create Tab')

9. It will take a few minutes for your Hub to deploy.

  ![IoT Hub Deployment](./media/Exercise-2/IoTHub-Deploy.png 'IoT Hub Deployment')

### **Task 3: Register an IoT Hub Device** ###

1. Navigate to your IoT Hub and select **IoT Edge** from the menu.

  ![Select IoT Edge](./media/Exercise-2/IoTHub-IoTEdge.png 'Select IoT Edge')

2. Select **Add an IoT Edge device**.

  ![Add IoT Edge Device](./media/Exercise-2/Add-IoTEdge-Device.png 'Add IoT Edge Device')

3. Create a Device - Select or Enter:

  | Setting | Value |
  |-|-|
  | Device ID | Provide a descriptive Device ID |
  | Authentication type | **Symmetric Key** |
  | Auto-generate keys | Default - Keep Box Checked |
  | Connect this device to an IoT Hub | Select **Enable**|

4. Select **Save**.

  ![Save IoT Edge Device](./media/Exercise-2/Create-Device.png 'Save IoT Edge Device')

### **Task 4: Download Windows Admin Center** ###
Back to the Windows VM, we need to download Windows Admin Center.

1. To download Windows Admin Center installer, type the following address in the browser: aka.ms/wacdownload

2. A prompt at the bottom of the screen should appear. Select **Run**

  ![Download Windows Admin Center](./media/Exercise-2/download-WAC.png 'Download Windows Admin Center')

3. Run the downloaded installer and follow the install wizard prompts to install Windows Admin Center.

4. Once installed, use a supported browser to open Windows Admin Center. Supported browsers include Microsoft Edge (Windows 10, version 1709 or later), Google Chrome, and Microsoft Edge Insider. Search **Windows Admin Center**

  ![Open Windows Admin Center](./media/Exercise-2/Open-WAC.png 'Open Windows Admin Center')

5. On the first use of Windows Admin Center, you will be prompted to select a certificate to use. Select **Windows Admin Center Client** as your certificate.

6. It is time to install the Azure IoT Edge extension. Select the gear icon in the top right of the Windows Admin Center dashboard.

  ![Open Windows Admin Center](./media/Exercise-2/select-settings-gear.png 'Open Windows Admin Center')


7. On the **Settings** menu, under **Gateway**, select **Extensions**.

8. On the **Available extensions** tab, find **Azure IoT Edge** in the list of extensions. Choose it, and select the **Install** prompt above the list of extensions.

  ![Install Azure IoT Edge](./media/Exercise-2/install-azure-iot-edge.png 'Install Azure IoT Edge')

9. After the installation completes, you should see Azure IoT Edge in the list of installed extensions on the **Installed extensions** tab.

  ![Extension Installed](./media/Exercise-2/installed.png 'Extension Installed')

### **Task 5: Create a new deployment** ###

On the Windows Admin Center start page, under the list of connections, you will see a local host connection representing the PC where you running Windows Admin Center. Any additional servers, PCs, or clusters that you manage will also show up here.

You can use Windows Admin Center to make install and manage Azure IoT Edge for Linux on Windows on either your local device or remote managed devices. In this guide, the local host connection will serve as the target device for the deployment of Azure IoT Edge for Linux on Windows.

1. Select **Add**.

  ![Add](./media/Exercise-2/Add.png 'Add')

2. On the **Add or create resources** pane, locate the **Azure IoT Edge** tile. Select **Create new** to install a new instance of Azure IoT Edge for Linux on Windows on a device.

  ![Create New Azure IoT Edge Resource](./media/Exercise-2/create-new.png 'Create New Azure IoT Edge Resource')

3. The **Create an Azure IoT Edge for Linux on Windows deployment** pane will open. On the **1. Getting Started** tab, verify that your target device meets the minimum requirements, and select **Next**.

  ![Getting Started - Prerequisites](./media/Exercise-2/pre-req.png 'Getting Started - Prerequisites')

4. Review the license terms, check **I Accept**, and select **Next**.

5. You can toggle **Optional diagnostic data** on or off, depending on your preference.

6. Select **Next: Deploy**.

7. On the **2. Deploy** tab, under **Select a target device**, click on your listed device to validate it meets the minimum requirements. Once its status is confirmed as supported, select **Next**.

  ![Select Target Device](./media/Exercise-2/target-device.png 'Select Target Device')

8. On the **2.2 Settings** tab, review the configuration settings of your deployment. Once you are satisfied with the settings, select **Next**.

9. On the **2.3 Deployment** tab, you can watch the progress of the deployment. The full process includes downloading the Azure IoT Edge for Linux on Windows package, installing the package, configuring the host device, and setting up the Linux virtual machine. This process may take several minutes to complete. A successful deployment is pictured below.

  ![Successful Deployment](./media/Exercise-2/successful-deployment.png 'Successful Deployment')

10. Select **Next: Connect**

11. Go back to the Azure Portal in another browser tab (outside of the VM) and navigate to the **IoT Edge tab** of your **IoT Hub**.

  ![IoT Edge Device](./media/Exercise-2/IoT-Edge-Device-IoT-Hub.png 'IoT Edge Device')

12. Click on the device ID of your device. Copy the Primary Connection String field.

  ![Retrieve Primary Connection String](./media/Exercise-2/primary-connection-string.png 'Retrieve Primary Connection String')

13. Paste it into the device connection string field in the Windows Admin Center. Then, choose Provisioning with the selected method.

  ![Device Provisioning](./media/Exercise-2/device-provisioning.png 'Device Provisioning')

14. Select **Finish** once Azure IoT device has successfully provisioned.

### **Task 6: Verify successful configuration** ###

1. Select your IoT Edge device from the list of connected devices in Windows Admin Center to connect to it.

  ![Connect to Device](./media/Exercise-2/connect-to-device.png 'Connect to Device')

1. The device overview page displays some information about the device:

    1. The **IoT Edge Module List** section shows running modules on the device. When the IoT Edge service starts for the first time, you should only see the **edgeAgent** module running. The edgeAgent module runs by default and helps to install and start any additional modules that you deploy to your device.
    1. The **IoT Edge Status** section shows the service status, and should be reporting **active (running)**.

1. If you need to troubleshoot the IoT Edge service, use the **Command Shell** tool on the device page to ssh (secure shell) into the virtual machine and run the Linux commands.

  ![Troubleshoot](./media/Exercise-2/troubleshoot.png 'Troubleshoot')

    1. If you need to troubleshoot the service, retrieve the service logs.

       ```bash
       journalctl -u iotedge
       ```

    2. Use the `check` tool to verify configuration and connection status of the device.

       ```bash
       sudo iotedge check
       ```

>[!TIP]
>
> If you Azure IoT Edge VM is not running due to a break or interrupt in connection, please visit Hyper-V Manager:
> 1. Start the VM, if you are unable to start the VM, right click on the VM and clear "last saved state" and try again
> 2. Visit the networking tab at the bottom and wait until an IP address has been populated to ensure that your VM is running.
