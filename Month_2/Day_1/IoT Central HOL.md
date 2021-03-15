# IoT Central - Hands-on Lab #


**Scenario**

Suppose you run a company that operates a fleet of refrigerated trucks. You have many customers within a city, and you operate from a base. You command each truck to deliver its contents to a customer.

If the cooling system fails on a truck and the contents start to melt, you need to instruct the truck to return to base and unload the contents. Or you can instead deliver the contents to a customer who's nearby when the cooling system fails.

To make these decisions, you need an up-to-date picture of all that happens with your trucks. You need to know the location of each truck on a map, the status of the cooling system, and the status of the contents.

IoT Central provides all you need to handle this scenario. In the following image, for example, the colored circles show the location of a truck on its way to a customer.

**Final Application**

Prtscrn here

**Architecture Diagram**

image here 

**IoT Central Theory**

The deck presented in this section it is available in the pdf files folder.

## **Content - Hands-on Lab:** ##
- [Exercise 1: Create a Custom IoT Central app](#exercise-1-create-a-custom-iot-central-app)
  - [Task 1: Creating an Application](#task-1-creating-an-application)
   - [Task 2: Add Capabilities - Telemetry](#task-2-add-capabilities---telemetry)
    - [Task 3: Add Capabilities - Properties](#task-3-Add-capabilities---properties)
     - [Task 4: Add Capabilities - Commands](#task-4-add-capabilities---commands)
    - [Task 5: Creating an Application](#task-5-creating-an---application)
    - [Task 6: Creating an Application](#task-6-creating-an---application)
- [Exercise 2: Create a Dashboard](#exercise-2-create-a-dashboard)
    - [Task 1: Visualizing the device](#task-1-Visualizing-the-device)
    - [Task 2: Writable Properties View](#task-2-Writable-Properties-View)
    - [Task 3: Create a Device](#task-3-Create-a-Device)

- [Exercise 3:  Azure Maps](#exercise-3-Azure-Maps)
- [Exercise 4: Create the device app](#exercise-4-Create-the-device-app)
    - [Task 1: Set up your environment](#task-1-Set-up-your-environment)
    - [Task 2: Launch your device](#task-2-Launch-your-device)
    - [Task 3: Set up Properties](#task-3-Set-up-Properties)
- [Exercise 5: Create Rules](#exercise-5-Create-Rules)
    - [Task 1: Cooling system state](#Task-1-Cooling-system-state)
    - [Task 2: Temperature spiking](#Task-2-Temperature-spiking)
    - [Task 3: Truck leaves base](#Task-3-Truck-leaves-base)
    - [Task 4: Temperature of the contents](#Task-4-Temperature-of-the-contents)

- [Exercise 6: Clean up](#exercise-6-clean-up)


## **Exercise 1: Create a Custom IoT Central app** ##

### **Task 1: Creating an Application** ###

In the browser open Azure IoT Central: https://apps.azureiotcentral.com/

 ![Create a Custom App](./media/iotc-custom-app.png 'Create a Custom App')

After selecting Custom apps, you should fill the fields in the Application Form:

 ![Create a Custom App](./media/iotc-new-app.png 'Create a Custom App')

- **Application Name:** Refrigerated Trucks

- **URL:** refrigerated-trucks-SUFFIX must be a unique URL
- **Application Template:** Custom application, default.

- **Pricing Plan:** Standard 1

- **Directory:** Your current company

- **Azure Subscription:** Your current subscription

- **Location** Select the region you are using for this training. 

Then select **Create**

Once your Application is available the next step will be to **Create a device template**. On your left menu click on **Device Templates** and then in **New**

 ![Create a new device template](./media/iotc-device-template.png 'Create new template')

<br> 

 1. Select **IoT Device** then **Next: Customize** 
 2. In the customize screen assign a **Device Template name** RefrigeratedTruck
 3. Don't select **Gateway device** box
 4. Select **Next: Review**. Then select **Create**.
 5. In the Create a model area, select Custom model. Your view should now look similar to the following image.
 
 <br> 


 ![Create a new Model](./media/iotc-create-model.png 'Create new Model')

6. Select **Add an inherited interface**. 
7. Then select **Custom** to start building from a blank interface

<br> 


### **Task 2: Add Capabilities - Telemetry** ###

1. To get started, select **Add capability**. Then enter the values in the following table.



  
  <table>
    <thead>
      <tr>
        <th>Entry Summary</th>
        <th>Value</th>
       </tr>
    </thead>
    <tbody>
        <tr>
            <td>Display Name</td>
            <td>Contents temperature</td>
          </tr>
          <tr>
            <td>Name</code></td>
            <td>ContentsTemperature</td>
        </tr>
        <tr>
            <td>Capability type</td>
            <td>Telemetry</td>
        </tr>
            <td>Semantic type</td>
            <td>Temperature</td>
        </tr>
         </tr>
            <td>Schema</td>
            <td>Double</td>
        </tr>
        </tr>
            <td>Unit</td>
            <td>C</td>
        </tr>
    </tbody>
</table>

  
<br> 
<br> 

Your window should now look like the following image:
 

![Add Capability](./media/iotc-add-capability.png 'Add Capability')


**Note**: The interface names must be entered exactly as shown in this unit. The names and entries must exactly match in the code you'll add later in this module.


2. States are important. They let the operator know what's happening. A state in IoT Central is a name associated with a range of values. Later you'll choose a color to associate with each value.

Use the **Add capability** control to add a state for the truck's refrigerated contents: **empty**, **full**, or **melting**.

 <table>
    <thead>
      <tr>
        <th>Entry Summary</th>
        <th>Value</th>
       </tr>
    </thead>
    <tbody>
        <tr>
            <td>Display Name</td>
            <td>Contents state</td>
          </tr>
          <tr>
            <td>Name</code></td>
            <td>ContentsState</td>
        </tr>
        <tr>
            <td>Capability type</td>
            <td>Telemetry</td>
        </tr>
            <td>Semantic type</td>
            <td>State</td>
        </tr>
         </tr>
            <td>Schema</td>
            <td>String</td>
        </tr>
    </tbody>
</table>
<br> 

Select **Add**. For Display name and Value, enter empty. The Name field should be populated automatically with empty. So all three fields are identical, containing **empty**. Add two more state values: **full** and **melting**. Again, the same text should appear in the fields for Display name, Name, and Value.

![Add Capability](./media/content-state.png 'Add Capability')

<br> 

3. If the cooling system fails, as you'll see in the following units, the chances of the contents melting increase considerably. 


 <table>
    <thead>
      <tr>
        <th>Entry Summary</th>
        <th>Value</th>
       </tr>
    </thead>
    <tbody>
        <tr>
            <td>Display Name</td>
            <td>Cooling system state</td>
          </tr>
          <tr>
            <td>Name</code></td>
            <td>CoolingSystemState</td>
        </tr>
        <tr>
            <td>Capability type</td>
            <td>Telemetry</td>
        </tr>
            <td>Semantic type</td>
            <td>State</td>
        </tr>
         </tr>
            <td>Value schema</td>
            <td>String</td>
        </tr>
    </tbody>
</table>

<br> 

Add **on**, **off**, and **failed** entries for the cooling system. Start by selecting Add capability. Then add another state:

![Cooling System State](./media/cooling-system.png 'Cooling System states')

4. A more complex state is the state of the truck itself. If all goes well, a truck's normal routing might be ready, enroute, delivering, returning, loading, and back to ready again. Also add the dumping state to account for the disposal of melted contents! To create the new state, use the same process as for the last two steps.

 <table>
    <thead>
      <tr>
        <th>Entry Summary</th>
        <th>Value</th>
       </tr>
    </thead>
    <tbody>
        <tr>
            <td>Display Name</td>
            <td>Truck state</td>
          </tr>
          <tr>
            <td>Name</code></td>
            <td>TruckState</td>
        </tr>
        <tr>
            <td>Capability type</td>
            <td>Telemetry</td>
        </tr>
            <td>Semantic type</td>
            <td>State</td>
        </tr>
         </tr>
            <td>Value schema</td>
            <td>String</td>
        </tr>
    </tbody>
</table>

<br> 

Now add: **ready**, **enroute**, **delivering**, **returning**, **loading**, and **dumping** as shown below: 

<br> 


![Truck State](./media/truck-state.png 'Truck states')

<br> 

5. Add and Event Capability. One event a device might trigger is a conflicting command. An example might be when an empty truck that's returning from a customer receives a command to deliver its contents to another customer. If a conflict occurs, the device should trigger an event to warn the operator of the IoT Central app.

Another event might just acknowledge and record the customer ID that a truck is to deliver to.

To create an event, select **Add capability**. Then fill in the following information.

<table>
    <thead>
      <tr>
        <th>Entry Summary</th>
        <th>Value</th>
       </tr>
    </thead>
    <tbody>
        <tr>
            <td>Display Name</td>
            <td>Event</td>
          </tr>
          <tr>
            <td>Name</code></td>
            <td>Event</td>
        </tr>
        <tr>
            <td>Capability type</td>
            <td>Telemetry</td>
        </tr>
            <td>Semantic type</td>
            <td>Event</td>
        </tr>
         </tr>
            <td>Schema</td>
            <td>String</td>
        </tr>
          </tr>
            <td>Severity</td>
            <td>Information</td>
        </tr>
    </tbody>
</table>
<br> 

Your settings should look like the image below: 

![Truck State](./media/event.png 'Truck states')

<br> 
6. Add a Location capability following the below information:
<br> 
<br> 


<table>
    <thead>
      <tr>
        <th>Entry Summary</th>
        <th>Value</th>
       </tr>
    </thead>
    <tbody>
        <tr>
            <td>Display Name</td>
            <td>Location</td>
          </tr>
          <tr>
            <td>Name</code></td>
            <td>Location</td>
        </tr>
        <tr>
            <td>Capability type</td>
            <td>Telemetry</td>
        </tr>
            <td>Semantic type</td>
            <td>Location</td>
        </tr>
         </tr>
            <td>Schema</td>
            <td>Geopoint</td>
        </tr>
    </tbody>
</table>
<br> 


### **Task 3: Add Capabilities - Properties** ###
You'll define an optimal temperature for the truck contents as a property.
1. Select Add capability. Then add the truck ID property.
<br> 
<br> 

<table>
    <thead>
      <tr>
        <th>Entry Summary</th>
        <th>Value</th>
       </tr>
    </thead>
    <tbody>
        <tr>
            <td>Display Name</td>
            <td>Truck ID</td>
          </tr>
          <tr>
            <td>Name</code></td>
            <td>TruckID</td>
        </tr>
        <tr>
            <td>Capability type</td>
            <td>Property</td>
        </tr>
            <td>Semantic type</td>
            <td>None</td>
        </tr>
         </tr>
            <td>Schema</td>
            <td>String</td>
        </tr>
          </tr>
            <td>Writable</td>
            <td>Off</td>
        </tr>
         </tr>
          </tr>
            <td>Unit</td>
            <td>None</td>
        </tr>
    </tbody>
</table>
<br> 

You should see your property set up as this one below:

![Truck State](./media/truckid-property.png 'Truck states')

2. Add the optimal temperature property.


<table>
    <thead>
      <tr>
        <th>Entry Summary</th>
        <th>Value</th>
       </tr>
    </thead>
    <tbody>
        <tr>
            <td>Display Name</td>
            <td>Optimal Temperature</td>
          </tr>
          <tr>
            <td>Name</code></td>
            <td>OptimalTemperature</td>
        </tr>
        <tr>
            <td>Capability type</td>
            <td>Property</td>
        </tr>
            <td>Semantic type</td>
            <td>Temperature</td>
        </tr>
         </tr>
            <td>Schema</td>
            <td>Double</td>
        </tr>
          </tr>
            <td>Writable</td>
            <td>On</td>
        </tr>
         </tr>
          </tr>
            <td>Unit</td>
            <td>C</td>
        </tr>
    </tbody>
</table>
<br> 

Now, should look like the below image:

![Truck State](./media/optimal-temp.png 'Truck states')


### **Task 4: Add Capabilities - Commands** ###
For refrigerated trucks, you should add two commands:

A command to deliver the contents to a customer
A command to recall the truck to base

1. To add the commands, select **Add capability**. Then add the first command.

    <table>
        <thead>
        <tr>
            <th>Entry Summary</th>
            <th>Value</th>
        </tr>
        </thead>
        <tbody>
            <tr>
                <td>Display Name</td>
                <td>Go to customer</td>
            </tr>
            <tr>
                <td>Name</code></td>
                <td>GoToCustomer</td>
            </tr>
            <tr>
                <td>Capability type</td>
                <td>Command</td>
            </tr>
        </tbody>
    </table>

<br> 

Turn on the **Request** option to enter more command details.
<br> 

<table>
                <thead>
                <tr>
                    <th>Entry Summary</th>
                    <th>Value</th>
                </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>Request</td>
                        <td>On</td>
                    </tr>
                    <tr>
                        <td>Display name</code></td>
                        <td>Customer ID</td>
                    </tr>
                    <tr>
                        <td>Name</td>
                        <td>CustomerID</td>
                    </tr>
                    <tr>
                        <td>Schema</td>
                        <td>Integer</td>
                    </tr>
                </tbody>
</table>

<br> 
Validate your inputs with the below image: 

![Command Go to Customer](./media/command-go-to-customer.png 'Command Go to Customer')

<br> 

2. Create a command to recall the truck.

    <table>
            <thead>
            <tr>
                <th>Entry Summary</th>
                <th>Value</th>
            </tr>
            </thead>
            <tbody>
                <tr>
                    <td>Display Name</td>
                    <td>Recall</td>
                </tr>
                <tr>
                    <td>Name</code></td>
                    <td>Recall</td>
                </tr>
                <tr>
                    <td>Capability type</td>
                    <td>Command</td>
                </tr>
            </tbody>
</table>

<br> 

Your recall property should look like the below one:

![Command Recall](./media/command-recall.png 'Command Recall')

<br> 

3. Select **Save**. Before you go any further, carefully double-check your interface. After an interface is published, editing options are limited. So you should get it right before publishing.

When you select the name of the device template, the menu that ends with the Views option summarizes the capabilities, 6 Telemetry based, 2 Properties and 2 Commands: 

![Command Recall](./media/capabilities-all.png 'Command Recall')

4. Select **Publish**. Then in the dialog box, select **Publish** again. The annotation should change from Draft to Published.

## Exercise 2: Create a Dashboard ## 

### **Task 1: Visualizing the device** ### 

1. Select **Views**. Then select **Visualizing the device**.
You see a list of all the Telemetry, Property, and Commands elements you created, each with a check box. You also see a list of Cloud properties and Custom tiles. Ignore these two lists for now.


  ![Command Recall](./media/dashboard-view.png 'Command Recall')




2. Under **Telemetry**, select **Location** > **Add tile**. Dashboards are made of tiles. We choose the location tile first because we want to expand it.

3. Change the View name to something more specific, for example, **Truck view**

4. Select each of the rest of the telemetry and property capabilities in turn, starting at the top. For each capability, select **Add tile**

Your new Dashboard should look like this one:

  ![Command Recall](./media/dashboard-device.png 'Command Recall')

5. Select **Save** to save this view.


### **Task 2: Writable Properties View** ### 

We need to create a separate view. Its sole purpose will be to set writable properties.

1. Select **Views**, and then select the **Editing device and cloud data tile**

2. Change the form name to something like **Set properties**.

3. Select the **Optimal temperature** property check box. Then select **Add section**.

4. Verify that your view looks similar to the following image. Then select **Save**.

  ![Command Recall](./media/writeable-form.png 'Command Recall')


### **Task 3: Create a Device** ### 

1. On the menu on the left, select **Devices**.

2. To ensure the new device uses this device template, in the Devices menu, select **RefrigeratedTruck**.

3. Select **New**. In the Create a new device dialog box, verify that the device template is **RefrigeratedTruck**.


    - **Device name**: RefrigeratedTruck - 1

    - **Device ID**: RefrigeratedTruck1

    - **Simulate this device?**: setting at No


  ![Command Recall](./media/new-device.png 'Command Recall')

4. Then click **Create**


Notice that the Device status is **Registered**. Only after the device status is **Provisioned** will the IoT Central app accept a connection to the device. The coding unit that follows shows how to provision a device.

5. Select RefrigeratedTruck -1. You see the live dashboard. It includes lots of Waiting for data messages. On the bar that includes Truck view, select Commands. Notice that the two commands you entered are ready to run.

6. Record the connection keys. In the upper-right menu, select **Connect**. Do not select **Attach to gateway**.

In the Device connection dialog box that opens, carefully copy the **ID scope**, **Device ID**, and P**rimary key**. The ID scope identifies the app. The device ID identifies the real device. And the primary key gives you permission for the connection.

Paste this information in a text file. 

Leave the Authentication type setting as **Shared access signature (SAS)**.

After you save the IDs and the key, select Close on the dialog box.

## Exercise 3: Azure Maps ## 

1. Go to Azure Portal: https://ms.portal.azure.com/
2. Select **Create a Resource**, in the marketplace look for **Azure Maps**, select Azure Maps and then click **create**

    ![Command Recall](./media/azure-maps.png 'Command Recall')

Complete the creation form: 
- **Subscription**: Select the subscription you are using for this training.
- **Resource Group**: Select the resource group you are using for this training.
-**Name**: mytrucksacademySUFFIX
-**Pricing Tier**: Standard S1
-**Confirm the license and Privacy terms** make sure it is check.

Then click **Create** at the bottom of the page.

   ![Command Recall](./media/azure-maps-form.png 'Command Recall')

Once Azure Maps resource is created, find the key by selecting **Authentication**. Copy the **primary key** and paste it into your notepad. 


## Exercise 4: Create the device app ## 

### **Task 1: Set up your environment** ###

1. Open Visual Studio Code locally

2. On the top bar select **Terminal** and then **New Terminal** in Visual Studio Code. 

3. Create a folder called RefrigeratedTruck by entering **mkdir RefrigeratedTruck** and then enter. Go to the folder by entering **cd RefrigeratedTruck**.

  ![Command Recall](./media/vs-code-mkdir.png 'Command Recall')



4. Enter **dotnet new console**. This command creates a Program.cs file and a project file in your folder.

5. Enter **dotnet restore**. This command gives your app access to the required .NET packages.

Install the required libraries, copy and paste the below code in the terminal.

```bash 

dotnet add package AzureMapsRestToolkit
dotnet add package Microsoft.Azure.Devices.Client
dotnet add package Microsoft.Azure.Devices.Provisioning.Client
dotnet add package Microsoft.Azure.Devices.Provisioning.Transport.Mqtt
dotnet add package System.Text.Json
```

6. From the File menu, open the Program.cs file just created. Then replace the whole content copying and pasting from the file **Program.cs** you will find in  
**code_sample** folder.

7. Once you replace the content of the files, we need to add our keys to connect with our services. Look for lines **123** to **126**. Replace accordingly based on the keys you were adding to your notepad in previous exercises. 


 ![Command Recall](./media/vscode-replace-keys.png 'Command Recall')

After the changes are made, save the file with **Ctrl+S**
### **Task 2: Launch your device** ### 

To begin testing, open the Azure IoT Central app in a browser. Then run the device app.
1. In the terminal, enter **dotnet run**.

A console screen opens with the message Starting Truck number 1.

 ![Command Recall](./media/register-device.png 'Command Recall')


Once your device in registered through VS Code, you should see in your IoT Central an status change to **Provisioned**:


 ![Command Recall](./media/device-provisioned.png 'Command Recall')

 At this point in the Track View dashboard you should see data flowing thorught it, the map should show a blue dot with your truck and the chart receiving telemetry data should show some data points already.

 2. Select the device's **Commands** tab. This control should be under the truck name, to the right of the Truck view control.

3. Enter a customer ID, say **1**. (Numerals 0 through 9 are valid customer IDs.) Then select **Run**.

In the console for the device app, you see both a New customer event and a Route found message



 ![Command Recall](./media/new-command.png 'Command Recall')


4. On the dashboard's Location tile, check to see whether your truck is on its way. You might have to wait a short time for the apps to sync.

5. Verify the event text on the Event tile. You should see a new Customer Event.

6. When the truck returns to base and is reloaded with contents, its state is ready. Try issuing another delivery command. Choose another customer ID.

7. Before the truck reaches the customer, make a recall command to check whether the truck responds.


### **Task 3: Set up Properties** ### 


The next test is to check the writable property, **OptimalTemperature**. To change this value, select the **Set properties** view.

Set the optimal temperature to any value, say **-8**. Select **Save** and then notice the Pending status.

 ![Command Recall](./media/set-property.png 'Command Recall')

Now you should see the new Optimal temperature is set to -8. in the **Optimal Temperature** Tile.


## Exercise 5: Create Rules ## 

### **Task 1: Cooling system state** ###

1. In the IoT portal, select **Rules** in the left-hand menu, then **+ New**. Enter a meaningful name for the rule, such as **"Cooling system failed"**. Press Enter.

 ![Rules](./media/new-rule.png 'New Rule')



 2. Select **RefrigeratedTruck** for the **device template**.

3. Under **Conditions** notice that all the telemetry elements of the device template are available. Select **Cooling system state**.

For Operator, select **Equals**.

For value, type the word **"failed"**, then click on Select: "failed".

Leave Time aggregation as Off.

For **Actions**, click on **+ Email**.

In Display name, enter a title for the email, perhaps "Cooling system failed"!

For To, enter the email you've used for your IoT Central account. And for Note enter some descriptive text that will form the body of the email.

**Note**: To receive emails the account you select has to be login to IoT central at least one time, otherwise you will not receive any emails.

Your new rule should look like the below image.

 ![Rules](./media/rule-cooling-system.png 'New Rule Cooling System')


### **Task 2: Temperature spiking** ###

1. Create a new rule with a name such as **"Contents temperature spiking"**.

2. Turn on **Time aggregation**, and select an interval of **5 minutes**.

3. Select **Contents Temperature** for Telemetry.

4. In the range Aggregation values, select **Maximum.**

5. For Operator. select Is greater than or equal to. Then enter **"0"** for the value, and select that as the value.

6. For Actions, fire off another email. Give the email an appropriate title and note.

7. Make sure to click Save, to save off this rule.


 ![Rules Temp](./media/temp-spiking.png 'New Rule Temp System')

### **Task 3: Truck leaves base** ###

1. Select **Rules** in the left-hand menu, then **+ New**. Enter a meaningful name for the rule, such as **"Truck leaving base"**. Press Enter.

Now, enter the following five conditions.
- Location / Latitude: doesn't equal => **47.644702**
- Location / Longitude: doesn't equal => **-122.130137**
- Truck state: Equals => **enroute**

### **Task 4: Temperature of the contents** ###

1. Enter a rule with a name such as **"Truck contents OK"**.

2. Turn on Time aggregation, with a period of **five minutes**.

3. Enter conditions that fire if the average Contents Temperature is less than **-1** degrees Celsius, and greater than **-18** degrees Celsius.

4. Again, enter an appropriate **email action**, and click **Save**.

At this point you should see all the rules listed as below:

 ![Rules Temp](./media/rules-all.png 'New Rule Temp System')


At this point it is time to test your Rules Go to your Device Dashboard, sent a Command to trigger a new Customer trip, remember use numbers from 1 to 9.
In a few minutes you should start receiving emails.

## Exercise 6: Clean up ## 

Once you completed all the exercises, go to Azure Portal, look for the azure IoT Central Application and delete resource.

**Resources Needed**
- Azure IoT Central
- VS Code