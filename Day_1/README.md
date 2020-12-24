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

Change to Bash access

   ![Screenshot of Bash access.](./media/bash.jpg 'Access Bash Link')

Once you are login run the following command to create an IoT Hub.

 ```csharp  
az iot hub create --name {your iot hub name} \
   --resource-group {your resource group name} --sku S1

```
Verify your IoT Hub has been created in the Portal. 

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

## Target audience

- Application Developer
- IoT Developer
- This is Manisha

## Abstracts

### Workshop

This workshop will guide you through an implementation of an end-to-end IoT solution simulating high velocity data emitted from smart meters and analyzed in Azure. You will design a lambda architecture, filtering a subset of the telemetry data for real-time visualization on the hot path, and storing all the data in long-term storage for the cold path.

At the end of this workshop, you will be better able to construct an IoT solution implementing device registration with the IoT Hub Device Provisioning Service and visualizing hot data with Power BI.

### Hands-on lab

In this hands-on lab, you will construct an end-to-end IoT solution simulating high velocity data emitted from smart meters and analyzed in Azure. You will design a lambda architecture, filtering a subset of the telemetry data for real-time visualization on the hot path, and storing all the data in long-term storage for the cold path.

At the end of this hands-on lab, you will be better able to build an IoT solution implementing device registration with the IoT Hub Device Provisioning Service and visualizing hot data with Power BI.

## Azure services and related products

- Azure App Services
- Azure Blob Storage
- Azure Data Factory
- Azure Databricks
- Azure SQL Database
- Azure Stream Analytics
- IoT Hub
- Power BI Desktop
- Visual Studio

## Azure solution

Internet of Things
