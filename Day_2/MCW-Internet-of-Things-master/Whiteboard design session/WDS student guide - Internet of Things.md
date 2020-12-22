![Microsoft Cloud Workshops](https://github.com/Microsoft/MCW-Template-Cloud-Workshop/raw/master/Media/ms-cloud-workshop.png 'Microsoft Cloud Workshops')

<div class="MCWHeader1">
Internet of Things
</div>

<div class="MCWHeader2">
Whiteboard design session student guide
</div>

<div class="MCWHeader3">
June 2020
</div>

Information in this document, including URL and other Internet Web site references, is subject to change without notice. Unless otherwise noted, the example companies, organizations, products, domain names, e-mail addresses, logos, people, places, and events depicted herein are fictitious, and no association with any real company, organization, product, domain name, e-mail address, logo, person, place or event is intended or should be inferred. Complying with all applicable copyright laws is the responsibility of the user. Without limiting the rights under copyright, no part of this document may be reproduced, stored in or introduced into a retrieval system, or transmitted in any form or by any means (electronic, mechanical, photocopying, recording, or otherwise), or for any purpose, without the express written permission of Microsoft Corporation.

Microsoft may have patents, patent applications, trademarks, copyrights, or other intellectual property rights covering subject matter in this document. Except as expressly provided in any written license agreement from Microsoft, the furnishing of this document does not give you any license to these patents, trademarks, copyrights, or other intellectual property.

The names of manufacturers, products, or URLs are provided for informational purposes only and Microsoft makes no representations and warranties, either expressed, implied, or statutory, regarding these manufacturers or the use of the products with any Microsoft technologies. The inclusion of a manufacturer or product does not imply endorsement of Microsoft of the manufacturer or product. Links may be provided to third party sites. Such sites are not under the control of Microsoft and Microsoft is not responsible for the contents of any linked site or any link contained in a linked site, or any changes or updates to such sites. Microsoft is not responsible for webcasting or any other form of transmission received from any linked site. Microsoft is providing these links to you only as a convenience, and the inclusion of any link does not imply endorsement of Microsoft of the site or the products contained therein.

© 2020 Microsoft Corporation. All rights reserved.

Microsoft and the trademarks listed at <https://www.microsoft.com/legal/intellectualproperty/Trademarks/Usage/General.aspx> are trademarks of the Microsoft group of companies. All other trademarks are property of their respective owners.

**Contents**

- [Internet of Things whiteboard design session student guide](#internet-of-things-whiteboard-design-session-student-guide)
  - [Abstract and learning objectives](#abstract-and-learning-objectives)
  - [Step 1: Review the customer case study](#step-1-review-the-customer-case-study)
    - [Customer situation](#customer-situation)
    - [Customer needs](#customer-needs)
    - [Customer objections](#customer-objections)
    - [Infographic of common scenarios](#infographic-of-common-scenarios)
  - [Step 2: Design a proof of concept solution](#step-2-design-a-proof-of-concept-solution)
  - [Step 3: Present the solution](#step-3-present-the-solution)
  - [Wrap-up](#wrap-up)
  - [Additional references](#additional-references)

# Internet of Things whiteboard design session student guide

## Abstract and learning objectives

In this whiteboard design session, you will work with a group to design an implementation of an end-to-end IoT solution simulating high velocity data emitted from smart meters and analyzed in Azure. You will design a lambda architecture, filtering a subset of the telemetry data for real-time visualization on the hot path, and storing all the data in long-term storage for the cold path.

At the end of this whiteboard design session, you will be better able to design an IoT solution implementing device registration with the IoT Hub Device Provisioning Service and visualizing hot data with Power BI.

## Step 1: Review the customer case study

**Outcome**

Analyze your customer's needs.

Timeframe: 15 minutes

Directions: With all participants in the session, the facilitator/SME presents an overview of the customer case study along with technical tips.

1. Meet your table participants and trainer.

2. Read all of the directions for steps 1-3 in the student guide.

3. As a table team, review the following customer case study.

### Customer situation

Fabrikam provides services and smart meters for enterprise energy (electrical power) management. Their **You-Left-The-Light-On** service enables the enterprise to understand their energy consumption, not just as monthly totals, but also with a more detailed breakdown providing kilowatt-hours consumed by day. These reports are used by the enterprise to identify operational patterns and address spikes that could be mitigated with better procedures.

Today, Fabrikam provides this information as printed reports that are mailed to customer enterprises monthly. However, regulations are changing and are encouraging Fabrikam to upgrade their offering and increase their market share.

The power company, City Power & Light (CPL), has an offering for enterprises that participate in their Smart Energy program. The program provides significantly discounted rates on electricity for enterprises that are managing their electricity consumption, with awards and rebates when customers demonstrate energy savings across various areas of the enterprise including reducing consumption during peak utility hours and improving efficiencies of energy used.

The requirements for meeting the criteria of the Smart Energy program center on the collection and analysis of detailed energy consumption data. In short, telemetry data must be collected in at least 30-minute intervals; the data needs to include a timestamp, the identifier of the device must be metered, and the energy consumed over the reporting interval. These devices report their telemetry to one of a few energy management solution providers authorized by City Power & Light. These providers store, process, and tag the telemetry data on behalf of City Power & Light and provide reporting Application Programming Interfaces (APIs) that CPL can use to calculate awards and rebates.

Fabrikam would like to become an authorized energy management solution provider. Today, they collect all telemetry data within on-premises servers and all telemetry data for the enterprises they monitor is stored as flat files on their Storage Area Network (SAN). They only keep the current quarter's worth of telemetry data loaded in their on-premises operational data store (running SQL Server 2012).

According to their Director of Analytics, Sam George, "We are investigating a move to the cloud to help our customers not only to meet CPL's data collection and reporting requirements, but also become the number one energy management solution provider." They are intending to enable their enterprise customers with a web-based dashboard where they can see historical trends of power consumption, no longer limited to the current quarter.

Fabrikam believes that small optimizations made using intra-day data is the best way to help its enterprise customers receive awards from CPL and cut costs from electricity use in the end. Within the customer dashboard, it would like to provide customized alerts for when a customer's electrical demand is "out of the ordinary" for that customer at that point of the day and could cause them to miss CPL rebates and energy savings. In short, they would like to provide their customers with a "hot" dashboard of near-real-time metrics and ultimately offer predictions over the current 15-minute period of data (albeit the predictive analytics is out of scope for this first effort) and support for the analysis of "cold" historical data (such as month over month, and year over year comparisons).

To accomplish this, Fabrikam will aggressively adjust its reporting interval for its smart meters to one-minute intervals for all of its 20,000 business customers and 200K smart meters. Each beacon generated by a meter is about 100 bytes in size and is sent to Fabrikam services via an HTTP POST over Transport Layer Security (TLS), Message Queuing Telemetry Transport (MQTT), or Advanced Message Queueing Protocol (AMQP).

In addition to collecting telemetry, Fabrikam not only seeks to gain competitive advantage by providing centralized reporting on their customers' smart meter data but would also like their new backend to be able to authorize which devices can send telemetry. They want a solution that will in the future enable Fabrikam's device administration website to send control messages to a particular device.

### Customer needs

1. We expect to have a "hot" and "cold" path of data.

2. The "hot" path will select certain data streaming in from the devices that we need to process in real time to drive updates to the customer dashboard.

3. While the "hot" path may focus on a subset of the incoming data, the "cold" path will store and process everything.

4. Because the "hot" path provides the data for the current day's operations, we only need to process the "cold" path on a nightly basis. Our customers are happy with performing their analysis starting with the last full day of data, which is inherently yesterday.

5. We want a way to visualize our data flow, the steps taken on the data, and the status of our data flow on a single screen.

6. We want to understand the ways in which we can scale the solution to accommodate future growth in terms of number of customers, meters, and the size of the data.

### Customer objections

1. We are considering an out-of-the-box time series database solution. Are there options for this on Azure?

2. We have a mix of large enterprise customers and many small-to-medium business (SMB) customers, which adds up to a lot of telemetry data to ingest, can Azure really handle it?

3. Can Azure handle a lambda architecture?

4. We have heard of Azure IoT Solution Accelerators, do these offer a good starting point for us?

5. Some of our customers require their IoT devices to communicate in a firewall-friendly way without opening up additional incoming or outgoing ports. What options do we have to accomplish this?

### Infographic of common scenarios

![A sample Internet of Things workflow is displayed, which is broken into On-Premises and Azure services. On-premises shows a building that has multiple devices communicating to the cloud. The Azure services ingest the telemetry through Event Hubs or IoT Hubs. From there Stream Processing functions through HDInsight Storm, HDInsight Spark, and Stream Analytics take place. Batch storage holds processed data in Data Lake and Storage Blobs services. Batch Processing takes place using HDInsight, Batch, SQL Data Warehouse and Machine Learning services. Views of this processed data are made available through HDInsight HBase, SQL Data Warehouse, and Search Services. Data is consumed by Analytics clients such as Power BI, Web Apps, and API applications. Stream near real time data is also made available to analytics clients through the use of Redis Cache, Cosmos DB, and SQL Databases - these services are fed directly from the stream processing engine(s).](./media/common-scenarios.png 'Common Internet of Things scenarios')

## Step 2: Design a proof of concept solution

**Outcome**

Design a solution and prepare to present the solution to the target customer audience in a 15-minute chalk-talk format.

Timeframe: 60 minutes

**Business needs**

Directions: With all participants at your table, answer the following questions and list the answers on a flip chart:

1. Who should you present this solution to? Who is your target customer audience? Who are the decision makers?

2. What customer business needs do you need to address with your solution?

**Design**

Directions: With all participants at your table, respond to the following questions on a flip chart:

_High-level architecture_

1. Without getting into the details (the following sections will address the particular details), diagram your initial vision for handling the top-level requirements for data ingest, hot and cold path processing, storage of telemetry data, and reporting. You will refine this diagram as you proceed.

_Device to cloud communication_

1. What is the anticipated volume in messages per second and in megabytes (MB) per second that Fabrikam will need to support given their customer base?

2. How would you propose they ingest that quantity of messages? What Azure service would you recommend and why? At what initial scale?

3. Diagram the device to cloud communication.

   - What protocol would they use in sending telemetry from the smart meter devices to the service used for message ingest?

   - What is the format of the message sent to the ingest endpoint?

   - What service endpoints do the devices talk to?

_Device provisioning_

1. Keeping the Azure service, you selected for ingest of telemetry data from the smart meters in mind, diagram how Fabrikam should handle the following three flows related to the provisioning of new smart meters at a customer site:

   - Create device identity

   - Install device

   - Activate device

_"Hot" path processing_

The "hot" path for Fabrikam is defined as the processing of the data as it arrives in near real-time fashion. A history of data collected this way need only be maintained for the current 24-hour period, providing a moving average of measures for each device across a 5-minute period. It needs to be made available as quickly as possible for consumption to be available on the web-based reporting interface.

1. How would you select out the "hot" data? Choosing between the stream processing options Azure Stream Analytics and Storm on HDInsight, which would you recommend for this scenario and why?

2. Explain how you could build the solution using Azure Stream Analytics:

   - What type of window would you use? What does your query look like?

   - How many Streaming Units would you need? Explain how you calculated it.

3. Explain how you could build the solution using Storm on HDInsight:

   - What are the high-level steps you would need to take?

4. How would you store the "hot" data for consumption by the web dashboard?

   - Estimate the write throughput you would require. Does your selected store support it?

_"Cold" path processing_

The "cold" path for Fabrikam is highlighted by the notion that Fabrikam does not want to lose any telemetry data from its devices, so it can be prepared to ask new questions of the data over time in the future. The second requirement is to support the calculation of period over period reports for each meter (for example, year over year, month over month, and week over week). For each new full day of data added, it amounts to updating the summary statistics for the current year, current month, and current week with a three-number summary consisting of minimum, mean, and maximum. Fabrikam would prefer the implementation of these reports to be done using syntax similar to SQL. Assume you want to create the summary statistics for each device by year.

1. How would you structure the output of blobs from your stream processing component? Draw an example hierarchy.

2. What would you use to query these blob files?

3. How would you orchestrate the processing and retain visibility into the status of the data flow? How would you configure this data flow? Be specific on what activities you would use.

_Cloud to device communication_

1. Diagram how commands from the Fabrikam device management website would flow to the target device. Be specific and identify endpoints used and protocols selected.

**Prepare**

Directions: With all participants at your table:

1. Identify any customer needs that are not addressed with the proposed solution.

2. Identify the benefits of your solution.

3. Determine how you will respond to the customer's objections.

Prepare a 15-minute chalk-talk style presentation to the customer.

## Step 3: Present the solution

**Outcome**

Present a solution to the target customer audience in a 15-minute chalk-talk format.

Timeframe: 30 minutes

**Presentation**

Directions:

1. Pair with another table.

2. One table is the Microsoft team and the other table is the customer.

3. The Microsoft team presents their proposed solution to the customer.

4. The customer makes one of the objections from the list of objections.

5. The Microsoft team responds to the objection.

6. The customer team gives feedback to the Microsoft team.

7. Tables switch roles and repeat Steps 2-6.

## Wrap-up

Timeframe: 15 minutes

Directions: Tables reconvene with the larger group to hear the facilitator/SME share the preferred solution for the case study.

## Additional references

|                         |                                                                                       |
| ----------------------- | ------------------------------------------------------------------------------------- |
| **Description**         | **Links**                                                                             |
| IoT Hubs                | <https://azure.microsoft.com/documentation/articles/iot-hub-devguide/>                |
| IoT Hub Message Routing | <https://docs.microsoft.com/azure/iot-hub/tutorial-routing>                           |
| Event Hubs              | <https://azure.microsoft.com/documentation/articles/event-hubs-overview/>             |
| Stream Analytics        | <https://azure.microsoft.com/documentation/articles/stream-analytics-introduction/>   |
| Data Factory            | <https://azure.microsoft.com/documentation/articles/data-factory-introduction/>       |
| Storm                   | <https://azure.microsoft.com/documentation/articles/hdinsight-storm-overview/>        |
| Hive                    | <https://azure.microsoft.com/documentation/articles/hdinsight-use-hive/>              |
| Spark                   | <https://azure.microsoft.com/documentation/articles/hdinsight-apache-spark-overview/> |
| Azure Databricks        | <https://azure.microsoft.com/services/databricks/>                                    |
