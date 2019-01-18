"# FinancialDemo" 

Regra de negócio:
	
Criar um microservices que, através de um http post efetue uma operação de debito (origem) e credito (destino) nas contas correntes.

Entidades: Contacorrente (Account), lancamentos (Transactions)

Parâmetros de entrada:
	conta origem (sourceAccountId)
	conta destino (destinationAccountId)
	valor (amount)

Parâmetros de saída:
	http status code	
