# OBDIIToolKit

The OBDIIToolKit is a C# library designed to simplify communication with OBD-II (On-Board Diagnostics) devices. This library provides modules for communication, executing commands, and handling responses, supporting both serial and Bluetooth communication protocols.

## Core Components

### 1. Communication Module

#### 1.1 ICommunicator Interface

Defines the basic contract for communication. Methods include ConnectAsync, Disconnect, Write, ReadString, and Dispose.

#### 1.2 SerialCommunicator Class

Implements the ICommunicator interface for serial communication. Uses the SerialPort class for serial communication.

#### 1.3 BluetoothCommunicator Class

Implements the ICommunicator interface for Bluetooth communication. Uses the BluetoothClient class for Bluetooth communication.

#### 1.4 BluetoothDeviceDiscoverer Class

Discovers Bluetooth devices using the InTheHand.Net.Sockets library.

### 2. ELM327 Controller

#### 2.1 ELM327Controller Class

Provides utility methods for sending commands, reading responses, and handling timeouts. Supports debug mode to print communication details to the console.

### 3. OBD-II Commands

#### 3.1 ICommand Interface

Defines the contract for OBD-II commands. Includes the pid property and Execute method.

#### 3.2 Command Class

Implements the ICommand interface. Represents an OBD-II command with a specific PID (Parameter ID). Executes the command and validates the response using a provided validator function.

#### 3.3 CommonCommands Class

Provides factory methods for creating common OBD-II commands. Examples include fault code retrieval, setting the protocol to auto, and retrieving engine load.

### 4. Emissions Module

#### 4.1 Emissions Class

Parses and interprets OBD-II responses related to emission readiness. Categorizes emission tests and their readiness status.

### 5. Fault Code Handling

#### 5.1 Fault Class

Retrieves and interprets fault codes from OBD-II responses. Uses an SQLite database to map fault codes to descriptions and categories.

## Getting Started

To use the OBDIIToolKit library, follow these steps:

1. Clone the repository.
2. Reference the OBDIIToolKit project in your C# solution.
3. Use the provided classes and modules to communicate with OBD-II devices.
