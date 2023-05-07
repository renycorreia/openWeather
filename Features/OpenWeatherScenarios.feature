@OpenWeather
Feature: Open Weather

Weather forecasts, nowcasts and history in a fast and elegant way

@sucesso
Scenario: Dados meteorológicos atuais
	Given a latitude <lat> de um determinado local
		And a longitude <lon> do mesmo local
	When a api de obtenção de clima é acionada
	Then a API deverá retornar status 200
		And é retornado os dados meteorológicos atuais do local pré determinado <local>
		
		Examples: 
		| lat       | lon        | local     |
		| -23.5489  | -46.6388   | São Paulo |
		| 25.761681 | -80.191788 | Miami     |


@excecao
Scenario: Consulta de dados meteorológicos atuais sem a longitude 
	Given que apenas a latitude <lat> é informada e a longitude não é 
	When a api de obtenção de clima é acionada
	Then a API deverá retornar status 400

	Examples: 
	| lat       |
	| -23.5489  |
	| 25.761681 |