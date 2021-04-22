**Contents**



- [Internet of Things before the hands-on lab setup guide](#internet-of-things-before-the-hands-on-lab-setup-guide)
  - [Requirements](#requirements)
  - [Before the hands-on lab](#before-the-hands-on-lab)
    - [Task 1: Provision a resource group](#task-1-provision-a-resource-group)
    - [Task 2: Setup a lab virtual machine](#task-2-setup-a-lab-virtual-machine)
    - [Task 3: Setup an Azure Digital Twins instance](#task-3-setup-an-Azure-Digital-Twins-instance)
    - [Task 4: Verify success and collect important values](#task-4-verify-success)
    

<!-- /TOC -->

# Internet of Things before the hands-on lab setup guide

## Requirements

- Microsoft Azure subscription must be pay-as-you-go or MSDN.
  - Trial subscriptions will not work.
- A virtual machine configured with:
  - Visual Studio Community 2019 or later
  - Azure SDK 2.9 or later (Included with Visual Studio)


## Before the hands-on lab

Duration: 45 minutes

In the Before the hands-on lab exercise, you will set up your environment for use in the rest of the hands-on lab. You should follow all the steps provided in the Before the hands-on lab section to prepare your environment **before attending** the hands-on lab. Failure to do so will significantly impact your ability to complete the lab within the time allowed.

> **IMPORTANT**: Most Azure resources require unique names. Throughout this lab you will see the word “SUFFIX” as part of resource names. You should replace this with your Microsoft alias, initials, or another value to ensure the resource is uniquely named.

### Task 1: Provision a resource group

In this task, you will create an Azure resource group for the resources used throughout this lab.

1. In the [Azure portal](https://portal.azure.com), select **Resource groups**, select **+Add**, then enter the following in the **Create an empty resource group** blade:

   - **Name**: Enter `hands-on-lab-SUFFIX`

   - **Subscription**: Select the subscription you are using for this hands-on lab.

   - **Resource group location**: Select the region you would like to use for resources in this hands-on lab. Remember this location so you can use it for the other resources you'll provision throughout this lab.

     ![Create Resource Group](media/create-resource-group.jpg 'Create resource group')

   - Select **Create**.

### Task 2: Setup a lab virtual machine

In this task, you will provision a virtual machine running Visual Studio Community 2019 that you will use as your development machine for this hands-on lab.

> **Note**: Your Azure subscription must have MSDN offers associated with it to provision a new virtual machine with Visual Studio pre-loaded. If your subscription does not meet this requirement, you will need to either create a new VM with the same settings below, but without Visual Studio pre-installed, then install Visual Studio Community 2019, or install it on your own machine.

> **Optional**: If you already have Visual Studio 2019 or greater installed on your machine, you may skip this step.

1. In the [Azure portal](https://portal.azure.com/), select **+Create a resource**

2. In the **Search the marketplace** textbox, enter `Visual Studio 2019 Latest` and then select it from the suggested options.

3. For the **Select a software plan**, select **Visual Studio 2019 Enterprise on Windows Server 2019 (x64)** from the results, and select **Create**.

   ![In the Visual Studio 2019 Latest resource overview screen, Visual Studio Community 2019 (latest) on Windows Server 2019 (x64) is selected in the Select a software plan drop down list.](./media/visual-studio-vm.jpg 'Create Windows Server 2019 with Visual Studio Community 2019')

4. Set the following configuration on the **Basics** tab:

   - **Subscription**: Select the same subscription you are using for this hands-on lab.

   - **Resource Group**: Select Use existing and select the **hands-on-lab-SUFFIX** resource group.

   - **Virtual machine name**: Enter `LabVM`

   - **Region**: Select the same region you selected for the resource group.

   - **Availability options**: Select **No infrastructure redundancy required**.

   - **Image**: The Visual Studio Community image you selected in the previous step should be listed here.

   - **Size**: Select the **Standard D2 v3** size if it is not already selected.

   - **Username**: Enter `demouser`

   - **Password**: Enter a password that you remember.

   - **Public inbound ports**: Select **Allow selected ports**.

   - **Selected inbound ports**: Select **RDP (3389)** from the drop down.

     ![Screenshot of the Create virtual machine blade, with fields set to the previously mentioned settings.](media/virtual-machine.jpg 'Create virtual machine blade')

   - **License Type**: Select **Windows Server** from the drop down.

   - Go to the **Management** tab in **Monitoring** section click on **Disable** for **Boot Diagnostics**

   - Select **Review + create** to move to the next step.

5. Select **Create** on the Create blade to provision the virtual machine.

6. It may take 10+ minutes for the virtual machine to complete provisioning.

7. You can move on to the next task while waiting for the lab VM to provision.

### Task 3: Set up an Azure Digital Twins instance 

In this task, you will set up a new Azure Digital twins instance 

1. **Checking your subscription access**

    To be able to complete all the steps in this article, you need to have a role in your subscription that has the following permissions:

    - Create and manage Azure resources
    - Manage user access to Azure resources (including granting and delegating permissions)

    Common roles that meet this requirement are **Owner, Account admin, or the combination of User Access Administrator and Contributor**. 

    To view your role in your subscription, visit the subscriptions page in the Azure portal (you can use this link or look for Subscriptions with the portal search bar). Look for the name of the subscription you are using, and view your role for it in the My role column:

    ![SubscriptionCheck](./media/SubscriptionCheck.jpg 'SubscriptionCheck')


    If you find that the value is Contributor, or another role that doesn't have the required permissions described above, you can contact the user on your subscription that does have these permissions (such as a subscription Owner or Account admin) and proceed in one of the following ways:

    Request that they complete the steps in this article on your behalf
    Request that they elevate your role on the subscription so that you will have the permissions to proceed yourself. Whether this is appropriate depends on your organization and your role within it.

2. **Create Azure Digital Twins instance**

    In this section, you will create a new instance of Azure Digital Twins using the Azure portal. Navigate to the portal and log in with your credentials.

    Once in the portal, start by selecting Create a resource in the Azure services home page menu.

    ![Azure Digital Twin](./media/ADT1.png 'AzureDigitalTwin')

    Search for Azure Digital Twins in the search box, and choose the Azure Digital Twins service from the results. Select the Create button to create a new instance of the service.

    ![Create Azure Digital Twin](./media/ADT2.jpg 'CreateAzureDigitalTwin')


    On the following Create Resource page, fill in the values given below:

    Subscription: The Azure subscription you're using
    Resource group: A resource group in which to deploy the instance. If you don't already have an existing resource group in mind, you can create one here by selecting the Create new link and entering a name for a new resource group
    Location: An Azure Digital Twins-enabled region for the deployment. For more details on regional support, visit Azure products available by region (Azure Digital Twins).
    Resource name: A name for your Azure Digital Twins instance. If your subscription has another Azure Digital Twins instance in the region that's already using the specified name, you'll be asked to pick a different name.

    ![Review+Create](./media/ADT3.jpg 'Review+Create')


    When you're finished, you can select Review + create if you don't want to configure any more settings for your instance. This will take you to a summary page, where you can review the instance details you've entered and finish with Create.

    ### Task 4: Verify success and collect important values

    After finishing your instance setup by selecting Create, you can view the status of your instance's deployment in your Azure notifications along the portal icon bar. The notification will indicate when deployment has succeeded, and you'll be able to select the Go to resource button to view your created instance.

    ![Createdinstance](./media/ADT4.jpg 'CreatedInstance')


    From the instance's Overview page, note its Name, Resource group, and Host name. These are all important values that you may need as you continue working with your Azure Digital Twins instance. If other users will be programming against the instance, you should share these values with them.

     ![Azure Digital Twin Host-name](./media/ADT5.jpg 'AzureDigitalTwinHostname')


    You now have an Azure Digital Twins instance ready to go. Next, you'll give the appropriate Azure user permissions to manage it.

    ## Set up User Access Permissions

    Assign the role
    To give a user permissions to manage an Azure Digital Twins instance, you must assign them the Azure Digital Twins Data Owner role within the instance.

    First, open the page for your Azure Digital Twins instance in the Azure portal. From the instance's menu, select Access control (IAM). Select the + Add button to add a new role assignment.

    ![Add-Role-Assignment](./media/ADT6.jpg 'AddRoleAssignment')

    On the following Add role assignment page, fill in the values (must be completed by a user with sufficient permissions in the Azure subscription):

    Role: Select Azure Digital Twins Data Owner from the dropdown menu
    Assign access to: Use User, group or service principal
    Select: Search for the name or email address of the user to assign. When you select the result, the user will show up in a Selected members section.

    ![Azure Digital Twin](./media/ADT7.jpg 'AzureDigitalTwin')


    When you're finished entering the details, hit the Save button.

    ## Verify success

    You can view the role assignment you've set up under Access control (IAM) > Role assignments. The user should show up in the list with a role of Azure Digital Twins Data Owner.

    ![Verify-access](./media/ADT8.jpg 'Verify-access')

    You now have an Azure Digital Twins instance ready to go, and have assigned permissions to manage it.





























    