# FamilyTree.API

### API Desenvolvida para gerenciar membros em uma **Árvore Genealógica**

### **Rotas disponíveis**

**GET - https://localhost:8000/api/person/v1/id/{id}**

    Obtém os ascendentes e descentes de uma pessoa com base em seu identificador.

**GET - https://localhost:8000/api/person/v1/name/{name}**

    Obtém as informações de uma pessoa com base em seu nome.

**GET - https://localhost:8000/api/person/v1**

    Obtém as informações de toda a coleção no banco.

**POST - https://localhost:8000/api/person/v1**

    Insere informações de uma nova pessoa.

**PUT - https://localhost:8000/api/person/v1**

    Atualiza informações de uma pessoa já existente.

**DELETE - https://localhost:8000/api/person/v1/id/{id}**

    Exclui uma pessoa com base em seu identificador.


## Tecnologias utilizadas

**Codebase:** `.NET 6`, `ASP.NET Core Web API` e `C#` 

**Banco de dados:** `MongoDB`

**Infra:** `Docker` (`Dockerfile` e `docker-compose`)

**Documentação de APIs:** `Swagger`

**Testes unitários:** `MSTest` e `Moq` 
## Utilização da API

Este projeto possui sua construção em arquivos `Dockerfile` e `docker-compose.yml`. Portanto, para executar a API, basta navegar até o diretório raiz da solução:

```bash
cd src/FamilyTree
```

E em seguida, executar o seguinte comando:

```bash
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
```

O **Docker** irá baixar a imagem oficial do MongoDB, realizar o build na aplicação e disponibilizar o acesso através da porta `8000`.
Como o projeto também utiliza o Swagger como "client", para acessar as rotas e realizar testes basta ir para o seguinte endereço:

```bash
http://localhost:8000/swagger/index.html
```

## Testes unitários

Para executar os testes unitários da aplicação, basta navegar até o diretório de testes:

```bash
  cd src/FamilyTree/FamilyTree.Tests
```

E em seguida, executar o seguinte comando:

```bash
  dotnet test
```

## Desafios

Realizar este projeto foi bastante interessante, pois fui exposto à ferramentas e tecnologias que não havia trabalhado a fundo anteriormente.
Foi curioso lidar com o MongoDB, por exemplo, já que toda a noção que tinha de SGBD's/Banco de Dados era a orientada à relacionamentos.
Devido há afazeres e outras obrigações, não pude implementar mais funcionalidades interessantes na API até o momento mas com certeza, darei continuidade ao projeto para incrementá-lo ainda mais.