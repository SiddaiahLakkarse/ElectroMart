ElectroMart
A responsive electronics marketplace built with React/Vite and an ASP.NET Core 8 Web API.

Structure
ElectroMart/
├── backend/ElectroMart.API/
│   ├── Controllers/ Data/ DTOs/ Middleware/ Models/ Services/
│   ├── Program.cs
│   └── appsettings.json
├── frontend/
│   └── src/api components context ...
└── README.md
Prerequisites
.NET 8 SDK
Node.js 20+
SQL Server LocalDB (Windows/Visual Studio installation)
Run the backend
cd backend/ElectroMart.API
dotnet restore
dotnet run
The API uses Server=(localdb)\\MSSQLLocalDB;Database=ElectroMartDb by default and seeds the schema and catalog on first run. Update appsettings.json or use user secrets/environment variables for real deployments. Replace the development JWT key before production.

EF Core migrations
The current demo also calls EnsureCreated for a frictionless first run. For migration-managed environments:

dotnet tool install --global dotnet-ef
dotnet ef migrations add InitialCreate
dotnet ef database update
Run the frontend
cd frontend
npm install
npm run dev
Set VITE_API_URL when the API is not at the default https://localhost:7001/api, for example in frontend/.env.local:

VITE_API_URL=https://localhost:7001/api
Demo admin
Email: admin@electromart.com
Password: Admin@123
Passwords are stored as BCrypt hashes, never as plaintext.

API summary
POST /api/auth/register, POST /api/auth/login, GET /api/auth/me
GET /api/categories, GET /api/categories/{id}
GET /api/products (search, categoryId, brand, minPrice, maxPrice, sortBy, pageNumber, pageSize), GET /api/products/featured, GET /api/products/{id}
Authenticated cart: GET /api/cart, POST /api/cart/items, PUT /api/cart/items/{id}, DELETE /api/cart/items/{id}, DELETE /api/cart/clear
Authenticated orders: POST /api/orders/checkout, GET /api/orders/my-orders, GET /api/orders/{id}
Admin: GET /api/orders, PUT /api/orders/{id}/status, GET /api/admin/dashboard
Swagger: /swagger in development
Example requests
Register:

POST /api/auth/register
{"firstName":"Ada","lastName":"Lovelace","email":"ada@example.com","password":"SecurePass1!"}
Login response:

{"token":"eyJ...","user":{"id":2,"firstName":"Ada","lastName":"Lovelace","email":"ada@example.com","role":"Customer"}}
Product response:

GET /api/products?search=sony&pageNumber=1&pageSize=12
{"items":[{"id":10,"name":"WH-1000XM5","brand":"Sony","price":399,"discountPrice":349,"stockQuantity":30,"category":"Headphones"}],"pageNumber":1,"pageSize":12}
Add to cart:

POST /api/cart/items
Authorization: Bearer <token>
{"productId":10,"quantity":1}
Checkout:

POST /api/orders/checkout
Authorization: Bearer <token>
{"shippingAddress":"123 Main St, Austin, TX 78701","paymentMethod":"Card"}
The server validates stock, calculates the order total, deducts inventory in a transaction, snapshots product details into order items, and clears the cart.
