Sql server
considerando o DER aonde Pesquisa_Mercado.satisfacao é do tipo int então 100==100%
SELECT        Produto_Limpeza.nome AS nome_produto_limpeza, 
		      Alimento.nome AS nome_produto_alimento, 
			  Elemento_Estoque.preco-(Elemento_Estoque.preco*0.15) AS preco_kit, 
			 (Elemento_Estoque.preco-(Elemento_Estoque.preco*0.15))-Elemento_Estoque.custo AS lucro_kit,
			 CONVERT(VARCHAR(10), Alimento.data_validade, 103)  AS data_validade_kit
FROM            Alimento INNER JOIN
                         Produto_Limpeza INNER JOIN
                         Pesquisa_Mercado ON Produto_Limpeza.id = Pesquisa_Mercado.id_produto_limpeza INNER JOIN
                         Elemento_Estoque ON Produto_Limpeza.id_elemento_estoque = Elemento_Estoque.id INNER JOIN
                         Estoque ON Elemento_Estoque.id = Estoque.id_elemento_estoque ON Alimento.id_elemento_estoque = Elemento_Estoque.id
WHERE        (Pesquisa_Mercado.satisfacao > 70) AND   CONVERT(int, DATEDIFF(DAY,GETDATE(),Alimento.data_validade))<5
ORDER BY lucro_kit DESC