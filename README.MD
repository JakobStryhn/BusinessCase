### BusinessCase Project

## API for a growing company
This project is a basic CRUD API for creating employees in an associated office building

## Architecture Design
Albeit not perfect due to the size of the project, the folder structure is implementing a vertical slice feature design.

This is done to keep feature functionality in high cohesion - low coupling compared to the noun-based design that usually creates low cohesion and high coupling.


## Warnings As Errors
WarningsAsErrors is turned on for this project, as no warning should be ignored without consideration.

## FluentValidation
Fluent validation has been added to validate all incoming DataTransferObjects to ensure data integrity before it reaches the Core application layer.

## Unit Tests
Unit tests has been implemented to the Fluent Validation that happen on the controller level to safeguard against changes, which could affect the core business rules for the Employee object.

## Logging?

