# 🚀 .NET Microservices with REST, gRPC & RabbitMQ  

This project demonstrates building **two microservices** using **.NET 9 Web API**, applying modern software architecture principles like API Gateway, asynchronous communication, and Kubernetes deployment.  

---

## 📌 Overview  

We are building two core microservices:  

### 🔹 Platform Service  
- Functions as an **Asset Register** (tracks platforms/systems in the company).  
- Built by the **Infrastructure Team**.  
- Used by Infrastructure, Technical Support, Engineering, Accounting, and Procurement teams.  

### 🔹 Command Service  
- Functions as a **repository of command line arguments** for given platforms.  
- Supports automation of support processes.  
- Built by the **Technical Support Team**.  
- Used by Technical Support, Infrastructure, and Engineering teams.  

Both services are designed to run **independently** but also communicate when required.  

---

## 🛠️ Features  

- ✅ **Two .NET 9 Microservices** using the REST API pattern  
- ✅ **Dedicated persistence layers** for each service  
- ✅ **Kubernetes deployment** for scalability and resilience  
- ✅ **API Gateway pattern** to route requests to the right service  
- ✅ **Synchronous messaging** between services using **HTTP & gRPC**  
- ✅ **Asynchronous messaging** using **RabbitMQ (Event Bus)** for loose coupling  

---

## 🏗️ Architecture  


graph TD;
    Client-->API_Gateway;
    API_Gateway-->Platform_Service;
    API_Gateway-->Command_Service;
    Platform_Service-->|HTTP/gRPC|Command_Service;
    Platform_Service-->|Events (RabbitMQ)|Command_Service;

- **API Gateway**: Routes client requests to the correct microservice  
- **REST / gRPC**: Used for synchronous communication  
- **RabbitMQ**: Used for asynchronous, event-driven messaging  
- **Kubernetes**: Handles container orchestration  

---

## 📂 Project Structure  

.NET-Microservices/
│── PlatformService/      # Platform microservice (REST, gRPC, RabbitMQ publisher)
│── CommandService/       # Command microservice (REST, gRPC, RabbitMQ subscriber)
│── k8s/                  # Kubernetes deployment manifests
│── gateway/              # API Gateway configuration
│── README.md             # Project documentation



---

## 🔄 Request Flow Example  

1. **Client** sends a request to the **API Gateway**  
2. The **API Gateway** forwards it to the **Platform Service**  
3. The **Platform Service**:  
   - Processes the request  
   - Publishes an event to **RabbitMQ**  
4. **Command Service** receives the event and updates its data store  
5. The client can query either service via the API Gateway  


---

## 🚀 Getting Started  

### 1️⃣ Clone the Repository  

git clone https://github.com/your-username/.net-microservices.git
cd .net-microservices

### 2️⃣ Run Locally with Docker Compose

docker-compose up --build

### 3️⃣ Deploy to Kubernetes

kubectl apply -f k8s/


---

## 🔗 Technologies  

- **.NET 9 (Web API)**  
- **Entity Framework Core** (SQL Server & InMemory for dev/test)  
- **RabbitMQ** (Event Bus)  
- **gRPC** (Synchronous messaging)  
- **API Gateway** (Request routing)  
- **Kubernetes + Docker** (Deployment & orchestration)  

---

## 👨‍💻 Contributors  

- **Dasun Sandeepa** – Project Maintainer / Platform & Command Microservices  

💡 Team contributions are welcome! 🙌  


