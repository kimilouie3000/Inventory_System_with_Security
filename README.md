# Inventory_System_with_Security

Secure ASP.NET Core 8 Web API implementing authentication and inventory management.

## Security Features
- AES-256 reversible encryption for `Firstname`, `MiddleName`, and `Lastname` at rest.
- bcrypt password hashing (`workFactor: 10`).
- Confirm-password validation on registration.
- JWT-based authentication and protected inventory routes.
- SHA-256 integrity hash for product business fields.
- Global exception middleware to avoid leaking stack traces.
- Input validation through data annotations and EF Core parameterized queries.

## Run
1. Install .NET 8 SDK.
2. Update `Jwt:Key` and `Security:AesKey` in `appsettings.json`.
3. Run:
   ```bash
   dotnet restore
   dotnet run
   ```

## API Endpoints
### Auth
- `POST /api/auth/register`
- `POST /api/auth/login`

### Categories (JWT required)
- `GET /api/categories`
- `POST /api/categories`

### Suppliers (JWT required)
- `GET /api/suppliers`
- `POST /api/suppliers`

### Products (JWT required)
- `GET /api/products`
- `POST /api/products`
- `PUT /api/products/{id}`
- `DELETE /api/products/{id}`

## Sample Payloads
### Register
```json
{
  "firstName": "John",
  "middleName": "A",
  "lastName": "Doe",
  "email": "john@example.com",
  "password": "P@ssword123",
  "confirmPassword": "P@ssword123"
}
```

### Login
```json
{
  "email": "john@example.com",
  "password": "P@ssword123"
}
```
