# FoodScanApp

**FoodScanApp** is a hobby project designed to simplify the process of checking ingredients and health-related information of food products. The app allows users to scan food items in stores, retrieve detailed ingredient lists, and assess the health implications based on data from **Livsmedelsverket** (The Swedish Food Agency).

The app leverages the **Livsmedelsverket API**, which provides a comprehensive database of food items and their associated nutritional data. By scanning barcodes or entering food item information, users can instantly access information about potential allergens, chemicals, and other health concerns linked to specific products.

## Key Technologies:
- **.NET Core**: The backend of the application is built using **.NET Core**, ensuring high performance and cross-platform compatibility.
- **ASP.NET Core MVC**: The backend APIs are built using **ASP.NET Core MVC**, allowing for clean separation of concerns and a scalable architecture.
- **Livsmedelsverket API**: The app integrates with **Livsmedelsverket’s API** to pull accurate and up-to-date data about food items, including nutritional values and ingredients.
- **Unit Testing**: The application incorporates unit testing to ensure robust and reliable functionality.
- **Git & GitHub**: The development process is managed using **Git** for version control and **GitHub** for repository hosting and collaboration.

## Core Features:
- **Barcode Scanning**: Users can scan barcodes to instantly retrieve food product details.
- **Ingredient Lookup**: The app retrieves ingredient information for any food item and cross-references it with **Livsmedelsverket**'s health database.
- **Health Alerts**: The app informs users about potential health risks based on the ingredients listed in the food product.

This project combines practical usage of APIs, modern development frameworks, and integration with external data sources to deliver a useful tool for everyday health-conscious shoppers.
