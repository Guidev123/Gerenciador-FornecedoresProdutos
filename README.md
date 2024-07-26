<body>
    <h1>API de Gerenciamento de Fornecedores e Produtos</h1>

   <p>Esta é uma API para gerenciamento de fornecedores e produtos, com sistema de autenticação usando Identity e JWT.</p>
   <h2>Video Demo da API</h2>

 https://www.youtube.com/watch?v=FkuR7WBtcX4

   <h2>Tecnologias Utilizadas</h2>
    <ul>
        <li>ASP.NET Core</li>
        <li>C#</li>
        <li>.NET Core</li>
        <li>SQL Server</li>
        <li>Entity Framework</li>
    </ul>

   <h2>Arquitetura</h2>
    <p>A aplicação segue uma arquitetura dividida em 3 camadas:</p>
    <ul>
        <li>Camada de Domínio</li>
        <li>Camada de Apresentação</li>
        <li>Camada de Infraestrutura</li>
    </ul>
    
   ![Screenshot_14](https://github.com/Guidev123/CrudFornecedores/assets/155389912/99daf502-c675-44dd-8985-e70912bc07cd)

   <h2>Princípios Utilizados</h2>
    <p>Foram implementados alguns princípios de:</p>
    <ul>
        <li>SOLID</li>
        <li>Domain Driven Design (DDD)</li>
        <li>Clean Code</li>
    </ul>
    <p>Também foi aplicado o padrão Repository e eventos para armazenamento e exibição de erros.</p>

   <h2>Tratamento de Imagem</h2>
    <p>O tratamento de imagem na API é realizado da seguinte forma:</p>
    <ul>
        <li>A API espera receber uma imagem em base64 do frontend.</li>
        <li>Esta imagem é convertida para arquivo (File) e armazenada na propriedade ImagemUpload.</li>
        <li>O nome da imagem é armazenado na propriedade Imagem.</li>
    </ul>

   
</body>
