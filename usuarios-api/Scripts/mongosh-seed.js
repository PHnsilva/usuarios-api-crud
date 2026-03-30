use('cadastrodb');

db.usuarios.createIndex({ email: 1 }, { unique: true });
db.usuarios.createIndex({ codigoPessoa: 1 }, { unique: true });

db.usuarios.updateOne(
  { email: 'pedro.vargas@sga.pucminas.br' },
  {
    $set: {
      nome: 'Pedro Henrique',
      email: 'pedro.vargas@sga.pucminas.br',
      senha: 'Pedro@123',
      codigoPessoa: '1525997',
      lembreteSenha: 'nome da universidade',
      idade: 21,
      sexo: 'Masculino'
    }
  },
  { upsert: true }
);

db.usuarios.updateOne(
  { email: 'ana.clara@email.com' },
  {
    $set: {
      nome: 'Ana Clara Souza',
      email: 'ana.clara@email.com',
      senha: 'Ana@123',
      codigoPessoa: '2026002',
      lembreteSenha: 'nome do primeiro pet',
      idade: 22,
      sexo: 'Feminino'
    }
  },
  { upsert: true }
);

db.usuarios.find().pretty();
