# Test Project for PRAS

![Дизайн без названия](https://github.com/user-attachments/assets/de57e274-4397-4f88-b139-229673675cb2)


## Features
- **CRUD operations** for News, including create, read, update, and delete functionality.
- **Clean Architecture** structure, separating concerns between Presentation, Application, and Domain layers.
- **CQRS (Command Query Responsibility Segregation)** pattern for handling commands and queries.
- **Server-side validation** implemented with **FluentValidation**.
- Integration with **external APIs**:
  - **Cloudinary**: for uploading and managing images.
  - **Systran**: for language translation of news content.
- Multi-language support for news content.


## Project Structure
- **Domain**: Core business models and enums.
- **Application**: DTOs, Commands, Queries, and Services.
- **Data Access**: Database context.
- **Infrastructure**: API integrations.
- **Web**: MVC controllers and Razor Views.

## apsettings is deleted according to storing API keys but here is the structure of it:

```
"ConnectionStrings": {
    "DefaultConnection": ""
  },
  "ExternalServicesOptions": {
    "CloudinaryOptions": {
      "CloudName": "",
      "ApiKey": "",
      "ApiSecret": ""
    },
    "TranslatorOptions": {
      "ApiKey": "",
      "ApiEndpoint": ""
    }
  }

