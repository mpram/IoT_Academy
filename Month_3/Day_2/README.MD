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

9. We will be using this virtual machine for the remaining two exercises.
