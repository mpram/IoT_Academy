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
- [Exercise 2: Create a Dashboard](#exercise-2)
- [Exercise 3: Create a Device](#exercise-3-)
- [Exercise 4: Set up Azure Maps](#exercise-4)
- [Exercise 5: Visual Studio Code](#exercise-5)
- [Exercise 6: Set up Alerts](#exercise-6)
- [Exercise 7: Export Data](#exercise-7)

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

![Truck State](./media/event.png 'Truck states')

<br> 
6. Add a Location capability following the below information:


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


**Resources Needed**
- Azure IoT Central
- VS Code


