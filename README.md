# Lab Manager

Aplicação que gerencia dados de computadores e laboratórios em um banco de dados.

## Funcionalidades

- Cadastro e listagem de computadores

- Cadastro e listagem de laboratórios


## Tecnologias utilizadas

- dotnet 6.0

## Uso

Clone o reposítório e realize os seguintes comandos para a inserção de dados nas tabelas Computadores e Laboratório.


 ```
 dotnet run -- Computer New id ram processador
 ```
Substitua os valores de id, ram e processor pelos dados que devem ser adicionados nas tuplas.

Para exibir esses dados utilize:

 ```
 dotnet run -- Computer List
 ```

Para a inserção de dados na tabela Laboratório, utilize o seguinte comando:
 ```
 dotnet run -- Lab New id number name block
 ```

Para exibir os dados dessa tabela, utilize:

 ```
 dotnet run -- Lab List
 ```