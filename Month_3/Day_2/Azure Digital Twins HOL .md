# Internet of Things - Azure Digital Twins HOL

1) Azure Digital Twins, theory, deck presented avialable at **pdf files** folder.

Before Starting this Lab make sure you complete the steps specified in **Azure Digital Twins BHOL.md** File.

## Architecture Diagram ## 
Here

## **Content:** ##
- [Exercise #1: Configure Sample Application](#Exercise-1-Configure-Sample-Application)
  - [Task 1: Configure sample App](#Task-1-configure-sample-app)
  - [Task 2: Configure Digital Twin Explorer](#Task-2-configure-digital-twin-explorer)

- [Exercise #2: Process data into Azure Digital Twins](#Exercise-2-Process-data-into-Azure-Digital-Twins])
  - [Task #1: Set up sample Function App](#Task-1-Set-up-sample-Function-App)
  - [Task #2: Process telemetry data](#Task-2-Process-telemetry-data)
  
- [Exercise #3: Propagate Azure Digital Twins events through the graph](#Exercise-3-Propagate-Azure-Digital-Twins-events-through-the-graph)
  - [Task #1: Set up endpoint](#Task-1-Set-up-endpoint)
  - [Task #2: Set up route](#Task-2-Set-up-route)
  - [Task #3: Connect the function to Event Grid](#Task-3-Connect-the-function-to-Event-Grid)
- [Exercise #4: Clean up](#Exercise-4-Clean-up)


</br>


## **Exercise #1: Configure Sample Application** ##
### **Task 1: Configure sample App** ###

1. Open Azure portal and copy the hostname of your Digital Twins created throught the steps provided in 
**Azure Digital Twins BHOL** Paste the hostname in a notepad.

  ![ADT Hostname](./media/adt-hostname.png 'ADT Hostname')

2. Next, go to the Virtual Machine created during BHOL section, connect using RDP. Go to the [IoT Academy github](https://github.com/mpram/IoT_Academy) and download the code as a zip file, **Download zip**

 ![ADT Hostname](./media/download-code.png 'ADT Hostname')


Look for the zip just downloaded, unzip the material and navigate into **AdtSampleApp** Folder **(Month_3/day_2/code)**. Right click on **AdtE2ESample.sln** and open with  **in Visual Studio 2019**.

</br>

3. Signin in Visual Studio. Once you are in Visual Studio, select the **SampleClientApp > appsettings.json** file to open it in the editing window. This will serve as a pre-set JSON file with the necessary configuration variables to run the project.

</br>

  ![Edit sample app](./media/sampleapp-dt-hostname.png 'AEdit Sample app')

**Note: make sure to add  "https://" infront**


4. Save and close the file

5. In Visual Studio go to the **View Menu** select **Cloud Explorer** as shown below:

</br>

   ![VS Cloud Explorer](./media/vs-cloud-explorer.png 'VS Cloud Explorer')

</br>

In the next window you should see your Subscription or login to your Azure Account.


6. After you are login to Azure, run the **Sample Client App** selecting the buttom below:


    ![Run Sample app](./media/vs-run-sampleapp.png 'Run SampleApp')


7. A console window will open, carry out authentication, and wait for a command. In this console, run the next command to instantiate the sample Azure Digital Twins solution.

    ![cmd window](./media/cmd-window.png 'cmd window')


8. Copy and paste the following command to 
```cmd
SetupBuildingScenario
```

The output of this command is a series of confirmation messages as three digital twins are created and connected in your Azure Digital Twins instance: a floor named floor1, a room named room21, and a temperature sensor named thermostat67. These digital twins represent the entities that would exist in a real-world environment.

They are connected via relationships into the following twin graph. The twin graph represents the environment as a whole, including how the entities interact with and relate to each other.

9. Now your first graph is ready to analyze, to see your graph go to open the digital twin explorer.

</br>

### **Task 2: Configure Digital Twin Explorer** ###
</br>

1. First, download and install Azure CLI using this lin:
https://docs.microsoft.com/en-us/cli/azure/install-azure-cli-windows click on **Current release of the Azure CLI** download and install.

Then open **Windows Powershell** and enter the follow command to login to Azure, the digital twin explorer will use this credentials.

```bash
az login
```

If you are using multiple subscriptions make sure to set up he subscription properly with the following command

```bash
az account set --subscription <YOUR SUBSCRIPTION ID HERE>
```

Keep this window open we will explore our twin later here later on.

Second, Download and install **Google Chrome**

Then, Open a cmd window and cd the folder where you download and unzip the digital-twins-explorer-main.zip

```cmd
cd C:\Users\iotacademy\Downloads\digital-twins-explorer-main\digital-twins-explorer-main\client\src
```

  ![cd adt explorer](./media/cd-adt-explorer.png 'CD ADT Explorer')

Once in the **src** folder run 
```cmd
npm run start
```

**Note:** In Before HOL we asked you to install npm, if you have an error here, run **npm install** first, assuming you download and isntall node.js firts. Then go back and run **npm run start**


2. Now open Google Chrome and copy the following url:

    http://localhost:3000/

You should be able to see the app as shown below:

 ![adt explorer](./media/adt-explorer.png 'ADT Explorer')


3. Copy the adt hostname from Azure Portal, add **htts://** and paste in the Digital Twin Explorer app.

  ![adt explorer](./media/adt-instance.png 'ADT Explorer')


Your App should look like below and then click **Save**

  ![adt url](./media/adt-url.png 'ADT url')


3. Once login, run the default query clicking in **Run query** and you should see the graph just created:

![adt url](./media/twin-query.png 'ADT url')

Take a few minutes to explore the tool.

4. Go back to powershell and run and run the following commnad to see the definition of your Digital Twin.

```bash
az dt model list -n <ADT_instance_name> --definition
````

  ![CLI Twin definition](./media/cli-twin-definition.png 'CLI Twin Definition')

5. You can also use CLI to update, delete or query your twin. Run the following command to query your twin.

```bash
az dt twin query -n <ADT_instance_name> -q "SELECT * FROM DIGITALTWINS"
```

![CLI Twin query](./media/cli-querying-twin.png 'CLI Twin Query')


## **Exercise #2: Process data into Azure Digital Twins** ##

</br>

### **Task #1: Set up sample Function App** ###

</br>
The next step is setting up an Azure Functions app that will be used throughout this tutorial to process data. The function app, SampleFunctionsApp, contains two functions:

ProcessHubToDTEvents: processes incoming IoT Hub data and updates Azure Digital Twins accordingly
ProcessDTRoutedData: processes data from digital twins, and updates the parent twins in Azure Digital Twins accordingly

1. In Visual Studio, stop the app if still running then go to the Solution Explorer pane, expand **SampleFunctionsApp** > **Dependencies** then Right-select **Packages** and choose **Manage NuGet Packages...**

2. Select the tab **Updates** then click on **Select all packages** and **Update**

  ![Function App update nuggets](./media/function-app-update-nuggets.png 'update nuggets')

A new window will pop up asking for **Preview Changes** click **Ok**. Then click in **I Accept** to the license agreements.


3. Go back to the **SampleFunctionsApp** and right click and select **Publish**

  ![Function App Publish](./media/publish-function-app.png 'Publish Function App')

4. On the Publish page that opens, leave the default target selection of **Azure**. Then select **Next**.

For a specific target, choose **Azure Function App (Windows)** and then select **Next**.

In the Publish window select your subscription and click in **+** to create a new Function App

 ![Function App Create](./media/function-app-create.png 'Create Function App')

 The New window will ask you to fill the information for the new Function App
 - **Name**: Leave default value.
 - **Subscription Name**: The subscription you are using for this training.
 - **Resource Group**: The resource group you are using for this training.
 - **Plan Type**: **Consumption**
 - **Location**: The location you are using for this training
 - **Azure Storage**: click on **New** and create a new storage account for this Function App.

 Then click **Create**.

After a few minutes the new instance should appear available, click on **Finish**

  ![Function App Instance](./media/function-app-create-instance.png 'Function App Instance')

In the next window select **Publish**

  ![Function App Publish](./media/function-app-publish.png 'Function App Publish')


After a few minutes in your Output window you should see the following message:

**========== Publish: 1 succeeded, 0 failed, 0 skipped ==========**

</br>
5. The first setting gives the function app the Azure Digital Twins Data Owner role in the Azure Digital Twins instance. This role is required for any user or function that wants to perform many data plane activities on the instance.


Go back to Powershell and run the following command:

```bash
az functionapp identity show -g <YOUR RESOURCE GROUP NAME HERE> -n <YOUR FUNCTION APP NAME HERE>
```

Now copy the prinicial id of your Function App

  ![Function App Ppal ID](./media/funciton-app-ppal-id.png 'Function App ppal Id')

6. Use the principalId value in the following command to assign the function app's identity to the Azure Digital Twins Data Owner role for your Azure Digital Twins instance.

If you have access to multiple resource groups in this subscription, is better if you run this command first.

```bash
az config set defaults.group=<YOUR RESOURCE GROUP HERE>
```
Then run the following command:

```bash
az dt role-assignment create --dt-name <YOUR ADT NAME HERE> --assignee "<YOUR PRINICPAL ID HERE>" --role "Azure Digital Twins Data Owner"
```
</br>

The result of this command is outputted information about the role assignment you've created. The function app now has permissions to access data in your Azure Digital Twins instance.

 ![Function App assign role](./media/funciton-app-assign-role.png 'Function App assign role')


7. This second setting creates an environment variable for the function with the URL of your Azure Digital Twins instance. The function code will use this to refer to your instance. Run the following command:


```bash
az functionapp config appsettings set -n <YOUR FUNCTION APP NAME HERE> --settings "ADT_SERVICE_URL=https://<YOUR ADT HOSTNAME HERE>"
```

The output should look like the below image:


  ![Function App applicaion settings](./media/app-settings.png 'Function App application settings')


</br>

### **Task #2: Process telemetry data** ###

Here are the actions you will complete to set up this device connection:

- Create an IoT hub that will manage the simulated device
- Connect the IoT hub to the appropriate Azure function by setting up an event subscription
- Register the simulated device in IoT hub
- Run the simulated device and generate telemetry
- Query Azure Digital Twins to see the live results

1. Create an IoT Hub replace the suffix and assing your resource group.

```bash
az iot hub create --name hubadtlabSUFFIX -g <your-resource-group> --sku S1
```
The output of this command is information about the IoT hub that was created.

Save the name that you gave to your IoT hub. You will use it later.

2. Connect the IoT hub to the Azure function. Once the IoT Hub is created go to the Azure Portal and select the IoT Hub, then click on **Events** then **Events Subscription**


  ![IoT Hub Events](./media/iot-hub-events.png 'IoT Hub Events')

This will bring up the Create Event Subscription page.


   ![Event Subscription](./media/event-subs-telemtry.png 'Event Subscription')


Fill in the fields as follows (fields filled by default are not mentioned):

EVENT SUBSCRIPTION DETAILS
  - **Name**: Give a name to your event subscription.

TOPIC DETAILS 
  - **System Topic Name**: Give a name to use for the system topic.

EVENT TYPES 
 - Filter to Event Types: Select **Device Telemetry** from the menu options.

ENDPOINT DETAILS 
  - Endpoint Type: **Azure Function**

ENDPOINT DETAILS 
  - Endpoint: Hit the **Select an endpoint** link. This will open a Select Azure Function window:

  ![Select end point](./media/select-end-point.png 'Select end point')

 Fill in your Subscription, Resource group, Function app and Function (**ProcessHubToDTEvents**). Some of these may auto-populate after selecting the subscription.

Hit **Confirm** Selection. then click on **Create**

3. Go back to the IoT Hub and create a device for your simulator.  Select **Explorer**, then **IoT Devices** then **New**

    The form to create device will pop up fill the information:. 
    
 ![Create Device](./media/create-device.png 'Create device')

 - **Device ID**: thermostat67
 - **Authentication type**: Symetric Key
 - **Auto-generate keys**: Make sure it is checked 
 - **Connect this device to an IoT Hub**: Enable

 Then clikc **Save**

 Open again the device and copy  in a notepad the connection string:

  ![Device CS](./media/device-cs.png 'Device CS')

  We will also need the Iot Hub Connection String. Go to your IoT Hub, select **Shared Access Policies** in Settings, then **Iothub owner** in **Policy Name** copy the **Primary Connection String** also in the notepad.


  ![IoT Hub CS](./media/iot-hub-cs.png 'IoT Hub CS')

  4. In a new Visual Studio window, open  **DeviceSimulator.sln.** (from the downloaded solution folder)  in this case will be a path similar to this one:
  C:\Users\iotacademy\Downloads\digital-twins-samples-master\digital-twins-samples-master\DeviceSimulator. Right click on the **DeviceSimulator.sln.**  Open with **Visual Studio 2019**

  From the Solution Explorer pane in this new Visual Studio window, select DeviceSimulator/AzureIoTHub.cs to open it in the editing window. Change the following connection string values to the values you gathered above:
  
  </br>


  ![device simualtor](./media/device-simulator.png 'Device Simulator')

  **Save** the file, Ctrl+S

Now, to see the results of the data simulation that you've set up, run the DeviceSimulator project with the green play button in the toolbar **Start**

A console window will open and display simulated temperature telemetry messages. These are being sent to IoT Hub, where they are then picked up and processed by the Azure function.

  ![device simualtor](./media/device-simulator-telemetry.png 'Device Simulator')



You don't need to do anything else in this console, but leave it running while you complete the next steps.


5. To see the data from the Azure Digital Twins side, go to your Visual Studio window where the **AdtE2ESample** project is open and run the project.

In the project console window that opens, run the following command to get the temperatures being reported by the digital twin thermostat67:

```cmd
ObserveProperties thermostat67 Temperature
```
You should see the data flowinf through the digital twin

  ![ADT Data](./media/adt-data.png 'ADT Data')

  Once you've verified this is working successfully, you can stop running both projects. Keep the Visual Studio windows open, as you'll continue using them in the rest of the tutorial.

  </br>

  ## Exercise #3:Propagate Azure Digital Twins events through the graph ##

Here are the actions you will complete to set up this data flow:

- Create an Event Grid endpoint in Azure Digital Twins that connects the instance to Event Grid.

- Set up a route within Azure Digital Twins to send twin property change events to the endpoint.

- Deploy an Azure Functions app that listens (through Event Grid) on the endpoint, and updates other twins accordingly.

- Run the simulated device and query Azure Digital Twins to see the live results

 
### **Task #1: Set up endpoint** ###
  
 In this section, you create an event grid topic, and then create an endpoint within Azure Digital Twins that points (sends events) to that topic.

1. Go back to the powershell window and run the following command replacing **SUFFIX** and your **Resource Group**

```bash
az eventgrid topic create -g <YOUR RESOURCE GROUP HERE> --name eventgridtopicadtSUFFIX -l eastus
```
  ![ADT Data](./media/create-event-grid-topic.png 'ADT Data')


2. Next, create an Event Grid endpoint in Azure Digital Twins, which will connect your instance to your event grid topic. Use the command below, filling in the placeholder fields as necessary:

```bash
az dt endpoint create eventgrid --dt-name <your-Azure-Digital-Twins-instance> --eventgrid-resource-group <your-resource-group> --eventgrid-topic <your-event-grid-topic> --endpoint-name <name-for-your-Azure-Digital-Twins-endpoint>
```

![Endpoint create](./media/event-grid-end-point.png 'Event Grid endpoint Create')

The output from this command is information about the endpoint you've created.

Look for the provisioningState field in the output, and check that the value is "Succeeded". It may also say "Provisioning", meaning that the endpoint is still being created. 
In this case, wait a few seconds and run the command again to check that it has completed successfully before continuing next steps to create routes using this endpoint.

```bash
az dt endpoint show --dt-name <your-Azure-Digital-Twins-instance> --endpoint-name <your-Azure-Digital-Twins-endpoint>
```


Save the names that you gave to your event grid topic and your Event Grid endpoint in Azure Digital Twins. You will use them later.

</br>

### **Task #2: Set up route** ###

Next, create an Azure Digital Twins route that sends events to the Event Grid endpoint you just created.

1. Run the following command to create a route, be carefull to replace all the values based on your previous steps.

```bash
az dt route create --dt-name <your-Azure-Digital-Twins-instance> --endpoint-name <your-Azure-Digital-Twins-endpoint> --route-name <name-for-your-Azure-Digital-Twins-route>
```
The output from this command is some information about the route you've created.

![Create Route](./media/create-route.png 'Create Route')

### **Task #3: Connect the function to Event Grid** ###


Next, subscribe the ProcessDTRoutedData Azure function to the event grid topic you created earlier, so that telemetry data can flow from the thermostat67 twin through the event grid topic to the function, which goes back into Azure Digital Twins and updates the room21 twin accordingly.

To do this, you'll create an Event Grid subscription that sends data from the event grid topic that you created earlier to your ProcessDTRoutedData Azure function.

1. Go to Azure Portal, navigate to the Event grid Topic created before, then click **+ Event Subscription**

![Create Event Subscription](./media/event-grid-topic-portal.png 'Create Event Subscription')

A new form wil open fill the values as shown below:

![Create Event Form](./media/form-events.png 'Create Event Form')

Assign a name to the event, make sure you select **Azure Function** as endpoint and then select the endpoint **ProcessDTRoutedData** from your Azure Function. Then click **Create**

2. Run the simulation and see the results
Now you can run the device simulator to kick off the new event flow you've set up. Go to your Visual Studio window where the **DeviceSimulator** project is open, and run the project.

Like when you ran the device simulator earlier, a console window will open and display simulated temperature telemetry messages. These events are going through the flow you set up earlier to update the thermostat67 twin, and then going through the flow you set up recently to update the room21 twin to match.


![Routing Messages](./media/message-received.png 'Routing Messages')

</br>

To see the data from the Azure Digital Twins side, go to your Visual Studio window where the AdtE2ESample project is open, and run the project.

In the project console window that opens, run the following command to get the temperatures being reported by both the digital twin thermostat67 and the digital twin room21.

```bash
ObserveProperties thermostat67 Temperature room21 Temperature
```

You should see the live updated temperatures from your Azure Digital Twins instance being logged to the console every two seconds. Notice that the temperature for room21 is being updated to match the updates to thermostat67.

</br>

![Routing Messages](./media/routing-messages.png 'Routing Messages')


## Exercise #4: Clean up ##

After completing all the exercises go to the Azure Portal and you can delete the resource group, click in **Delete resource group**

![Routing Messages](./media/delete-resource-group.png 'Routing Messages')

