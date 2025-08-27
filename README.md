# 🛒 Sistema de E-commerce com Arquitetura de Microserviços

Este projeto foi desenvolvido como desafio técnico solicitado pela **Avanade**, com o objetivo de construir uma aplicação de **gerenciamento de estoque e vendas** para uma plataforma de e-commerce, utilizando **arquitetura de microserviços** e boas práticas de desenvolvimento.

---

## ✨ Visão Geral

O sistema é composto por dois microserviços principais e um **API Gateway**, garantindo separação de responsabilidades, escalabilidade e comunicação eficiente entre os serviços.

- **Microserviço de Estoque**  
  Responsável por cadastrar e gerenciar os produtos e seus estoques.  

- **Microserviço de Vendas**  
  Responsável por processar pedidos e validar a disponibilidade de produtos antes da compra.  

- **API Gateway**  
  Ponto único de entrada, roteando as requisições para os microserviços corretos.  

- **RabbitMQ**  
  Utilizado para comunicação assíncrona entre os serviços, notificando mudanças de estoque após vendas.  

- **Autenticação via JWT**  
  Garantindo segurança e controle de acesso para ações de estoque e vendas.  

---

## ⚙️ Tecnologias Utilizadas

- **.NET Core (C#)** – Desenvolvimento dos microserviços  
- **Entity Framework Core** – ORM e persistência de dados  
- **SQL Server** – Banco de dados relacional  
- **RabbitMQ** – Comunicação assíncrona entre os serviços  
- **RESTful API** – Padrão de comunicação  
- **JWT (JSON Web Token)** – Autenticação e autorização  
- **Docker** – Containerização e orquestração  

---

## 📌 Funcionalidades

### Microserviço 1: Gestão de Estoque
- Cadastro de produtos (nome, descrição, preço, quantidade).
- Consulta ao catálogo de produtos.
- Atualização automática do estoque após vendas.

### Microserviço 2: Gestão de Vendas
- Criação de pedidos com validação de estoque.
- Consulta de status dos pedidos.
- Notificação ao serviço de estoque sobre reduções.

### Comum aos dois serviços
- Autenticação JWT para todas as requisições.
- Acesso centralizado via API Gateway.

---

**Projeto desenvolvido como parte de um desafio técnico da Avanade, aplicando arquitetura moderna e escalável de microserviços.**
👨‍💻 Desenvolvedor Lucas Lôbo | 💼 Desenvolvedor .NET | Full Stack
