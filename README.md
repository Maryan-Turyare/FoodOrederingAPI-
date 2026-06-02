# Food Ordering System

A full-stack Food Ordering System built using ASP.NET Core Web API, PostgreSQL, React, and Tailwind CSS.

## Features

### Authentication
- User Registration
- User Login
- JWT Token Authentication
- Logout

### Food Management (CRUD)
- Add Food
- View Food Menu
- Update Food
- Delete Food

### Order Management (CRUD)
- Add Order
- View Orders
- Update Order
- Delete Order

## Technologies Used

### Backend
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- JWT Authentication

### Frontend
- React (Vite)
- Axios
- React Router DOM
- Tailwind CSS

## API Endpoints

### Auth
- POST `/api/Auth/register`
- POST `/api/Auth/login`

### Food
- GET `/api/Food`
- POST `/api/Food`
- PUT `/api/Food/{id}`
- DELETE `/api/Food/{id}`

### Order
- GET `/api/Order`
- POST `/api/Order`
- PUT `/api/Order/{id}`
- DELETE `/api/Order/{id}`

## How to Run

### Backend
```bash
dotnet run

## Author
Maryan Turyare
