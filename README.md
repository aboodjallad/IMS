# Inventory Management System (IMS)

## Description
The Inventory Management System (IMS) is a versatile application designed for small to medium businesses, enabling effective management of inventory, order processing, and sales analysis. This system automates inventory tracking to update stock levels in real-time, facilitates the entire order lifecycle, and generates detailed reports for insightful business decisions.

## Key Features
- **Real-time Inventory Tracking**: Monitors stock levels to reflect current inventory, including sales and restocks.
- **Automated Order Management**: Streamlines order processing from placement to fulfillment, enhancing efficiency.
- **User Access Control**: Implements role-based access control (RBAC) to manage system permissions securely.

## Installation
1. **Prerequisites**: Ensure PostgreSQL is installed and running.
2. **Clone the Project**:
   ```bash
   git clone https://github.com/aboodjallad/IMS.git
   cd IMS
# Install dependencies 
```bash

dotnet restore
```

# To start the application 
```bash
dotnet run
```
## Ensure the database connection string in appsettings.json matches your PostgreSQL setup:
```bash
"ConnectionStrings": {
    "DefaultConnection": "Host=localhost; Port=5432; Database=ims; Username=postgres; Password=password;"
}

