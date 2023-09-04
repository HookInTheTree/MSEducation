# MSEducation
RU: Учебный проект для получения знаний и практического опыта в разработке микросервисов.
EN: A learning project to gain knowledge and practical experience in microservices development.
# Target
RU: Конечной целью данного проекта является: аутентификация/авторизация (JWT) в микросервисах, с использованием api-шлюза Ocelot
EN: The ultimate goal of this project is: authentication/authorization (JWT) in microservices, using the Ocelot api gateway
# Stack
.Net 7, Ocelot, EF CORE 7, Docker, Docker Compose
# Microservices
Auth-service
Customer-service
Orders-service
# Gateaway
RU: Gateaway - api-шлюз, задача которого маршрутизация запросов к конкретным микросервисам.
EN: Gateaway service (using Ocelot) that acts as a proxy. Accepts requests and routes them to specific microservice
