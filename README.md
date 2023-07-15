
# OBD-II Reader

This project is a library that simplifies communication with a vehicle's On-Board Diagnostics (OBD-II) system using an ELM327 device. The core of this library is the `ELM327` class, which communicates with the device using commands defined by the OBD-II protocol.

This library is designed to allow for flexibility in the communication mechanism. The `ELM327` class depends on an `ISerialPort` interface, which can be implemented for different communication mechanisms such as serial ports, Bluetooth, or others.

## Components

The library consists of the following main components:

1. **`ISerialPort` Interface**: This interface provides a contract for serial communication with the ELM327 device. Implementations of this interface could provide communication over USB, Bluetooth, etc.

2. **`ELM327` Class**: This class is responsible for sending commands to the ELM327 device, reading the response and translating that response into a usable format. It depends on an implementation of the `ISerialPort` interface for the actual data transfer.

3. **`OBDIIInterpreter` Class**: This class is a data interpreter which uses a dictionary of command handlers. Each command handler knows how to interpret the response from a specific OBD-II command.

4. **`Emissions` Class**: This class provides a method to retrieve emission readiness data from a given response. The response is interpreted to determine the status of the various emission-related systems in the vehicle.

5. **`Fault` Class**: This class provides methods to handle fault codes. It includes functionality for determining the number of fault codes, and for retrieving descriptions of fault codes from a database.

## Design Notes

1. **Interface Segregation**: The `ISerialPort` interface is a key part of the design, segregating the communication mechanism from the OBD-II command/response logic. This means that the `ELM327` class is not tied to a specific communication mechanism, and can be used with a USB-based serial port, a Bluetooth-based serial port, or any other communication mechanism.

2. **Dependency Injection**: The `ELM327` class receives an instance of `ISerialPort` in its constructor. This is an example of dependency injection, and it means that the `ELM327` class doesn't need to know how to construct an `ISerialPort` instance.

3. **Single Responsibility**: Each class in the library has a single responsibility. For example, the `ELM327` class is responsible for communicating with the ELM327 device, but not for interpreting the responses. The `OBDIIInterpreter` class is responsible for interpreting responses.

4. **Command Pattern**: The `OBDIIInterpreter` class uses the command pattern to handle responses. Each command handler is a delegate that knows how to handle a specific type of response.

5. **Database Access**: The `Fault` class uses a SQLite database to retrieve descriptions of fault codes. This shows how the library can integrate with a database to provide more detailed information.
