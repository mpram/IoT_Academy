# Internet of Things - Azure Digital Twins HOL

1) Azure Digital Twins, theory, deck presented avialable at **pdf files** folder.

Before Starting this Lab make sure you complete the steps specified in **Azure Digital Twins BHOL.md** File.

## Architecture Diagram ## 
Here

## **Content:** ##
- [Exercise #1: Configure Sample Application](#Exercise-1-Configure-Sample-Application)


Download zip with code.


Open VM.
Click Yes, in the Network sign allowing your Virtual Machine to be discoverable by other devices in your network.


### **Exercise #1: Configure Sample Application** ###

1. Open Azure portal and copy the hostname of your Digital Twins created throught the steps provided in 
**Azure Digital Twins BHOL** Paste the hostname in a notepad.

  ![ADT Hostname](./media/adt-hostname.png 'ADT Hostname')

2. Next, go to Virtual Machine look for the zip just downloaded, unzip the material and navigate into **AdtSampleApp** Folder. Right clieck on **AdtE2ESample.sln** and open with  **in Visual Studio 2019**.

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

