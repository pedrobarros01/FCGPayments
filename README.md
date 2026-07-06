# 🚀 FIAP-Cloud-Games Payments Worker

Esse worker foi construido para compor o projeto da FIAP-Cloud-Games, com o intuito de realizar a etapa do fluxo de 
pagamentos para simular o processamento de pagamento de uma compra de jogo

---

## 📌 Segunda fase

Essa fase consiste em refatorar o monolitico feito na primeira fase em uma arquitetura de microsserviços, contendo os 
seguintes microsserviços

* Microsserviço de Usuários (UsersAPI)
* Microsserviço de Catálogo (CatalogAPI e CatalogWorker)
* Microsserviço de Pagamentos (PaymentsWorker)
* Microsserviço de Notificações (NotificationsWorker)

## Imagem no docker
A imagem desse projeto está disponível no Docker Hub como `drungas/fcgpayments`. As variaveis de ambientes são 
mostradas na seção de `Como rodar o projeto via docker compose`


---

## ⚙️ Tecnologias

* .NET 10
* Worker Service
* Entity Framework Core
* PostgreSQL

---

## 📦 Pré-requisitos

Antes de começar, você precisa ter instalado:

* .NET SDK 10
* Banco de dados (PostgreSQL)

---

## ▶️ Como rodar o projeto localmente

### 1. Clonar o repositório

```bash
git clone --recurse-submodules https://github.com/pedrobarros01/FCGPayments.git
cd FCGPayments
```

### 2. Configurar variáveis de ambiente

Cri e modifique o arquivo `appsettings.json` para o template abaixo:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=portaDoBanco;Database=seubanco;Username=seuusuario;Password=suasenhga"
  },
  "RabbitMQ": {
    "Host": "host",
    "VirtualHost": "vhost",
    "Username": "usuario",
    "Password": "senha",
    "KeyQueueOrderPlaced": "fila_pedido",
    "KeyPublisher": "fila_pedido_processado"
  }

}
```

### 3. Executar o worker

```bash
cd src/FCG.Payments
dotnet run
```

> É necessário subir um serviço de RabbitMQ e Postgres para poder rodar o projeto.

---
## ▶️ Como rodar o projeto via docker compose

### 1. Clonar o repositório

```bash
git clone --recurse-submodules https://github.com/pedrobarros01/FCGPayments.git
cd FCGPayments
```

### 2. Configurar variáveis de ambiente

Acesse o arquivo `docker-compose.yml` e modifique as seguintes variáveis de ambiente abaixo:
```
Chave do postgres:
POSTGRES_PASSWORD: senha

Chave do pgadmin:
PGADMIN_DEFAULT_EMAIL: "email@email.com"
PGADMIN_DEFAULT_PASSWORD: senha

Chave da imagem Rabbit:
RABBITMQ_DEFAULT_USER: usuario_rabbit
RABBITMQ_DEFAULT_PASS: senha_rabbit
RABBITMQ_DEFAULT_VHOST: vhost_rabbit

Chave do banco:
ConnectionStrings__DefaultConnection: "Host=;Port=;Database=;Username=;Password="

Chaves do Rabbit no projeto:
ConnectionStrings__DefaultConnection: "Host=fcgpayments-postgres-1;Port=5432;Database=postgres;Username=postgres;Password=senha"
RabbitMQ__Host: fcgpayments-rabbitmq-1
RabbitMQ__VirtualHost: vhost_rabbit
RabbitMQ__Username: usuario_rabbit
RabbitMQ__Password: senha_rabbit
RabbitMQ__KeyQueueOrderPlaced: "fila_pedido"
RabbitMQ__KeyPublisher: "fila_pedido_processado"
```

### 3. Executar o compose
#### 3.1 Subir os containers
```bash
cd FCGPayments
docker compose up
```

#### 3.2 Descer os containers
```bash
docker compose down
```
---
## ▶️ Como rodar o projeto via kubernetes

### 1. Clonar o repositório

```bash
git clone --recurse-submodules https://github.com/pedrobarros01/FCGPayments.git
cd FCGPayments
```

### 2. Configurar variáveis de ambiente

Acesse o arquivo `k8s/configmap.yml` e modifique a variáveis de ambiente abaixo:
```
RabbitMQ__KeyQueueOrderPlaced: "fila_pedido"
RabbitMQ__KeyPublisher: "fila_pedido_processado"
```

Acesse o arquivo `k8s/secrets.yml` e modifique a variáveis de ambiente abaixo:
```
ConnectionStrings__DefaultConnection: "Host=host;Port=5432;Database=;Username=;Password="
RabbitMQ__Host: host_rabbit
RabbitMQ__VirtualHost: vhost_rabbit
RabbitMQ__Username: usuario_rabbit
RabbitMQ__Password: senha_rabbit
```

> É necessário subir um serviço de RabbitMQ e Postgres para poder rodar o projeto.

### 3. Executar o projeto
#### 3.1 Aplicar o kubernetes
```bash
cd FCGCatalogWorker
kubectl apply -f fcg-payments-config.yml
kubectl apply -f fcg-payments-secret.yml
kubectl apply -f fcg-payments.yml
```

#### 3.2 Deletar o Configmap, Secret e Deployment
```bash
kubectl delete configmap fcg-payments-config
kubectl delete secret fcg-payments-secret
kubectl delete deployment fcg-payments
```

---

## 🗂️ Estrutura do projeto

```
/src
 ├── FCG.Payments
 ├── FCG.Payments.Application
 ├── FCG.Payments.Domain
 ├── FCG.Payments.Infrastructure
 |── FCG.Payments.Logger
```

---

## 🧪 Testes

```bash
dotnet test
```

---

## 🤝 Contribuição

1. Crie uma branch:

```bash
git checkout -b feature/minha-feature
```

2. Commit:

```bash
git commit -m "feat: minha nova feature"
```

3. Push:

```bash
git push origin minha-feature
```

---


## 📞 Contato

O time responsável desse sistema:
- Igor Anthony - igor.anthony.iop@gmail.com
- Nathalia Greice - nponce410@gmail.com
- Otávio de Andrade - otavio_andrade@live.com
- Pedro Henrique Barros - pedrobarros0101@outlook.com
- Sérgio Henrique - ssergioh3@gmail.com