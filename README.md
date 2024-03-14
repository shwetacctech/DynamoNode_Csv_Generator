# Dynamo Node for Exchange Data Retrieval

## Overview
This project includes a Dynamo node created using the ZeroTouch library. The node is designed to fetch exchange data by fileurn and return a list of properties in a dictionary format. <br>
The inputs to the node consist of the filepath of a CSV file, which is the result of the exchange data retrieval process.

## Features
- Fetch exchange data by fileurn
- Return exchange data properties in a dictionary format
- Accept filepath of CSV file as input
- Writes the output data into csv filepath provided by user

## Usage
1. Open your Dynamo project.
2. Add the custom node to your Dynamo workspace.
3. Connect the required inputs, including the filepath of the CSV file.
4. Run the Dynamo script to retrieve the exchange data.
5. Utilize the output dictionary containing the exchange data properties.

## Installation
To use this custom Dynamo node, follow these steps:
1. Clone this repository to your local machine.
2. Open the project in Visual Studio.
3. Build the project to generate the necessary DLL files.
4. Copy the generated DLL files to the appropriate directory for custom nodes in Dynamo.
5. Restart Dynamo to load the custom node.

## Dependencies
- ZeroTouch library

