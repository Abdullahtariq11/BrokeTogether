# Broke Together 🏡✨

![Broke Together Logo](https://placehold.co/600x300/A3B18A/FFFFFF?text=Broke+Together&font=poppins)

**Share groceries and expenses, stress-free.**

Broke Together is a mobile app designed to eliminate the friction of managing a shared home. It reframes shared expenses from "debt" into "contributions," fostering household harmony through a collaborative and beautifully simple interface.

[![Build Status](https://img.shields.io/badge/build-passing-brightgreen)](https://github.com/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Contributions Welcome](https://img.shields.io/badge/contributions-welcome-brightgreen.svg?style=flat)](./CONTRIBUTING.md)

---

## The Vision: More Than Just a Ledger

Existing expense-splitting apps feel like cold, transactional ledgers. They solve the math problem but ignore the human element. Broke Together is different. Our core mission is to be a **collaborative household tool** that makes shared living easier, more transparent, and less awkward.

We are not a "Splitwise killer"; we are a **household harmony tool**. We achieve this by focusing on a specific use case—roommates managing shared goods—and solving it perfectly.

## ✨ Core Features (MVP)

* **🛒 Shared Shopping List:** A real-time, collaborative shopping list. Anyone in the household can add an item, and anyone can shop for it. This is our killer feature that solves the problem *before* the purchase happens.
* **⚡️ One-Tap Contributions:** Convert a shopping trip into a shared expense in seconds. Our "magic flow" pre-fills the description from the shopping list, so all you have to do is enter the total amount.
* **💰 Simplified Balances:** No more confusing webs of debt. Our "Pot" dashboard gives you a clear, at-a-glance summary of who's chipped in and what's needed to balance the household.
* **🤝 Balance the Pot:** A simple, guided flow to settle up. We tell you the easiest way to make things even, and you mark payments as complete.
* **🏡 Household Focused:** Create a "Home" and invite your roommates with a simple code. Everything is centered around your shared space.

## 🎯 Target Audience

Our primary user is the **"Urban Roommate"**: a tech-savvy young professional or student living in a shared apartment who wants to keep finances fair without the stress and awkwardness of nagging for money.

## 🛠️ Technology Stack

* **Frontend:** React Native (or Flutter)
* **Backend:** ASP.NET Core Web API
    * **Database:** PostgreSQL or SQL Server with Entity Framework Core.
    * **Real-time Communication:** ASP.NET Core SignalR for live updates to the shopping list and contribution feed.
    * **Authentication:** JWT-based authentication using ASP.NET Core Identity.

## 🚀 Getting Started

Follow these instructions to get a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

* Node.js and npm/yarn installed
* A development environment set up for React Native (see [React Native Environment Setup](https://reactnative.dev/docs/environment-setup))
* .NET 8 SDK (or newer)
* A database server (e.g., PostgreSQL or SQL Server)

### Installation & Setup

#### Backend (ASP.NET Core)

1.  **Navigate to the backend directory:**
    ```sh
    cd backend
    ```
2.  **Restore dependencies:**
    ```sh
    dotnet restore
    ```
3.  **Configure your database:**
    * Open `backend/BrokeTogether.Api/appsettings.Development.json`.
    * Update the `DefaultConnection` connection string with your database credentials.
4.  **Apply database migrations:**
    ```sh
    dotnet ef database update --project backend/BrokeTogether.Infrastructure
    ```
5.  **Run the backend server:**
    ```sh
    dotnet run --project backend/BrokeTogether.Api
    ```
    The API will be running at `https://localhost:7001` (or a similar port).

#### Frontend (React Native)

1.  **Navigate to the frontend directory:**
    ```sh
    # From the root directory
    cd frontend
    ```
2.  **Install dependencies:**
    ```sh
    npm install
    # or
    yarn install
    ```
3.  **Configure API endpoint:**
    * In the frontend's service configuration file (e.g., `src/services/apiConfig.js`), update the API base URL to point to your running backend server.
4.  **Run the app:**
    * **For iOS:**
        ```sh
        npx pod-install
        npx react-native run-ios
        ```
    * **For Android:**
        ```sh
        npx react-native run-android
        ```

## 📂 Project Structure

This project is structured as a monorepo using a Clean Architecture approach for the backend.


BrokeTogether/
├── backend/                              # .NET Solution Root
│   ├── BrokeTogether.Api/                # ASP.NET Core Web API (Controllers, DI, Filters)
│   ├── BrokeTogether.Application/        # Business logic (Services, DTOs, Validation)
│   ├── BrokeTogether.Domain/             # Core business entities and contracts
│   ├── BrokeTogether.Infrastructure/     # EF Core, Repositories, DbContext
│   └── BrokeTogether.Tests/              # Unit and integration tests
│
├── frontend/                             # React Native project
│   ├── android/
│   ├── ios/
│   └── src/
│
└── BrokeTogether.sln                     # Visual Studio Solution file


## 🤝 Contributing

Contributions are what make the open-source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

Please see our [CONTRIBUTING.md](./CONTRIBUTING.md) file for details on our code of conduct and the process for submitting pull requests.

## 📜 License

This project is licensed under the MIT License - see the [LICENSE.md](./LICENSE.md) file for details.
